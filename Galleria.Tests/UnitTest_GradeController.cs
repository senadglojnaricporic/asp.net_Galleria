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
    public class UnitTest_GradeController
    {
        [Fact]
        public void Test_Index()
        {
            //Arange
            var list = new List<Grade>(){
                new Grade(){
                    GradeId = 1,
                    GradeNum = 1
                },
                new Grade(){
                    GradeId = 2,
                    GradeNum = 2
                }
            };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Grade_Index().Result).Returns(list);
            var controller = new GradeController(mock_repo.Object);

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
            var gra = new Grade(){
                    GradeId = 1,
                    GradeNum = 1
                };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Grade_Details(id).Result).Returns(gra);
            var controller = new GradeController(mock_repo.Object);

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
            var controller = new GradeController(mock_repo.Object);

            //Act
            var result = controller.Details(null).Result;

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Test_Details_IdNotNull_GradeNull()
        {
            //Arange
            var id = 1;
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Grade_Details(id)).ReturnsAsync((Grade) null);
            var controller = new GradeController(mock_repo.Object);

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
            var controller = new GradeController(mock_repo.Object);

            //Act
            var result = controller.Create();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Test_CreatePost()
        {
            //Arange
            var gra = new Grade(){
                    GradeId = 1,
                    GradeNum = 1
                    };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Grade_Create(gra).Result).Returns(It.IsAny<int>());
            var controller = new GradeController(mock_repo.Object);

            //Act
            var result = controller.Create(gra).Result;

            //Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void Test_CreatePost_ModelStateNotValid()
        {
            //Arange
            var gra = new Grade(){
                    GradeId = 1,
                    GradeNum = 1
                    };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            var controller = new GradeController(mock_repo.Object);

            //Act
            controller.ModelState.AddModelError("key", "error");
            var result = controller.Create(gra).Result;

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Test_Edit()
        {
            //Arange
            var id = 1;
            var gra = new Grade(){
                    GradeId = 1,
                    GradeNum = 1
                };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Grade_Edit(id).Result).Returns(gra);
            var controller = new GradeController(mock_repo.Object);

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
            var controller = new GradeController(mock_repo.Object);

            //Act
            var result = controller.Edit(null).Result;

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Test_Edit_IdNotNull_GradeNull()
        {
            //Arange
            var id = 1;
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Grade_Edit(id)).ReturnsAsync((Grade) null);
            var controller = new GradeController(mock_repo.Object);

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
            var gra = new Grade(){
                    GradeId = 1,
                    GradeNum = 1
                };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Grade_Edit(gra).Result).Returns(It.IsAny<int>());
            var controller = new GradeController(mock_repo.Object);

            //Act
            var result = controller.Edit(id, gra).Result;

            //Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void Test_EditPost_IdNotEqual()
        {
            //Arange
            var id = 2;
            var gra = new Grade(){
                    GradeId = 1,
                    GradeNum = 1
                };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            var controller = new GradeController(mock_repo.Object);

            //Act
            var result = controller.Edit(id, gra).Result;

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Test_EditPost_DbUpdateConcurrencyException_graExistsFalse()
        {
            //Arange
            var id = 1;
            var gra = new Grade(){
                    GradeId = 1,
                    GradeNum = 1
                };
            var graExists = false;
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Grade_Edit(gra).Result).Throws(new DbUpdateConcurrencyException());
            mock_repo.Setup(x => x.Grade_Exists(id)).Returns(graExists);
            var controller = new GradeController(mock_repo.Object);

            //Act
            var result = controller.Edit(id, gra).Result;

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Test_EditPost_DbUpdateConcurrencyException_graExistsTrue()
        {
            //Arange
            var id = 1;
            var gra = new Grade(){
                    GradeId = 1,
                    GradeNum = 1
                };
            var graExists = true;
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Grade_Edit(gra).Result).Throws(new DbUpdateConcurrencyException());
            mock_repo.Setup(x => x.Grade_Exists(id)).Returns(graExists);
            var controller = new GradeController(mock_repo.Object);

            //Act and Assert
            var exAgg = Assert.Throws<AggregateException>(() => controller.Edit(id, gra).Result);
            var exDb = Assert.IsType<DbUpdateConcurrencyException>(exAgg.GetBaseException());
        }

        [Fact]
        public void Test_EditPost_ModelStateNotValid()
        {
            //Arange
            var id = 1;
            var gra = new Grade(){
                    GradeId = 1,
                    GradeNum = 1
                };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            var controller = new GradeController(mock_repo.Object);

            //Act
            controller.ModelState.AddModelError("somekey", "someerror");
            var result = controller.Edit(id, gra).Result;

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Test_Delete()
        {
            //Arange
            var id = 1;
            var gra = new Grade(){
                    GradeId = 1,
                    GradeNum = 1
                };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Grade_Delete(id).Result).Returns(gra);
            var controller = new GradeController(mock_repo.Object);

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
            var controller = new GradeController(mock_repo.Object);

            //Act
            var result = controller.Delete(null).Result;

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Test_Delete_IdNotNull_GradeNull()
        {
            //Arange
            var id = 1;
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Grade_Delete(id)).ReturnsAsync((Grade) null);
            var controller = new GradeController(mock_repo.Object);

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
            mock_repo.Setup(x => x.Grade_DeleteConfirmed(id).Result).Returns(It.IsAny<int>());
            var controller = new GradeController(mock_repo.Object);

            //Act
            var result = controller.DeleteConfirmed(id).Result;

            //Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }
    }
}