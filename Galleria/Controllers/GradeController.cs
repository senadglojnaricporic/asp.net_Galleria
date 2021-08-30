using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Galleria.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Galleria.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GradeController : Controller
    {
        private readonly IControllerRepository _repo;

        public GradeController(IControllerRepository repo)
        {
            _repo = repo;
        }

        // GET: Grade
        public async Task<IActionResult> Index()
        {
            return View(await _repo.Grade_Index());
        }

        // GET: Grade/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _repo.Grade_Details((int) id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // GET: Grade/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Grade/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GradeId,GradeNum")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                await _repo.Grade_Create(grade);
                return RedirectToAction(nameof(Index));
            }
            return View(grade);
        }

        // GET: Grade/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _repo.Grade_Edit((int) id);
            if (grade == null)
            {
                return NotFound();
            }
            return View(grade);
        }

        // POST: Grade/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GradeId,GradeNum")] Grade grade)
        {
            if (id != grade.GradeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repo.Grade_Edit(grade);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeExists(grade.GradeId))
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
            return View(grade);
        }

        // GET: Grade/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _repo.Grade_Delete((int) id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // POST: Grade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repo.Grade_DeleteConfirmed(id);
            return RedirectToAction(nameof(Index));
        }

        private bool GradeExists(int id)
        {
            return _repo.Grade_Exists(id);
        }
    }
}
