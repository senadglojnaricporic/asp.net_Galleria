using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Galleria.Areas.Identity.Pages.Account.Manage;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using Galleria.Areas.Identity.Pages.Account;


namespace Galleria.Interfaces
{
    public interface IPageModelRepository
    {
        Task<EmailData> Email_LoadAsync(IdentityUser user);

        Task<IdentityUser> Email_OnGetAsync(ClaimsPrincipal user);

        string Email_OnGetAsync_UserId(ClaimsPrincipal user);

        Task<IdentityUser> Email_OnPostChangeEmailAsync_GetUser(ClaimsPrincipal user);

        Task<string> Email_OnPostChangeEmailAsync_GetEmail(IdentityUser user);

        string Email_OnPostChangeEmailAsync_UserId(ClaimsPrincipal user);

        Task Email_OnPostChangeEmailAsync(IdentityUser user, string newEmail, IUrlHelper Url, HttpRequest Request);

        Task<IdentityUser> Email_OnPostSendVerificationEmailAsync_GetUser(ClaimsPrincipal user);

        string Email_OnPostSendVerificationEmailAsync_UserId(ClaimsPrincipal user);

        Task Email_OnPostSendVerificationEmailAsync(IdentityUser user, IUrlHelper Url, HttpRequest Request);

        Task<IndexData> Index_LoadAsync(IdentityUser user);

        Task<IdentityUser> Index_OnGetAsync(ClaimsPrincipal user);

        string Index_OnGetAsync_UserId(ClaimsPrincipal user);

        Task<IdentityUser> Index_OnPostAsync(ClaimsPrincipal user);

        string Index_OnPostAsync_UserId(ClaimsPrincipal user);

        Task<string> Index_OnPostAsync_GetPhoneNumber(IdentityUser user);

        Task<bool> Index_OnPostAsync_SetPhoneNumber(IdentityUser user, string phoneNumber);

        Task Index_OnPostAsync_RefreshSignIn(IdentityUser user);

        Task<UploadData> UploadImage_OnGetAsync();

        Task<UploadData> UploadImage_OnPostUploadAsync(bool IsValid, 
                                                    UploadImageModel.ImageUploadForm UploadFormData,
                                                    ClaimsPrincipal User);
        
        Task<IdentityUser> ConfirmEmail_OnGetAsync_FindById(string userId);

        Task<string> ConfirmEmail_OnGetAsync_StatusMessage(IdentityUser user, string code);

        Task<LoginData> Login_OnPostAsync_PasswordSignIn(string email, string password, bool RememberMe);

        void Login_OnPostAsync_LogInfo();

        void Login_OnPostAsync_LogWarning();
    }
}