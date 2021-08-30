using Xunit;
using Galleria.Areas.Identity.Pages.Account;
using Moq;
using Galleria.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;



namespace Galleria.Tests
{
    public class UnitTest_LoginModel
    {
        [Fact]
        public void Test_OnGetAsync()
        {
            //Arrange
            var mock_PageModelRepo = new Mock<IPageModelRepository>();
            var pageModel = new LoginModel(mock_PageModelRepo.Object);

            //Act
            var result = pageModel.OnGetAsync();
            
            //Assert
            Assert.Equal(null, pageModel.ReturnUrl);
        }

        [Fact]
        public void Test_OnGetAsync_StringNotNull()
        {
            //Arrange
            var mock_PageModelRepo = new Mock<IPageModelRepository>();
            var pageModel = new LoginModel(mock_PageModelRepo.Object);

            //Act
            var result = pageModel.OnGetAsync("/index");
            
            //Assert
            Assert.Equal("/index", pageModel.ReturnUrl);
        }

        [Fact]
        public void Test_OnPostAsync_Succeeded()
        {
            //Arrange
            var data = new LoginData(){
                Succeeded = true,
                RequiresTwoFactor = false,
                IsLockedOut = false
            };
            var mock_PageModelRepo = new Mock<IPageModelRepository>();
            var pageModel = new LoginModel(mock_PageModelRepo.Object);
            pageModel.Input = new LoginModel.InputModel(){
                Email = "xxx",
                Password = "xxx",
                RememberMe = true
            };
            var mock_Url = new Mock<IUrlHelper>();
            pageModel.Url = mock_Url.Object;
            mock_Url.Setup(x => x.Content(It.IsNotNull<string>())).Returns("/index");
            mock_PageModelRepo.Setup(x => x.Login_OnPostAsync_PasswordSignIn(
                It.IsNotNull<string>(), It.IsNotNull<string>(), It.IsNotNull<bool>()))
                .Returns(Task.FromResult<LoginData>(data));

            //Act
            var result = pageModel.OnPostAsync().Result;
            
            //Assert
            var res = Assert.IsType<LocalRedirectResult>(result);
            Assert.Equal("/index", res.Url);
        }

        [Fact]
        public void Test_OnPostAsync_RequiresTwoFactor()
        {
            //Arrange
            var data = new LoginData(){
                Succeeded = false,
                RequiresTwoFactor = true,
                IsLockedOut = false
            };
            var mock_PageModelRepo = new Mock<IPageModelRepository>();
            var pageModel = new LoginModel(mock_PageModelRepo.Object);
            pageModel.Input = new LoginModel.InputModel(){
                Email = "xxx",
                Password = "xxx",
                RememberMe = true
            };
            var mock_Url = new Mock<IUrlHelper>();
            pageModel.Url = mock_Url.Object;
            mock_Url.Setup(x => x.Content(It.IsNotNull<string>())).Returns("/index");
            mock_PageModelRepo.Setup(x => x.Login_OnPostAsync_PasswordSignIn(
                It.IsNotNull<string>(), It.IsNotNull<string>(), It.IsNotNull<bool>()))
                .Returns(Task.FromResult<LoginData>(data));

            //Act
            var result = pageModel.OnPostAsync().Result;
            
            //Assert
            var res = Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("./LoginWith2fa", res.PageName);
        }


    }
}