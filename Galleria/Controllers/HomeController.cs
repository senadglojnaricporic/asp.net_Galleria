using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Galleria.Models;
using Microsoft.AspNetCore.Authorization;
using Galleria.Interfaces;

namespace Galleria.Controllers
{
    public class HomeController : Controller
    {
        private readonly IControllerRepository _repo;

        public HomeController(IControllerRepository repo)
        {
            _repo = repo;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _repo.Home_Index());
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult About()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Gallery()
        {
            return View(await _repo.Home_Gallery());
        }

        public IActionResult Review(int id, bool isDuplicate)
        {
            return View(_repo.Home_Review(id, isDuplicate));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendReview([Bind("newGrade,newComment,photoId,Username")] ReviewData rd)
        {   
            var isDuplicate = rd.isDuplicate; //flag if user has allready voted

            if (ModelState.IsValid)
            {
                isDuplicate = await _repo.Home_SendReview(rd, rd.Username);
            }         
            return RedirectToAction(nameof(Review), new { id = rd.photoId , isDuplicate = isDuplicate});
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
