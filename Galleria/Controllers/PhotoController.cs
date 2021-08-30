using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Galleria.Interfaces;

namespace Galleria.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IControllerRepository _repo;

        public PhotoController(IControllerRepository repo)
        {
            _repo = repo;
        }

        // GET: Photo
        public async Task<IActionResult> Index()
        {
            return View(await _repo.Photo_Index());
        }

        // GET: Photo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photo = await _repo.Photo_Details((int) id);
            if (photo == null)
            {
                return NotFound();
            }

            return View(photo);
        }

        // GET: Photo/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_repo.Get_Categories(), "CategoryId", "Description");
            ViewData["ColorId"] = new SelectList(_repo.Get_Colors(), "ColorId", "Description");
            return View();
        }

        // POST: Photo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhotoId,UserId,CategoryId,ColorId,Timestamp,FileUrl,DisplayName")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                await _repo.Photo_Create(photo);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_repo.Get_Categories(), "CategoryId", "Description", photo.CategoryId);
            ViewData["ColorId"] = new SelectList(_repo.Get_Colors(), "ColorId", "Description", photo.ColorId);
            return View(photo);
        }

        // GET: Photo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photo = await _repo.Photo_Edit((int) id);
            if (photo == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_repo.Get_Categories(), "CategoryId", "Description", photo.CategoryId);
            ViewData["ColorId"] = new SelectList(_repo.Get_Colors(), "ColorId", "Description", photo.ColorId);
            return View(photo);
        }

        // POST: Photo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PhotoId,UserId,CategoryId,ColorId,Timestamp,FileUrl,DisplayName")] Photo photo)
        {
            if (id != photo.PhotoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repo.Photo_Edit(photo);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhotoExists(photo.PhotoId))
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
            ViewData["CategoryId"] = new SelectList(_repo.Get_Categories(), "CategoryId", "Description", photo.CategoryId);
            ViewData["ColorId"] = new SelectList(_repo.Get_Colors(), "ColorId", "Description", photo.ColorId);
            return View(photo);
        }

        // GET: Photo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photo = await _repo.Photo_Delete((int) id);
            if (photo == null)
            {
                return NotFound();
            }

            return View(photo);
        }

        // POST: Photo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repo.Photo_DeleteConfirmed(id);
            return RedirectToAction(nameof(Index));
        }

        private bool PhotoExists(int id)
        {
            return _repo.Photo_Exists(id);
        }
    }
}
