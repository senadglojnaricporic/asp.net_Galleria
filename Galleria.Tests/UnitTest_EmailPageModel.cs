using Xunit;
using Moq;
using Microsoft.AspNetCore.Identity;
using Galleria.Interfaces;
using Galleria.Areas.Identity.Pages.Account.Manage;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Galleria.Tests
{
    public class UnitTest_EmailPageModel
    {
        [Fact]
        public void Test_OnGetAsync()
        {
            //Arrange
            var emailData = new EmailData() 
            {
                email = "some text",
                IsEmailConfirmed = true
            };
            var IdUser = new IdentityUser();
            var mock_PageModelRepo = new Mock<IPageModelRepository>();
            mock_PageModelRepo.Setup(x => x.Email_LoadAsync(IdUser).Result).Returns(emailData);
            mock_PageModelRepo.Setup(x => x.Email_OnGetAsync(null).Result).Returns(IdUser);
            var pageModel = new EmailModel(mock_PageModelRepo.Object);

            //Act
            var result = pageModel.OnGetAsync().Result;

            //Assert
            var pageResult = Assert.IsType<PageResult>(result);

        }

        [Fact]
        public void Test_OnGetAsync_UserNull()
        {
            //Arrange
            var emailData = new EmailData() 
            {
                email = "some text",
                IsEmailConfirmed = true
            };
            var IdUser = new IdentityUser();
            var mock_PageModelRepo = new Mock<IPageModelRepository>();
            mock_PageModelRepo.Setup(x => x.Email_LoadAsync(IdUser).Result).Returns(emailData);
            mock_PageModelRepo.Setup(x => x.Email_OnGetAsync(null).Result).Returns((IdentityUser)null);
            var pageModel = new EmailModel(mock_PageModelRepo.Object);

            //Act
            var result = pageModel.OnGetAsync().Result;

            //Assert
            var pageResult = Assert.IsType<NotFoundObjectResult>(result);

        }

        [Fact]
        public void Test_OnPostChangeEmailAsync()
        {
            //Arrange
            var IdUser = new IdentityUser();
            var mock_PageModelRepo = new Mock<IPageModelRepository>();
            var pageModel = new EmailModel(mock_PageModelRepo.Object);
            pageModel.Input = new EmailModel.InputModel(){ NewEmail = "same text" };
            mock_PageModelRepo.Setup(x => x.Email_OnPostChangeEmailAsync_GetUser(null).Result).Returns(IdUser);
            mock_PageModelRepo.Setup(x => x.Email_OnPostChangeEmailAsync_GetEmail(IdUser).Result).Returns("same text");
            
            //Act
            var result = pageModel.OnPostChangeEmailAsync().Result;

            //Assert
            var pageResult = Assert.IsType<RedirectToPageResult>(result);

        }

        [Fact]
        public void Test_OnPostChangeEmailAsync_UserNull()
        {
            //Arrange
            var IdUser = new IdentityUser();
            var mock_PageModelRepo = new Mock<IPageModelRepository>();
            var pageModel = new EmailModel(mock_PageModelRepo.Object);
            pageModel.Input = new EmailModel.InputModel(){ NewEmail = "same text" };
            mock_PageModelRepo.Setup(x => x.Email_OnPostChangeEmailAsync_GetUser(null).Result).Returns((IdentityUser)null);
            mock_PageModelRepo.Setup(x => x.Email_OnPostChangeEmailAsync_GetEmail(IdUser).Result).Returns("same text");
            
            //Act
            var result = pageModel.OnPostChangeEmailAsync().Result;

            //Assert
            var pageResult = Assert.IsType<NotFoundObjectResult>(result);

        }

        [Fact]
        public void Test_OnPostChangeEmailAsync_ModelStateNotValid()
        {
            //Arrange
            var emailData = new EmailData() 
            {
                email = "same text",
                IsEmailConfirmed = true
            };
            var IdUser = new IdentityUser();
            var mock_PageModelRepo = new Mock<IPageModelRepository>();
            var pageModel = new EmailModel(mock_PageModelRepo.Object);
            mock_PageModelRepo.Setup(x => x.Email_OnPostChangeEmailAsync_GetUser(null).Result).Returns(IdUser);
            mock_PageModelRepo.Setup(x => x.Email_LoadAsync(IdUser).Result).Returns(emailData);
            
            //Act
            pageModel.ModelState.AddModelError("key", "error");
            var result = pageModel.OnPostChangeEmailAsync().Result;

            //Assert
            var pageResult = Assert.IsType<PageResult>(result);

        }

        [Fact]
        public void Test_OnPostChangeEmailAsync_EmailNotSame()
        {
            //Arrange
            var emailData = new EmailData() 
            {
                email = "email text",
                IsEmailConfirmed = true
            };
            var IdUser = new IdentityUser();
            var mock_PageModelRepo = new Mock<IPageModelRepository>();
            var pageModel = new EmailModel(mock_PageModelRepo.Object);
            pageModel.Input = new EmailModel.InputModel(){ NewEmail = "input text" };
            var url = new Mock<IUrlHelper>();
            var request = new Mock<HttpRequest>();
            mock_PageModelRepo.Setup(x => x.Email_OnPostChangeEmailAsync_GetUser(null).Result).Returns(IdUser);
            mock_PageModelRepo.Setup(x => x.Email_OnPostChangeEmailAsync_GetEmail(IdUser).Result).Returns(emailData.email);
            mock_PageModelRepo.Setup(x => x.Email_OnPostChangeEmailAsync(IdUser, pageModel.Input.NewEmail, 
                                    url.Object, request.Object)).Returns(It.IsAny<Task>());
            
            //Act
            var result = pageModel.OnPostChangeEmailAsync().Result;

            //Assert
            var pageResult = Assert.IsType<RedirectToPageResult>(result);

        }

        [Fact]
        public void Test_OnPostSendVerificationEmailAsync()
        {
            //Arrange
            var IdUser = new IdentityUser();
            var mock_PageModelRepo = new Mock<IPageModelRepository>();
            var pageModel = new EmailModel(mock_PageModelRepo.Object);
            var url = new Mock<IUrlHelper>();
            var request = new Mock<HttpRequest>();
            mock_PageModelRepo.Setup(x => x.Email_OnPostSendVerificationEmailAsync_GetUser(null).Result).Returns(IdUser);
            mock_PageModelRepo.Setup(x => x.Email_OnPostSendVerificationEmailAsync(IdUser, 
                                    url.Object, request.Object)).Returns(It.IsAny<Task>());
            
            //Act
            var result = pageModel.OnPostSendVerificationEmailAsync().Result;

            //Assert
            var pageResult = Assert.IsType<RedirectToPageResult>(result);

        }

        [Fact]
        public void Test_OnPostSendVerificationEmailAsync_UserNull()
        {
            //Arrange
            var IdUser = new IdentityUser();
            var mock_PageModelRepo = new Mock<IPageModelRepository>();
            var pageModel = new EmailModel(mock_PageModelRepo.Object);
            var url = new Mock<IUrlHelper>();
            var request = new Mock<HttpRequest>();
            mock_PageModelRepo.Setup(x => x.Email_OnPostSendVerificationEmailAsync_GetUser(null).Result).Returns((IdentityUser)null);
            mock_PageModelRepo.Setup(x => x.Email_OnPostSendVerificationEmailAsync(IdUser, 
                                    url.Object, request.Object)).Returns(It.IsAny<Task>());
            
            //Act
            var result = pageModel.OnPostSendVerificationEmailAsync().Result;

            //Assert
            var pageResult = Assert.IsType<NotFoundObjectResult>(result);

        }

        [Fact]
        public void Test_OnPostSendVerificationEmailAsync_ModelStateNotValid()
        {
            //Arrange
            var emailData = new EmailData() 
            {
                email = "some text",
                IsEmailConfirmed = true
            };
            var IdUser = new IdentityUser();
            var mock_PageModelRepo = new Mock<IPageModelRepository>();
            var pageModel = new EmailModel(mock_PageModelRepo.Object);
            var url = new Mock<IUrlHelper>();
            var request = new Mock<HttpRequest>();
            mock_PageModelRepo.Setup(x => x.Email_LoadAsync(IdUser).Result).Returns(emailData);
            mock_PageModelRepo.Setup(x => x.Email_OnPostSendVerificationEmailAsync_GetUser(null).Result).Returns(IdUser);
            mock_PageModelRepo.Setup(x => x.Email_OnPostSendVerificationEmailAsync(IdUser, 
                                    url.Object, request.Object)).Returns(It.IsAny<Task>());
            
            //Act
            pageModel.ModelState.AddModelError("key", "error");
            var result = pageModel.OnPostSendVerificationEmailAsync().Result;

            //Assert
            var pageResult = Assert.IsType<PageResult>(result);

        }
    }
}