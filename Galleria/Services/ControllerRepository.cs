using Galleria.Interfaces;
using Galleria.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Npgsql;

namespace Galleria.Services
{
    public class ControllerRepository : IControllerRepository
    {
        private readonly GalleriaContext _context;
        private readonly ApplicationDbContext _appDBContext;
        public ControllerRepository(GalleriaContext context, ApplicationDbContext appDBContext)
        {
            _context = context;
            _appDBContext = appDBContext;
        }

        public virtual DbSet<Category> Get_Categories()
        {
            return _context.Categories;
        }

        public virtual async Task<List<Category>> Category_Index()
        {
            return await _context.Categories.ToListAsync();
        }

        public virtual async Task<Category> Category_Details(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(m => m.CategoryId == id);
        }

        public virtual async Task<int> Category_Create(Category category)
        {
            _context.Add(category);
            return await _context.SaveChangesAsync();
        }

        public virtual async ValueTask<Category> Category_Edit(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public virtual async Task<int> Category_Edit(Category category)
        {
            _context.Update(category);
            return await _context.SaveChangesAsync();
        }

        public virtual async Task<Category> Category_Delete(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(m => m.CategoryId == id);
        }

        public virtual async Task<int> Category_DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            return await _context.SaveChangesAsync();
        }

        public virtual bool Category_Exists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }

        public virtual DbSet<Color> Get_Colors()
        {
            return _context.Colors;
        }

        public virtual async Task<List<Color>> Color_Index()
        {
            return await _context.Colors.ToListAsync();
        }

        public virtual async Task<Color> Color_Details(int id)
        {
            return await _context.Colors.FirstOrDefaultAsync(m => m.ColorId == id);
        }

        public virtual async Task<int> Color_Create(Color color)
        {
            _context.Add(color);
            return await _context.SaveChangesAsync();
        }

        public virtual async ValueTask<Color> Color_Edit(int id)
        {
            return await _context.Colors.FindAsync(id);
        }

        public virtual async Task<int> Color_Edit(Color color)
        {
            _context.Update(color);
            return await _context.SaveChangesAsync();
        }

        public virtual async Task<Color> Color_Delete(int id)
        {
            return await _context.Colors.FirstOrDefaultAsync(m => m.ColorId == id);
        }

        public virtual async Task<int> Color_DeleteConfirmed(int id)
        {
            var color = await _context.Colors.FindAsync(id);
            _context.Colors.Remove(color);
            return await _context.SaveChangesAsync();
        }

        public virtual bool Color_Exists(int id)
        {
            return _context.Colors.Any(e => e.ColorId == id);
        }

        public virtual DbSet<Grade> Get_Grades()
        {
            return _context.Grades;
        }

        public virtual async Task<List<Grade>> Grade_Index()
        {
            return await _context.Grades.ToListAsync();
        }

        public virtual async Task<Grade> Grade_Details(int id)
        {
            return await _context.Grades.FirstOrDefaultAsync(m => m.GradeId == id);
        }

        public virtual async Task<int> Grade_Create(Grade grade)
        {
            _context.Add(grade);
            return await _context.SaveChangesAsync();
        }

        public virtual async ValueTask<Grade> Grade_Edit(int id)
        {
            return await _context.Grades.FindAsync(id);
        }

        public virtual async Task<int> Grade_Edit(Grade grade)
        {
            _context.Update(grade);
            return await _context.SaveChangesAsync();
        }

        public virtual async Task<Grade> Grade_Delete(int id)
        {
            return await _context.Grades.FirstOrDefaultAsync(m => m.GradeId == id);
        }

        public virtual async Task<int> Grade_DeleteConfirmed(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            _context.Grades.Remove(grade);
            return await _context.SaveChangesAsync();
        }

        public virtual bool Grade_Exists(int id)
        {
            return _context.Grades.Any(e => e.GradeId == id);
        }

        public virtual async Task<List<Photo>> Home_Index()
        {
            return await _context.Photos.ToListAsync();
        }

        public virtual async Task<List<Photo>> Home_Gallery()
        {
            return await _context.Photos.ToListAsync();
        }

        public virtual ReviewData Home_Review(int id, bool isDuplicate)
        {
            var rd = new ReviewData();
            rd.ReviewList = new List<ReviewData.Reviews>();
            rd.FileUrl = _context.Photos.Single(p => p.PhotoId == id).FileUrl;
            rd.photoId = id;
            rd.GradesList = _context.Grades.ToList();
            rd.isDuplicate = isDuplicate;
            try
            {
                rd.averageGrade = _context.Reviews.Include(r => r.Grade).Where(i => i.PhotoId == id)
                .ToList().Select(x => x.Grade).Average(y => y.GradeNum);
            }
            catch(InvalidOperationException)
            {
                rd.averageGrade = 0;
            }
            var reviewList = _context.Reviews.Where(r => r.PhotoId == id);
            foreach(var item in reviewList)
            {
                var userID = item.UserId;
                var username = _appDBContext.Users.Single(u => u.Id == userID).UserName;
                var review = new ReviewData.Reviews(){
                    Username = username,
                    Grade = item.Grade.GradeNum,
                    Timestamp = item.Timestamp,
                    Comment = item.Comment
                };
                rd.ReviewList.Add(review);
            }
            return rd;
        }

        public virtual async Task<bool> Home_SendReview(ReviewData rd, string username)
        {
            var _isDuplicate = false;
            var review = new Review();
            review.GradeId = rd.newGrade;
            review.Comment = rd.newComment;
            review.UserId = _appDBContext.Users.Single(u => u.UserName == username).Id;
            review.PhotoId = rd.photoId;
            review.Timestamp = DateTime.UtcNow;
            _context.Add(review);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ex.GetBaseException() is PostgresException pgException)
                {
                    switch (pgException.SqlState)
                    {
                        case "23505":
                            _isDuplicate =  true;
                            break;
                        default:
                            throw;
                    }
                }
            }
            return _isDuplicate;
        }

        public virtual DbSet<Photo> Get_Photos()
        {
            return _context.Photos;
        }

        public virtual async Task<List<Photo>> Photo_Index()
        {
            var galleriaContext = _context.Photos.Include(p => p.Category).Include(p => p.Color);
            return await galleriaContext.ToListAsync();
        }

        public virtual async Task<Photo> Photo_Details(int id)
        {
            return await _context.Photos.Include(p => p.Category).Include(p => p.Color).FirstOrDefaultAsync(m => m.PhotoId == id);
        }

        public virtual async Task<int> Photo_Create(Photo photo)
        {
            _context.Add(photo);
            return await _context.SaveChangesAsync();
        }

        public virtual async ValueTask<Photo> Photo_Edit(int id)
        {
            return await _context.Photos.FindAsync(id);
        }

        public virtual async Task<int> Photo_Edit(Photo photo)
        {
            _context.Update(photo);
            return await _context.SaveChangesAsync();
        }

        public virtual async Task<Photo> Photo_Delete(int id)
        {
            return await _context.Photos.Include(p => p.Category).Include(p => p.Color).FirstOrDefaultAsync(m => m.PhotoId == id);
        }

        public virtual async Task<int> Photo_DeleteConfirmed(int id)
        {
            var photo = await _context.Photos.FindAsync(id);
            _context.Photos.Remove(photo);
            return await _context.SaveChangesAsync();
        }

        public virtual bool Photo_Exists(int id)
        {
            return _context.Photos.Any(e => e.PhotoId == id);
        }

        public virtual async Task<List<Review>> Review_Index()
        {
            var galleriaContext = _context.Reviews.Include(r => r.Grade).Include(r => r.Photo);
            return await galleriaContext.ToListAsync();
        }

        public virtual async Task<Review> Review_Details(string id)
        {
            return await _context.Reviews.Include(r => r.Grade).Include(r => r.Photo).FirstOrDefaultAsync(m => m.UserId == id);
        }

        public virtual async Task<int> Review_Create(Review review)
        {
            _context.Add(review);
            return await _context.SaveChangesAsync();
        }

        public virtual async ValueTask<Review> Review_Edit(string id)
        {
            return await _context.Reviews.FindAsync(id);
        }

        public virtual async Task<int> Review_Edit(Review review)
        {
            _context.Update(review);
            return await _context.SaveChangesAsync();
        }

        public virtual async Task<Review> Review_Delete(string id)
        {
            return await _context.Reviews.Include(r => r.Grade).Include(r => r.Photo).FirstOrDefaultAsync(m => m.UserId == id);
        }

        public virtual async Task<int> Review_DeleteConfirmed(string id)
        {
            var review = await _context.Reviews.FindAsync(id);
            _context.Reviews.Remove(review);
            return await _context.SaveChangesAsync();
        }

        public virtual bool Review_Exists(string id)
        {
            return _context.Reviews.Any(e => e.UserId == id);
        }
    }
}