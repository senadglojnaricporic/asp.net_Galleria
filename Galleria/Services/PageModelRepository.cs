using System;
using System.Threading.Tasks;
using Galleria.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Galleria.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Galleria.Areas.Identity.Pages.Account.Manage;
using Microsoft.Extensions.Logging;
using Galleria.Areas.Identity.Pages.Account;



namespace Galleria.Services
{
    public class PageModelRepository : IPageModelRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IEmailText _emailText;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly GalleriaContext _context;
        private readonly IFileHelper _fileHelper;
        private readonly ILogger<LoginModel> _logger;

        public PageModelRepository(UserManager<IdentityUser> userManager, 
                                    IEmailSender emailSender, 
                                    IEmailText emailText,
                                    SignInManager<IdentityUser> signInManager,
                                    IFileHelper fileHelper,
                                    GalleriaContext context,
                                    ILogger<LoginModel> logger)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _emailText = emailText;
            _signInManager = signInManager;
            _fileHelper = fileHelper;
            _context = context;
            _logger = logger;
        }

        public virtual async Task<EmailData> Email_LoadAsync(IdentityUser user)
        {
            var data = new EmailData();
            data.email = await _userManager.GetEmailAsync(user);
            data.IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            return data;
        }

        public virtual async Task<IdentityUser> Email_OnGetAsync(ClaimsPrincipal user)
        {
            return await _userManager.GetUserAsync(user);
        }

        public virtual async Task<IdentityUser> Email_OnPostChangeEmailAsync_GetUser(ClaimsPrincipal user)
        {
            return await _userManager.GetUserAsync(user);
        }

        public virtual async Task<string> Email_OnPostChangeEmailAsync_GetEmail(IdentityUser user)
        {
            return await _userManager.GetEmailAsync(user);
        }

        public virtual async Task Email_OnPostChangeEmailAsync(IdentityUser user, string newEmail, IUrlHelper Url, HttpRequest Request)
        {
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmailChange",
                pageHandler: null,
                values: new { userId = userId, email = newEmail, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                newEmail,
                _emailText.getEmailTitle(), 
                String.Format(_emailText.getEmailText(), HtmlEncoder.Default.Encode(callbackUrl)));
        }

        public virtual async Task<IdentityUser> Email_OnPostSendVerificationEmailAsync_GetUser(ClaimsPrincipal user)
        {
            return await _userManager.GetUserAsync(user);
        }

        public virtual async Task Email_OnPostSendVerificationEmailAsync(IdentityUser user, IUrlHelper Url, HttpRequest Request)
        {
            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                email,
                _emailText.getEmailTitle(),
                String.Format(_emailText.getEmailText(), HtmlEncoder.Default.Encode(callbackUrl)));
        }

        public virtual string Email_OnGetAsync_UserId(ClaimsPrincipal user)
        {
            return _userManager.GetUserId(user);
        }

        public virtual string Email_OnPostChangeEmailAsync_UserId(ClaimsPrincipal user)
        {
            return _userManager.GetUserId(user);
        }

        public virtual string Email_OnPostSendVerificationEmailAsync_UserId(ClaimsPrincipal user)
        {
            return _userManager.GetUserId(user);
        }

        public virtual async Task<IndexData> Index_LoadAsync(IdentityUser user)
        {
            var data = new IndexData();
            data.userName = await _userManager.GetUserNameAsync(user);
            data.phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            return data;
        }

        public virtual async Task<IdentityUser> Index_OnGetAsync(ClaimsPrincipal user)
        {
            return await _userManager.GetUserAsync(user);
        }

        public virtual async Task<IdentityUser> Index_OnPostAsync(ClaimsPrincipal user)
        {
            return await _userManager.GetUserAsync(user);
        }

        public virtual async Task<string> Index_OnPostAsync_GetPhoneNumber(IdentityUser user)
        {
            return await _userManager.GetPhoneNumberAsync(user);
        }

        public virtual async Task<bool> Index_OnPostAsync_SetPhoneNumber(IdentityUser user, string phoneNumber)
        {
            var result = await _userManager.SetPhoneNumberAsync(user, phoneNumber);
            return result.Succeeded;
        }

        public virtual async Task Index_OnPostAsync_RefreshSignIn(IdentityUser user)
        {
            await _signInManager.RefreshSignInAsync(user);
        }

        public virtual string Index_OnGetAsync_UserId(ClaimsPrincipal user)
        {
            return _userManager.GetUserId(user);
        }

        public virtual string Index_OnPostAsync_UserId(ClaimsPrincipal user)
        {
            return _userManager.GetUserId(user);
        }

        public virtual async Task<UploadData> UploadImage_OnGetAsync()
        {
            var data = new UploadData();
            data.categories = await _context.Categories.ToListAsync();
            data.colors = await _context.Colors.ToListAsync();
            return data;
        }

        public virtual async Task<UploadData> UploadImage_OnPostUploadAsync(bool isValid, 
                                                                        UploadImageModel.ImageUploadForm UploadFormData,
                                                                        ClaimsPrincipal User)
        {
            var data = new UploadData();

            if(isValid)
            {
                data.isModelValid = true;
                if(_fileHelper.isFileValid(UploadFormData.FormFile))
                {
                    data.isFileValid = true;
                    var tempFileURL = _fileHelper.TempFileURL(UploadFormData.FormFile.FileName);
                    using (var stream = System.IO.File.Create(tempFileURL))
                    {
                        await UploadFormData.FormFile.CopyToAsync(stream);
                    }
                    if(_fileHelper.isScanValid(tempFileURL))
                    {
                        var photo = new Photo();
                        photo.UserId = _userManager.GetUserId(User);
                        photo.CategoryId = UploadFormData.CategoryId;
                        photo.ColorId = UploadFormData.ColorId;
                        photo.Timestamp = DateTime.UtcNow;
                        photo.FileUrl = _fileHelper.DBFileURL(UploadFormData.FormFile.FileName);
                        photo.DisplayName = _fileHelper.DisplayName(UploadFormData.FormFile.FileName);
                        _context.Add(photo);
                        await _context.SaveChangesAsync();
                        using (var stream = System.IO.File.Create(_fileHelper.FileURL(UploadFormData.FormFile.FileName)))
                        {
                            await UploadFormData.FormFile.CopyToAsync(stream);
                        }
                        System.IO.File.Delete(tempFileURL);
                        
                        return data;
                    }
                    data.validationError = _fileHelper.Message;
                    data.categories = await _context.Categories.ToListAsync();
                    data.colors = await _context.Colors.ToListAsync();
                    System.IO.File.Delete(tempFileURL);
                    
                    return data;
                }
                data.validationError = "Max. file size is 2 MB. Permitted file types are: .jpeg, .jpg, .png.";
            }
            data.categories = await _context.Categories.ToListAsync();
            data.colors = await _context.Colors.ToListAsync();
            
            return data;
        }

        public virtual async Task<IdentityUser> ConfirmEmail_OnGetAsync_FindById(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public virtual async Task<string> ConfirmEmail_OnGetAsync_StatusMessage(IdentityUser user, string code)
        {
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            var StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";

            return StatusMessage;
        }

        public virtual async Task<LoginData> Login_OnPostAsync_PasswordSignIn(string email, string password, bool RememberMe)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(email, password, RememberMe, lockoutOnFailure: false);
            var data = new LoginData(){
                Succeeded = result.Succeeded,
                RequiresTwoFactor = result.RequiresTwoFactor,
                IsLockedOut = result.IsLockedOut
            };

            return data;
        }

        public virtual void Login_OnPostAsync_LogInfo()
        {
            _logger.LogInformation("User logged in.");
        }

        public virtual void Login_OnPostAsync_LogWarning()
        {
            _logger.LogWarning("User account locked out.");
        }
    }
}