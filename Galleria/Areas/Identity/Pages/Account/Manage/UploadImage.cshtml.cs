using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Galleria.Interfaces;
using System.ComponentModel;
using Galleria.Data;

namespace Galleria.Areas.Identity.Pages.Account.Manage
{
    public class UploadImageModel : PageModel
    {
        public IList<Category> CategoriesList { get;set; }
        public IList<Color> ColorsList { get;set; }
        [DefaultValue("")]
        public string FileUploadValidationError { get;set; } 
        private readonly IPageModelRepository _repo;
        public class ImageUploadForm
        {
            [DisplayName("Image File")]
            public IFormFile FormFile { get;set; }
            [DisplayName("Image Category")]
            public int CategoryId { get;set; }
            [DisplayName("Image Color")]
            public int ColorId { get;set; }
        }

        [BindProperty]
        public ImageUploadForm UploadFormData { get;set; }

        public UploadImageModel(IPageModelRepository repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var data = await _repo.UploadImage_OnGetAsync();
            CategoriesList = data.categories;
            ColorsList = data.colors;
            return Page();
        }

        public async Task<IActionResult> OnPostUploadAsync()
        {
            var data = await _repo.UploadImage_OnPostUploadAsync(ModelState.IsValid, UploadFormData, User);

            if(data.isModelValid)
            {
                if(data.isFileValid)
                {
                    if(data.isScanValid)
                    {
                        return RedirectToPage("UploadImage");
                    }
                    FileUploadValidationError = data.validationError;
                    CategoriesList = data.categories;
                    ColorsList = data.colors;
                    return Page();
                }
                FileUploadValidationError = data.validationError;
            }
            CategoriesList = data.categories;
            ColorsList = data.colors;
            return Page();
        }
    }
}
