using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Xunit;
using Moq;
using Galleria.Controllers;
using Galleria.Data;
using System.Collections.Generic;
using Galleria.Services;
using Microsoft.EntityFrameworkCore;
using System;

namespace Galleria.Tests
{
    public class UnitTest_ColorController
    {
        [Fact]
        public void Test_Index()
        {
            //Arange
            var list = new List<Color>(){
                new Color(){
                    ColorId = 1,
                    Description = "First"
                },
                new Color(){
                    ColorId = 2,
                    Description = "Second"
                }
            };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Color_Index().Result).Returns(list);
            var controller = new ColorController(mock_repo.Object);

            //Act
            var result = controller.Index().Result;

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Test_Details()
        {
            //Arange
            var id = 1;
            var col = new Color(){
                    ColorId = 1,
                    Description = "First"
                };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Color_Details(id).Result).Returns(col);
            var controller = new ColorController(mock_repo.Object);

            //Act
            var result = controller.Details(id).Result;

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Test_Details_IdNull()
        {
            //Arange
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            var controller = new ColorController(mock_repo.Object);

            //Act
            var result = controller.Details(null).Result;

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Test_Details_IdNotNull_ColorNull()
        {
            //Arange
            var id = 1;
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Color_Details(id)).ReturnsAsync((Color) null);
            var controller = new ColorController(mock_repo.Object);

            //Act
            var result = controller.Details(id).Result;

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Test_Create()
        {
            //Arange
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            var controller = new ColorController(mock_repo.Object);

            //Act
            var result = controller.Create();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Test_CreatePost()
        {
            //Arange
            var col = new Color(){
                    ColorId = 1,
                    Description = "First"
                    };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Color_Create(col).Result).Returns(It.IsAny<int>());
            var controller = new ColorController(mock_repo.Object);

            //Act
            var result = controller.Create(col).Result;

            //Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void Test_CreatePost_ModelStateNotValid()
        {
            //Arange
            var col = new Color(){
                    ColorId = 1,
                    Description = "First"
                    };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            var controller = new ColorController(mock_repo.Object);

            //Act
            controller.ModelState.AddModelError("key", "error");
            var result = controller.Create(col).Result;

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Test_Edit()
        {
            //Arange
            var id = 1;
            var col = new Color(){
                    ColorId = 1,
                    Description = "First"
                };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Color_Edit(id).Result).Returns(col);
            var controller = new ColorController(mock_repo.Object);

            //Act
            var result = controller.Edit(id).Result;

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Test_Edit_IdNull()
        {
            //Arange
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            var controller = new ColorController(mock_repo.Object);

            //Act
            var result = controller.Edit(null).Result;

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Test_Edit_IdNotNull_ColorNull()
        {
            //Arange
            var id = 1;
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Color_Edit(id)).ReturnsAsync((Color) null);
            var controller = new ColorController(mock_repo.Object);

            //Act
            var result = controller.Edit(id).Result;

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Test_EditPost()
        {
            //Arange
            var id = 1;
            var col = new Color(){
                    ColorId = 1,
                    Description = "First"
                };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Color_Edit(col).Result).Returns(It.IsAny<int>());
            var controller = new ColorController(mock_repo.Object);

            //Act
            var result = controller.Edit(id, col).Result;

            //Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void Test_EditPost_IdNotEqual()
        {
            //Arange
            var id = 2;
            var col = new Color(){
                    ColorId = 1,
                    Description = "First"
                };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            var controller = new ColorController(mock_repo.Object);

            //Act
            var result = controller.Edit(id, col).Result;

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Test_EditPost_DbUpdateConcurrencyException_colExistsFalse()
        {
            //Arange
            var id = 1;
            var col = new Color(){
                    ColorId = 1,
                    Description = "First"
                };
            var colExists = false;
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Color_Edit(col).Result).Throws(new DbUpdateConcurrencyException());
            mock_repo.Setup(x => x.Color_Exists(id)).Returns(colExists);
            var controller = new ColorController(mock_repo.Object);

            //Act
            var result = controller.Edit(id, col).Result;

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Test_EditPost_DbUpdateConcurrencyException_colExistsTrue()
        {
            //Arange
            var id = 1;
            var col = new Color(){
                    ColorId = 1,
                    Description = "First"
                };
            var colExists = true;
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Color_Edit(col).Result).Throws(new DbUpdateConcurrencyException());
            mock_repo.Setup(x => x.Color_Exists(id)).Returns(colExists);
            var controller = new ColorController(mock_repo.Object);

            //Act and Assert
            var exAgg = Assert.Throws<AggregateException>(() => controller.Edit(id, col).Result);
            var exDb = Assert.IsType<DbUpdateConcurrencyException>(exAgg.GetBaseException());
        }

        [Fact]
        public void Test_EditPost_ModelStateNotValid()
        {
            //Arange
            var id = 1;
            var col = new Color(){
                    ColorId = 1,
                    Description = "First"
                };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            var controller = new ColorController(mock_repo.Object);

            //Act
            controller.ModelState.AddModelError("somekey", "someerror");
            var result = controller.Edit(id, col).Result;

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Test_Delete()
        {
            //Arange
            var id = 1;
            var col = new Color(){
                    ColorId = 1,
                    Description = "First"
                };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Color_Delete(id).Result).Returns(col);
            var controller = new ColorController(mock_repo.Object);

            //Act
            var result = controller.Delete(id).Result;

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Test_Delete_IdNull()
        {
            //Arange
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            var controller = new ColorController(mock_repo.Object);

            //Act
            var result = controller.Delete(null).Result;

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Test_Delete_IdNotNull_ColorNull()
        {
            //Arange
            var id = 1;
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Color_Delete(id)).ReturnsAsync((Color) null);
            var controller = new ColorController(mock_repo.Object);

            //Act
            var result = controller.Delete(id).Result;

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Test_DeleteConfirmed()
        {
            //Arange
            var id = 1;
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Color_DeleteConfirmed(id).Result).Returns(It.IsAny<int>());
            var controller = new ColorController(mock_repo.Object);

            //Act
            var result = controller.DeleteConfirmed(id).Result;

            //Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }
    }
}