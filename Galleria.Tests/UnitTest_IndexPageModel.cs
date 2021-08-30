using Xunit;
using Moq;
using Microsoft.AspNetCore.Identity;
using Galleria.Interfaces;
using Galleria.Areas.Identity.Pages.Account.Manage;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace Galleria.Tests
{
    public class UnitTest_IndexPageModel
    {
        [Fact]
        public void Test_OnGetAsync()
        {
            //Arrange
            var indexData = new IndexData()
            {
                userName = "someName",
                phoneNumber = "1234-5678"
            };
            var IdUser = new IdentityUser();
            var mock_PageModelRepo = new Mock<IPageModelRepository>();
            mock_PageModelRepo.Setup(x => x.Index_LoadAsync(IdUser).Result).Returns(indexData);
            mock_PageModelRepo.Setup(x => x.Index_OnGetAsync(null).Result).Returns(IdUser);
            var pageModel = new IndexModel(mock_PageModelRepo.Object);

            //Act
            var result = pageModel.OnGetAsync().Result;

            //Assert
            var pageResult = Assert.IsType<PageResult>(result);

        }

        [Fact]
        public void Test_OnGetAsync_UserNull()
        {
            //Arrange
            var indexData = new IndexData()
            {
                userName = "someName",
                phoneNumber = "1234-5678"
            };
            var IdUser = new IdentityUser();
            var mock_PageModelRepo = new Mock<IPageModelRepository>();
            mock_PageModelRepo.Setup(x => x.Index_LoadAsync(IdUser).Result).Returns(indexData);
            mock_PageModelRepo.Setup(x => x.Index_OnGetAsync(null).Result).Returns((IdentityUser)null);
            var pageModel = new IndexModel(mock_PageModelRepo.Object);

            //Act
            var result = pageModel.OnGetAsync().Result;

            //Assert
            var pageResult = Assert.IsType<NotFoundObjectResult>(result);

        }

        [Fact]
        public void Test_OnPostAsync()
        {
            //Arrange
            var IdUser = new IdentityUser();
            var mock_PageModelRepo = new Mock<IPageModelRepository>();
            var pageModel = new IndexModel(mock_PageModelRepo.Object);
            pageModel.Input = new IndexModel.InputModel(){ PhoneNumber = "1234-5678" };
            mock_PageModelRepo.Setup(x => x.Index_OnPostAsync(null).Result).Returns(IdUser);
            mock_PageModelRepo.Setup(x => x.Index_OnPostAsync_GetPhoneNumber(IdUser).Result).Returns("1234-5678");

            //Act
            var result = pageModel.OnPostAsync().Result;

            //Assert
            var pageResult = Assert.IsType<RedirectToPageResult>(result);

        }

        [Fact]
        public void Test_OnPostAsync_UserNull()
        {
            //Arrange
            var IdUser = new IdentityUser();
            var mock_PageModelRepo = new Mock<IPageModelRepository>();
            var pageModel = new IndexModel(mock_PageModelRepo.Object);
            mock_PageModelRepo.Setup(x => x.Index_OnPostAsync(null).Result).Returns((IdentityUser)null);

            //Act
            var result = pageModel.OnPostAsync().Result;

            //Assert
            var pageResult = Assert.IsType<NotFoundObjectResult>(result);

        }

        [Fact]
        public void Test_OnPostAsync_ModelStateNotValid()
        {
            //Arrange
            var indexData = new IndexData()
            {
                userName = "someName",
                phoneNumber = "1234-5678"
            };
            var IdUser = new IdentityUser();
            var mock_PageModelRepo = new Mock<IPageModelRepository>();
            var pageModel = new IndexModel(mock_PageModelRepo.Object);
            mock_PageModelRepo.Setup(x => x.Index_OnPostAsync(null).Result).Returns(IdUser);
            mock_PageModelRepo.Setup(x => x.Index_LoadAsync(IdUser).Result).Returns(indexData);

            //Act
            pageModel.ModelState.AddModelError("key", "error");
            var result = pageModel.OnPostAsync().Result;

            //Assert
            var pageResult = Assert.IsType<PageResult>(result);

        }

        [Fact]
        public void Test_OnPostAsync_Phonenumber_NotSame()
        {
            //Arrange
            var IdUser = new IdentityUser();
            var mock_PageModelRepo = new Mock<IPageModelRepository>();
            var pageModel = new IndexModel(mock_PageModelRepo.Object);
            pageModel.Input = new IndexModel.InputModel(){ PhoneNumber = "1234-5678" };
            mock_PageModelRepo.Setup(x => x.Index_OnPostAsync(null).Result).Returns(IdUser);
            mock_PageModelRepo.Setup(x => x.Index_OnPostAsync_GetPhoneNumber(IdUser).Result).Returns("1234-5678-xxxx");
            mock_PageModelRepo.Setup(x => x.Index_OnPostAsync_SetPhoneNumber(IdUser, "1234-5678-xxxx").Result).Returns(false);

            //Act
            var result = pageModel.OnPostAsync().Result;

            //Assert
            var pageResult = Assert.IsType<RedirectToPageResult>(result);

        }
    }
}