using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Galleria.Interfaces;

namespace Galleria.Areas.Identity.Pages.Account.Manage
{
    public partial class EmailModel : PageModel
    {
        private readonly IPageModelRepository _repo;

        public EmailModel(IPageModelRepository repo) 
        {
            _repo = repo;
        }

        public string Username { get; set; }

        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "New email")]
            public string NewEmail { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var data = await _repo.Email_LoadAsync(user);

            Email = data.email;

            Input = new InputModel
            {
                NewEmail = data.email,
            };

            IsEmailConfirmed = data.IsEmailConfirmed;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = new IdentityUser();
            try
            {
                user = await _repo.Email_OnGetAsync(User);
            }
            catch(NullReferenceException)
            {
                user = null;
            }
            
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_repo.Email_OnGetAsync_UserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostChangeEmailAsync()
        {
            var user = new IdentityUser();
            try
            {
                user = await _repo.Email_OnPostChangeEmailAsync_GetUser(User);
            }
            catch(NullReferenceException)
            {
                user = null;
            }
            
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_repo.Email_OnPostChangeEmailAsync_UserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var email = await _repo.Email_OnPostChangeEmailAsync_GetEmail(user);
            if (Input.NewEmail != email)
            {
                await _repo.Email_OnPostChangeEmailAsync(user, Input.NewEmail, Url, Request);

                StatusMessage = "Confirmation link to change email sent. Please check your email.";
                return RedirectToPage();
            }

            StatusMessage = "Your email is unchanged.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            var user = new IdentityUser();
            try
            {
                user = await _repo.Email_OnPostSendVerificationEmailAsync_GetUser(User);
            }
            catch
            {
                user = null;
            }
            
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_repo.Email_OnPostSendVerificationEmailAsync_UserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }
            
            await _repo.Email_OnPostSendVerificationEmailAsync(user, Url, Request);

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage();
        }
    }
}
