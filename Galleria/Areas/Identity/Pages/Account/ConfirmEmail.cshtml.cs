using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Galleria.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;

namespace Galleria.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly IPageModelRepository _repo;

        public ConfirmEmailModel(IPageModelRepository repo)
        {
            _repo = repo;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = new IdentityUser();
            try
            {
                user = await _repo.ConfirmEmail_OnGetAsync_FindById(userId);
            }
            catch(NullReferenceException)
            {
                user = null;
            }
            
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            StatusMessage = await _repo.ConfirmEmail_OnGetAsync_StatusMessage(user, code);
            return Page();
        }
    }
}
