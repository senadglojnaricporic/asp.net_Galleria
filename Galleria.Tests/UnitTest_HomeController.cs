using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Xunit;
using Moq;
using Galleria.Controllers;
using Galleria.Data;
using System.Collections.Generic;
using Galleria.Services;

namespace Galleria.Tests
{
    public class UnitTest_HomeController
    {
        [Fact]
        public void Test_Index()
        {
            //Arange
            var list = new List<Photo>(){
                new Photo(){
                    PhotoId = 1,
                    UserId = "First"
                },
                new Photo(){
                    PhotoId = 2,
                    UserId = "Second"
                }
            };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Home_Index().Result).Returns(list);
            var controller = new HomeController(mock_repo.Object);

            //Act
            var result = controller.Index().Result;

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Test_Privacy()
        {
            //Arange
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            var controller = new HomeController(mock_repo.Object);

            //Act
            var result = controller.Privacy();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Test_About()
        {
            //Arange
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            var controller = new HomeController(mock_repo.Object);

            //Act
            var result = controller.About();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Test_Gallery()
        {
            //Arange
            var list = new List<Photo>(){
                new Photo(){
                    PhotoId = 1,
                    UserId = "First"
                },
                new Photo(){
                    PhotoId = 2,
                    UserId = "Second"
                }
            };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Home_Gallery().Result).Returns(list);
            var controller = new HomeController(mock_repo.Object);

            //Act
            var result = controller.Gallery().Result;

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Test_Review()
        {
            //Arange
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Home_Review(It.IsAny<int>(), It.IsAny<bool>())).Returns(It.IsAny<ReviewData>());
            var controller = new HomeController(mock_repo.Object);

            //Act
            var result = controller.Review(It.IsAny<int>(), It.IsAny<bool>());

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Test_SendReview()
        {
            //Arange
            var rd = new ReviewData();
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Home_SendReview(rd, It.IsAny<string>()).Result).Returns(It.IsAny<bool>());
            var controller = new HomeController(mock_repo.Object);

            //Act
            var result = controller.SendReview(rd).Result;

            //Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }
    }
}