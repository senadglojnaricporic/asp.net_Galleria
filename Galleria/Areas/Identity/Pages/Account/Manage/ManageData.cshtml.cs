using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace Galleria.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "Admin")]
    public class ManageDataModel : PageModel
    {
        public ManageDataModel()
        {
        }

        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
