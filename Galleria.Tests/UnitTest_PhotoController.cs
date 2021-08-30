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
    public class UnitTest_PhotoController
    {

        class CategorySet : DbSet<Category>
        {
            public CategorySet()
            {

            }
        }

        class ColorSet : DbSet<Color>
        {
            public ColorSet()
            {

            }
        }

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
            mock_repo.Setup(x => x.Photo_Index().Result).Returns(list);
            var controller = new PhotoController(mock_repo.Object);

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
            var pho = new Photo(){
                    PhotoId = 1,
                    UserId = "First"
                };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Photo_Details(id).Result).Returns(pho);
            var controller = new PhotoController(mock_repo.Object);

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
            var controller = new PhotoController(mock_repo.Object);

            //Act
            var result = controller.Details(null).Result;

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Test_Details_IdNotNull_PhotoNull()
        {
            //Arange
            var id = 1;
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Photo_Details(id)).ReturnsAsync((Photo) null);
            var controller = new PhotoController(mock_repo.Object);

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
            mock_repo.Setup(x => x.Get_Categories()).Returns(new CategorySet());
            mock_repo.Setup(x => x.Get_Colors()).Returns(new ColorSet());
            var controller = new PhotoController(mock_repo.Object);

            //Act
            var result = controller.Create();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Test_CreatePost()
        {
            //Arange
            var pho = new Photo(){
                    PhotoId = 1,
                    UserId = "First"
                    };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Photo_Create(pho).Result).Returns(It.IsAny<int>());
            var controller = new PhotoController(mock_repo.Object);

            //Act
            var result = controller.Create(pho).Result;

            //Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void Test_CreatePost_ModelStateNotValid()
        {
            //Arange
            var pho = new Photo(){
                    PhotoId = 1,
                    UserId = "First"
                    };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Get_Categories()).Returns(new CategorySet());
            mock_repo.Setup(x => x.Get_Colors()).Returns(new ColorSet());
            var controller = new PhotoController(mock_repo.Object);

            //Act
            controller.ModelState.AddModelError("key", "error");
            var result = controller.Create(pho).Result;

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Test_Edit()
        {
            //Arange
            var id = 1;
            var pho = new Photo(){
                    PhotoId = 1,
                    UserId = "First"
                };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Photo_Edit(id).Result).Returns(pho);
            mock_repo.Setup(x => x.Get_Categories()).Returns(new CategorySet());
            mock_repo.Setup(x => x.Get_Colors()).Returns(new ColorSet());
            var controller = new PhotoController(mock_repo.Object);

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
            var controller = new PhotoController(mock_repo.Object);

            //Act
            var result = controller.Edit(null).Result;

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Test_Edit_IdNotNull_PhotoNull()
        {
            //Arange
            var id = 1;
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Photo_Edit(id)).ReturnsAsync((Photo) null);
            var controller = new PhotoController(mock_repo.Object);

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
            var pho = new Photo(){
                    PhotoId = 1,
                    UserId = "First"
                };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Photo_Edit(pho).Result).Returns(It.IsAny<int>());
            var controller = new PhotoController(mock_repo.Object);

            //Act
            var result = controller.Edit(id, pho).Result;

            //Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void Test_EditPost_IdNotEqual()
        {
            //Arange
            var id = 2;
            var pho = new Photo(){
                    PhotoId = 1,
                    UserId = "First"
                };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            var controller = new PhotoController(mock_repo.Object);

            //Act
            var result = controller.Edit(id, pho).Result;

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Test_EditPost_DbUpdateConcurrencyException_phoExistsFalse()
        {
            //Arange
            var id = 1;
            var pho = new Photo(){
                    PhotoId = 1,
                    UserId = "First"
                };
            var phoExists = false;
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Photo_Edit(pho).Result).Throws(new DbUpdateConcurrencyException());
            mock_repo.Setup(x => x.Photo_Exists(id)).Returns(phoExists);
            var controller = new PhotoController(mock_repo.Object);

            //Act
            var result = controller.Edit(id, pho).Result;

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Test_EditPost_DbUpdateConcurrencyException_phoExistsTrue()
        {
            //Arange
            var id = 1;
            var pho = new Photo(){
                    PhotoId = 1,
                    UserId = "First"
                };
            var phoExists = true;
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Photo_Edit(pho).Result).Throws(new DbUpdateConcurrencyException());
            mock_repo.Setup(x => x.Photo_Exists(id)).Returns(phoExists);
            var controller = new PhotoController(mock_repo.Object);

            //Act and Assert
            var exAgg = Assert.Throws<AggregateException>(() => controller.Edit(id, pho).Result);
            var exDb = Assert.IsType<DbUpdateConcurrencyException>(exAgg.GetBaseException());
        }

        [Fact]
        public void Test_EditPost_ModelStateNotValid()
        {
            //Arange
            var id = 1;
            var pho = new Photo(){
                    PhotoId = 1,
                    UserId = "First"
                };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Get_Categories()).Returns(new CategorySet());
            mock_repo.Setup(x => x.Get_Colors()).Returns(new ColorSet());
            var controller = new PhotoController(mock_repo.Object);

            //Act
            controller.ModelState.AddModelError("somekey", "someerror");
            var result = controller.Edit(id, pho).Result;

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Test_Delete()
        {
            //Arange
            var id = 1;
            var pho = new Photo(){
                    PhotoId = 1,
                    UserId = "First"
                };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Photo_Delete(id).Result).Returns(pho);
            var controller = new PhotoController(mock_repo.Object);

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
            var controller = new PhotoController(mock_repo.Object);

            //Act
            var result = controller.Delete(null).Result;

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Test_Delete_IdNotNull_PhotoNull()
        {
            //Arange
            var id = 1;
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Photo_Delete(id)).ReturnsAsync((Photo) null);
            var controller = new PhotoController(mock_repo.Object);

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
            mock_repo.Setup(x => x.Photo_DeleteConfirmed(id).Result).Returns(It.IsAny<int>());
            var controller = new PhotoController(mock_repo.Object);

            //Act
            var result = controller.DeleteConfirmed(id).Result;

            //Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }
    }
}