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
    public class UnitTest_CategoryController
    {
        [Fact]
        public void Test_Index()
        {
            //Arange
            var list = new List<Category>(){
                new Category(){
                    CategoryId = 1,
                    Description = "First"
                },
                new Category(){
                    CategoryId = 2,
                    Description = "Second"
                }
            };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Category_Index().Result).Returns(list);
            var controller = new CategoryController(mock_repo.Object);

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
            var cat = new Category(){
                    CategoryId = 1,
                    Description = "First"
                };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Category_Details(id).Result).Returns(cat);
            var controller = new CategoryController(mock_repo.Object);

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
            var controller = new CategoryController(mock_repo.Object);

            //Act
            var result = controller.Details(null).Result;

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Test_Details_IdNotNull_CategoryNull()
        {
            //Arange
            var id = 1;
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Category_Details(id)).ReturnsAsync((Category) null);
            var controller = new CategoryController(mock_repo.Object);

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
            var controller = new CategoryController(mock_repo.Object);

            //Act
            var result = controller.Create();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Test_CreatePost()
        {
            //Arange
            var cat = new Category(){
                    CategoryId = 1,
                    Description = "First"
                    };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Category_Create(cat).Result).Returns(It.IsAny<int>());
            var controller = new CategoryController(mock_repo.Object);

            //Act
            var result = controller.Create(cat).Result;

            //Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void Test_CreatePost_ModelStateNotValid()
        {
            //Arange
            var cat = new Category(){
                    CategoryId = 1,
                    Description = "First"
                    };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            var controller = new CategoryController(mock_repo.Object);

            //Act
            controller.ModelState.AddModelError("key", "error");
            var result = controller.Create(cat).Result;

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Test_Edit()
        {
            //Arange
            var id = 1;
            var cat = new Category(){
                    CategoryId = 1,
                    Description = "First"
                };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Category_Edit(id).Result).Returns(cat);
            var controller = new CategoryController(mock_repo.Object);

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
            var controller = new CategoryController(mock_repo.Object);

            //Act
            var result = controller.Edit(null).Result;

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Test_Edit_IdNotNull_CategoryNull()
        {
            //Arange
            var id = 1;
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Category_Edit(id)).ReturnsAsync((Category) null);
            var controller = new CategoryController(mock_repo.Object);

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
            var cat = new Category(){
                    CategoryId = 1,
                    Description = "First"
                };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Category_Edit(cat).Result).Returns(It.IsAny<int>());
            var controller = new CategoryController(mock_repo.Object);

            //Act
            var result = controller.Edit(id, cat).Result;

            //Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void Test_EditPost_IdNotEqual()
        {
            //Arange
            var id = 2;
            var cat = new Category(){
                    CategoryId = 1,
                    Description = "First"
                };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            var controller = new CategoryController(mock_repo.Object);

            //Act
            var result = controller.Edit(id, cat).Result;

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Test_EditPost_DbUpdateConcurrencyException_catExistsFalse()
        {
            //Arange
            var id = 1;
            var cat = new Category(){
                    CategoryId = 1,
                    Description = "First"
                };
            var catExists = false;
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Category_Edit(cat).Result).Throws(new DbUpdateConcurrencyException());
            mock_repo.Setup(x => x.Category_Exists(id)).Returns(catExists);
            var controller = new CategoryController(mock_repo.Object);

            //Act
            var result = controller.Edit(id, cat).Result;

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Test_EditPost_DbUpdateConcurrencyException_catExistsTrue()
        {
            //Arange
            var id = 1;
            var cat = new Category(){
                    CategoryId = 1,
                    Description = "First"
                };
            var catExists = true;
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Category_Edit(cat).Result).Throws(new DbUpdateConcurrencyException());
            mock_repo.Setup(x => x.Category_Exists(id)).Returns(catExists);
            var controller = new CategoryController(mock_repo.Object);

            //Act and Assert
            var exAgg = Assert.Throws<AggregateException>(() => controller.Edit(id, cat).Result);
            var exDb = Assert.IsType<DbUpdateConcurrencyException>(exAgg.GetBaseException());
        }

        [Fact]
        public void Test_EditPost_ModelStateNotValid()
        {
            //Arange
            var id = 1;
            var cat = new Category(){
                    CategoryId = 1,
                    Description = "First"
                };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            var controller = new CategoryController(mock_repo.Object);

            //Act
            controller.ModelState.AddModelError("somekey", "someerror");
            var result = controller.Edit(id, cat).Result;

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Test_Delete()
        {
            //Arange
            var id = 1;
            var cat = new Category(){
                    CategoryId = 1,
                    Description = "First"
                };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Category_Delete(id).Result).Returns(cat);
            var controller = new CategoryController(mock_repo.Object);

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
            var controller = new CategoryController(mock_repo.Object);

            //Act
            var result = controller.Delete(null).Result;

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Test_Delete_IdNotNull_CategoryNull()
        {
            //Arange
            var id = 1;
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Category_Delete(id)).ReturnsAsync((Category) null);
            var controller = new CategoryController(mock_repo.Object);

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
            mock_repo.Setup(x => x.Category_DeleteConfirmed(id).Result).Returns(It.IsAny<int>());
            var controller = new CategoryController(mock_repo.Object);

            //Act
            var result = controller.DeleteConfirmed(id).Result;

            //Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }
    }
}