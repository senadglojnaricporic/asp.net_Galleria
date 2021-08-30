using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Galleria.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Galleria.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReviewController : Controller
    {
        private readonly IControllerRepository _repo;

        public ReviewController(IControllerRepository repo)
        {
            _repo = repo;
        }

        // GET: Review
        public async Task<IActionResult> Index()
        {
            
            return View(await _repo.Review_Index());
        }

        // GET: Review/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _repo.Review_Details(id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Review/Create
        public IActionResult Create()
        {
            ViewData["GradeId"] = new SelectList(_repo.Get_Grades(), "GradeId", "GradeId");
            ViewData["PhotoId"] = new SelectList(_repo.Get_Photos(), "PhotoId", "FileUrl");
            return View();
        }

        // POST: Review/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,PhotoId,GradeId,Timestamp,Comment")] Review review)
        {
            if (ModelState.IsValid)
            {
                await _repo.Review_Create(review);
                return RedirectToAction(nameof(Index));
            }
            ViewData["GradeId"] = new SelectList(_repo.Get_Grades(), "GradeId", "GradeId", review.GradeId);
            ViewData["PhotoId"] = new SelectList(_repo.Get_Photos(), "PhotoId", "FileUrl", review.PhotoId);
            return View(review);
        }

        // GET: Review/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _repo.Review_Edit(id);
            if (review == null)
            {
                return NotFound();
            }
            ViewData["GradeId"] = new SelectList(_repo.Get_Grades(), "GradeId", "GradeId", review.GradeId);
            ViewData["PhotoId"] = new SelectList(_repo.Get_Photos(), "PhotoId", "FileUrl", review.PhotoId);
            return View(review);
        }

        // POST: Review/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserId,PhotoId,GradeId,Timestamp,Comment")] Review review)
        {
            if (id != review.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repo.Review_Edit(review);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GradeId"] = new SelectList(_repo.Get_Grades(), "GradeId", "GradeId", review.GradeId);
            ViewData["PhotoId"] = new SelectList(_repo.Get_Photos(), "PhotoId", "FileUrl", review.PhotoId);
            return View(review);
        }

        // GET: Review/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _repo.Review_Delete(id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Review/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _repo.Review_DeleteConfirmed(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(string id)
        {
            return _repo.Review_Exists(id);
        }
    }
}
