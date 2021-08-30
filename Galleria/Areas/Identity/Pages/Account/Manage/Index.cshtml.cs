using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Galleria.Interfaces;
using System;

namespace Galleria.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly IPageModelRepository _repo; 

        public IndexModel(IPageModelRepository repo)
        {
            _repo = repo;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var data = await _repo.Index_LoadAsync(user);

            Username = data.userName;

            Input = new InputModel
            {
                PhoneNumber = data.phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = new IdentityUser();
            try
            {
                user = await _repo.Index_OnGetAsync(User);
            }
            catch(NullReferenceException)
            {
                user = null;
            }
            
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_repo.Index_OnGetAsync_UserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = new IdentityUser();
            try
            {
                user = await _repo.Index_OnPostAsync(User);
            }
            catch(NullReferenceException)
            {
                user = null;
            }
            
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_repo.Index_OnPostAsync_UserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _repo.Index_OnPostAsync_GetPhoneNumber(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = new bool();
                try
                {
                    setPhoneResult = await _repo.Index_OnPostAsync_SetPhoneNumber(user, Input.PhoneNumber);
                    if (!setPhoneResult)
                    {
                        StatusMessage = "Unexpected error when trying to set phone number.";
                        return RedirectToPage();
                    }
                }
                catch(NullReferenceException)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            await _repo.Index_OnPostAsync_RefreshSignIn(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
