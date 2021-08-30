using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Galleria.Interfaces
{
    public interface IControllerRepository
    {
        DbSet<Category> Get_Categories();

        Task<List<Category>> Category_Index();

        Task<Category> Category_Details(int id);

        Task<int> Category_Create(Category category);

        ValueTask<Category> Category_Edit(int id);

        Task<int> Category_Edit(Category category);

        Task<Category> Category_Delete(int id);

        Task<int> Category_DeleteConfirmed(int id);

        bool Category_Exists(int id);

        DbSet<Color> Get_Colors();

        Task<List<Color>> Color_Index();

        Task<Color> Color_Details(int id);

        Task<int> Color_Create(Color color);

        ValueTask<Color> Color_Edit(int id);

        Task<int> Color_Edit(Color color);

        Task<Color> Color_Delete(int id);

        Task<int> Color_DeleteConfirmed(int id);

        bool Color_Exists(int id);

        DbSet<Grade> Get_Grades();

        Task<List<Grade>> Grade_Index();

        Task<Grade> Grade_Details(int id);

        Task<int> Grade_Create(Grade grade);

        ValueTask<Grade> Grade_Edit(int id);

        Task<int> Grade_Edit(Grade grade);

        Task<Grade> Grade_Delete(int id);

        Task<int> Grade_DeleteConfirmed(int id);

        bool Grade_Exists(int id);

        Task<List<Photo>> Home_Index();

        Task<List<Photo>> Home_Gallery();

        ReviewData Home_Review(int id, bool isDuplicate);

        Task<bool> Home_SendReview(ReviewData rd, string username);

        DbSet<Photo> Get_Photos();

        Task<List<Photo>> Photo_Index();

        Task<Photo> Photo_Details(int id);

        Task<int> Photo_Create(Photo photo);

        ValueTask<Photo> Photo_Edit(int id);

        Task<int> Photo_Edit(Photo photo);

        Task<Photo> Photo_Delete(int id);

        Task<int> Photo_DeleteConfirmed(int id);

        bool Photo_Exists(int id);

        Task<List<Review>> Review_Index();

        Task<Review> Review_Details(string id);

        Task<int> Review_Create(Review review);

        ValueTask<Review> Review_Edit(string id);

        Task<int> Review_Edit(Review review);

        Task<Review> Review_Delete(string id);

        Task<int> Review_DeleteConfirmed(string id);

        bool Review_Exists(string id);
    }
}