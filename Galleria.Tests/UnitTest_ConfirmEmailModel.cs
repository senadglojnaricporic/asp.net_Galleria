using Xunit;
using Galleria.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using Galleria.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Galleria.Tests
{
    public class UnitTest_ConfirmEmailModel
    {
        [Fact]
        public void Test_OnGetAsync()
        {
            //Arrange
            var userId = "xxx";
            var code = "xxx";
            var statusMessage = "xxx";
            var user = new IdentityUser();
            var mock_PageModelRepo = new Mock<IPageModelRepository>();
            mock_PageModelRepo.Setup(x => x.ConfirmEmail_OnGetAsync_FindById(It.IsAny<string>()).Result)
                                    .Returns(user);
            mock_PageModelRepo.Setup(x => x.ConfirmEmail_OnGetAsync_StatusMessage(It.IsAny<IdentityUser>(), It.IsAny<string>()).Result)
                                    .Returns(statusMessage);
            var pageModel = new ConfirmEmailModel(mock_PageModelRepo.Object);

            //Act
            var result = pageModel.OnGetAsync(userId, code).Result;

            //Assert
            var pageResult = Assert.IsType<PageResult>(result);

        }

        [Fact]
        public void Test_OnGetAsync_UserIdNull()
        {
            //Arrange
            var userId = "xxx";
            userId = null;
            var code = "xxx";
            var statusMessage = "xxx";
            var user = new IdentityUser();
            var mock_PageModelRepo = new Mock<IPageModelRepository>();
            mock_PageModelRepo.Setup(x => x.ConfirmEmail_OnGetAsync_FindById(It.IsAny<string>()).Result)
                                    .Returns(user);
            mock_PageModelRepo.Setup(x => x.ConfirmEmail_OnGetAsync_StatusMessage(It.IsAny<IdentityUser>(), It.IsAny<string>()).Result)
                                    .Returns(statusMessage);
            var pageModel = new ConfirmEmailModel(mock_PageModelRepo.Object);

            //Act
            var result = pageModel.OnGetAsync(userId, code).Result;

            //Assert
            var pageResult = Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("/Index", pageResult.PageName);
        }

        [Fact]
        public void Test_OnGetAsync_CodeNull()
        {
            //Arrange
            var userId = "xxx";
            var code = "xxx";
            code = null;
            var statusMessage = "xxx";
            var user = new IdentityUser();
            var mock_PageModelRepo = new Mock<IPageModelRepository>();
            mock_PageModelRepo.Setup(x => x.ConfirmEmail_OnGetAsync_FindById(It.IsAny<string>()).Result)
                                    .Returns(user);
            mock_PageModelRepo.Setup(x => x.ConfirmEmail_OnGetAsync_StatusMessage(It.IsAny<IdentityUser>(), It.IsAny<string>()).Result)
                                    .Returns(statusMessage);
            var pageModel = new ConfirmEmailModel(mock_PageModelRepo.Object);

            //Act
            var result = pageModel.OnGetAsync(userId, code).Result;

            //Assert
            var pageResult = Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("/Index", pageResult.PageName);
        }

        [Fact]
        public void Test_OnGetAsync_UserNull()
        {
            //Arrange
            var userId = "xxx";
            var code = "xxx";
            var statusMessage = "xxx";
            var mock_PageModelRepo = new Mock<IPageModelRepository>();
            mock_PageModelRepo.Setup(x => x.ConfirmEmail_OnGetAsync_FindById(It.IsAny<string>()).Result)
                                    .Returns((IdentityUser)null);
            mock_PageModelRepo.Setup(x => x.ConfirmEmail_OnGetAsync_StatusMessage(It.IsAny<IdentityUser>(), It.IsAny<string>()).Result)
                                    .Returns(statusMessage);
            var pageModel = new ConfirmEmailModel(mock_PageModelRepo.Object);

            //Act
            var result = pageModel.OnGetAsync(userId, code).Result;

            //Assert
            var pageResult = Assert.IsType<NotFoundObjectResult>(result);

        }
    }
}