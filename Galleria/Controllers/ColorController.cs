using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Galleria.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Galleria.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ColorController : Controller
    {
        private readonly IControllerRepository _repo;

        public ColorController(IControllerRepository repo)
        {
            _repo = repo;
        }

        // GET: Color
        public async Task<IActionResult> Index()
        {
            return View(await _repo.Color_Index());
        }

        // GET: Color/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var color = await _repo.Color_Details((int) id);
            if (color == null)
            {
                return NotFound();
            }

            return View(color);
        }

        // GET: Color/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Color/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ColorId,Description")] Color color)
        {
            if (ModelState.IsValid)
            {
                await _repo.Color_Create(color);
                return RedirectToAction(nameof(Index));
            }
            return View(color);
        }

        // GET: Color/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var color = await _repo.Color_Edit((int) id);
            if (color == null)
            {
                return NotFound();
            }
            return View(color);
        }

        // POST: Color/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ColorId,Description")] Color color)
        {
            if (id != color.ColorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repo.Color_Edit(color);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColorExists(color.ColorId))
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
            return View(color);
        }

        // GET: Color/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var color = await _repo.Color_Delete((int) id);
            if (color == null)
            {
                return NotFound();
            }

            return View(color);
        }

        // POST: Color/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repo.Color_DeleteConfirmed(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ColorExists(int id)
        {
            return _repo.Color_Exists(id);
        }
    }
}
