using Xunit;
using Moq;
using Galleria.Areas.Identity.Pages.Account.Manage;
using Galleria.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Galleria.Tests
{
    public class UnitTest_UploadImageModel
    {
        [Fact]
        public void Test_OnGet()
        {
            //Arrange
            var data = new UploadData()
            {
                categories = null,
                colors = null
            };
            var mock_pageModelRepo = new Mock<IPageModelRepository>();
            mock_pageModelRepo.Setup(x => x.UploadImage_OnGetAsync().Result).Returns(data);
            var pageModel = new UploadImageModel(mock_pageModelRepo.Object);

            //Act
            var result = pageModel.OnGetAsync().Result;

            //Assert
            var pageResult = Assert.IsType<PageResult>(result);

        }

        [Fact]
        public void Test_OnPostUploadAsync()
        {
            //Arrange
            var data = new UploadData()
            {
                categories = null,
                colors = null,
                isModelValid = true,
                isFileValid = true,
                isScanValid = true
            };
            var mock_pageModelRepo = new Mock<IPageModelRepository>();
            mock_pageModelRepo.Setup(
                x => x.UploadImage_OnPostUploadAsync(true, It.IsAny<UploadImageModel.ImageUploadForm>(), It.IsAny<ClaimsPrincipal>()).Result).Returns(data);
            var pageModel = new UploadImageModel(mock_pageModelRepo.Object);

            //Act
            var result = pageModel.OnPostUploadAsync().Result;

            //Assert
            var pageResult = Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("UploadImage", pageResult.PageName);
        }

        [Fact]
        public void Test_OnPostUploadAsync_ScanNotValid()
        {
            //Arrange
            var data = new UploadData()
            {
                categories = null,
                colors = null,
                isModelValid = true,
                isFileValid = true,
                isScanValid = false
            };
            var mock_pageModelRepo = new Mock<IPageModelRepository>();
            mock_pageModelRepo.Setup(
                x => x.UploadImage_OnPostUploadAsync(true, It.IsAny<UploadImageModel.ImageUploadForm>(), It.IsAny<ClaimsPrincipal>()).Result).Returns(data);
            var pageModel = new UploadImageModel(mock_pageModelRepo.Object);

            //Act
            var result = pageModel.OnPostUploadAsync().Result;

            //Assert
            var pageResult = Assert.IsType<PageResult>(result);
        }

        [Fact]
        public void Test_OnPostUploadAsync_FileNotValid()
        {
            //Arrange
            var data = new UploadData()
            {
                categories = null,
                colors = null,
                isModelValid = true,
                isFileValid = false,
                isScanValid = false
            };
            var mock_pageModelRepo = new Mock<IPageModelRepository>();
            mock_pageModelRepo.Setup(
                x => x.UploadImage_OnPostUploadAsync(true, It.IsAny<UploadImageModel.ImageUploadForm>(), It.IsAny<ClaimsPrincipal>()).Result).Returns(data);
            var pageModel = new UploadImageModel(mock_pageModelRepo.Object);

            //Act
            var result = pageModel.OnPostUploadAsync().Result;

            //Assert
            var pageResult = Assert.IsType<PageResult>(result);
        }

        [Fact]
        public void Test_OnPostUploadAsync_ModelNotValid()
        {
            //Arrange
            var data = new UploadData()
            {
                categories = null,
                colors = null,
                isModelValid = false,
                isFileValid = false,
                isScanValid = false
            };
            var mock_pageModelRepo = new Mock<IPageModelRepository>();
            mock_pageModelRepo.Setup(
                x => x.UploadImage_OnPostUploadAsync(true, It.IsAny<UploadImageModel.ImageUploadForm>(), It.IsAny<ClaimsPrincipal>()).Result).Returns(data);
            var pageModel = new UploadImageModel(mock_pageModelRepo.Object);

            //Act
            var result = pageModel.OnPostUploadAsync().Result;

            //Assert
            var pageResult = Assert.IsType<PageResult>(result);
        }
    }
}