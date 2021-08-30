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
    public class UnitTest_ReviewController
    {

        class GradeSet : DbSet<Grade>
        {
            public GradeSet()
            {

            }
        }

        class PhotoSet : DbSet<Photo>
        {
            public PhotoSet()
            {

            }
        }

        [Fact]
        public void Test_Index()
        {
            //Arange
            var list = new List<Review>(){
                new Review(){
                    UserId = "First",
                    PhotoId = 1
                },
                new Review(){
                    UserId = "Second",
                    PhotoId = 2
                }
            };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Review_Index().Result).Returns(list);
            var controller = new ReviewController(mock_repo.Object);

            //Act
            var result = controller.Index().Result;

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Test_Details()
        {
            //Arange
            var id = "First";
            var rev = new Review(){
                    UserId = "First",
                    PhotoId = 1
                };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Review_Details(id).Result).Returns(rev);
            var controller = new ReviewController(mock_repo.Object);

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
            var controller = new ReviewController(mock_repo.Object);

            //Act
            var result = controller.Details(null).Result;

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Test_Details_IdNotNull_ReviewNull()
        {
            //Arange
            var id = "First";
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Review_Details(id)).ReturnsAsync((Review) null);
            var controller = new ReviewController(mock_repo.Object);

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
            mock_repo.Setup(x => x.Get_Grades()).Returns(new GradeSet());
            mock_repo.Setup(x => x.Get_Photos()).Returns(new PhotoSet());
            var controller = new ReviewController(mock_repo.Object);

            //Act
            var result = controller.Create();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Test_CreatePost()
        {
            //Arange
            var rev = new Review(){
                    UserId = "First",
                    PhotoId = 1
                };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Review_Create(rev).Result).Returns(It.IsAny<int>());
            var controller = new ReviewController(mock_repo.Object);

            //Act
            var result = controller.Create(rev).Result;

            //Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void Test_CreatePost_ModelStateNotValid()
        {
            //Arange
            var rev = new Review(){
                    UserId = "First",
                    PhotoId = 1
                };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Get_Grades()).Returns(new GradeSet());
            mock_repo.Setup(x => x.Get_Photos()).Returns(new PhotoSet());
            var controller = new ReviewController(mock_repo.Object);

            //Act
            controller.ModelState.AddModelError("key", "error");
            var result = controller.Create(rev).Result;

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Test_Edit()
        {
            //Arange
            var id = "First";
            var rev = new Review(){
                    UserId = "First",
                    PhotoId = 1
                };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Review_Edit(id).Result).Returns(rev);
            mock_repo.Setup(x => x.Get_Grades()).Returns(new GradeSet());
            mock_repo.Setup(x => x.Get_Photos()).Returns(new PhotoSet());
            var controller = new ReviewController(mock_repo.Object);

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
            var controller = new ReviewController(mock_repo.Object);

            //Act
            var result = controller.Edit(null).Result;

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Test_Edit_IdNotNull_ReviewNull()
        {
            //Arange
            var id = "First";
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Review_Edit(id)).ReturnsAsync((Review) null);
            var controller = new ReviewController(mock_repo.Object);

            //Act
            var result = controller.Edit(id).Result;

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Test_EditPost()
        {
            //Arange
            var id = "First";
            var rev = new Review(){
                    UserId = "First",
                    PhotoId = 1
                };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Review_Edit(rev).Result).Returns(It.IsAny<int>());
            var controller = new ReviewController(mock_repo.Object);

            //Act
            var result = controller.Edit(id, rev).Result;

            //Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void Test_EditPost_IdNotEqual()
        {
            //Arange
            var id = "Second";
            var rev = new Review(){
                    UserId = "First",
                    PhotoId = 1
                };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            var controller = new ReviewController(mock_repo.Object);

            //Act
            var result = controller.Edit(id, rev).Result;

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Test_EditPost_DbUpdateConcurrencyException_revExistsFalse()
        {
            //Arange
            var id = "First";
            var rev = new Review(){
                    UserId = "First",
                    PhotoId = 1
                };
            var revExists = false;
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Review_Edit(rev).Result).Throws(new DbUpdateConcurrencyException());
            mock_repo.Setup(x => x.Review_Exists(id)).Returns(revExists);
            var controller = new ReviewController(mock_repo.Object);

            //Act
            var result = controller.Edit(id, rev).Result;

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Test_EditPost_DbUpdateConcurrencyException_revExistsTrue()
        {
            //Arange
            var id = "First";
            var rev = new Review(){
                    UserId = "First",
                    PhotoId = 1
                };
            var revExists = true;
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Review_Edit(rev).Result).Throws(new DbUpdateConcurrencyException());
            mock_repo.Setup(x => x.Review_Exists(id)).Returns(revExists);
            var controller = new ReviewController(mock_repo.Object);

            //Act and Assert
            var exAgg = Assert.Throws<AggregateException>(() => controller.Edit(id, rev).Result);
            var exDb = Assert.IsType<DbUpdateConcurrencyException>(exAgg.GetBaseException());
        }

        [Fact]
        public void Test_EditPost_ModelStateNotValid()
        {
            //Arange
            var id = "First";
            var rev = new Review(){
                    UserId = "First",
                    PhotoId = 1
                };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Get_Grades()).Returns(new GradeSet());
            mock_repo.Setup(x => x.Get_Photos()).Returns(new PhotoSet());
            var controller = new ReviewController(mock_repo.Object);

            //Act
            controller.ModelState.AddModelError("somekey", "someerror");
            var result = controller.Edit(id, rev).Result;

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Test_Delete()
        {
            //Arange
            var id = "First";
            var rev = new Review(){
                    UserId = "First",
                    PhotoId = 1
                };
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Review_Delete(id).Result).Returns(rev);
            var controller = new ReviewController(mock_repo.Object);

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
            var controller = new ReviewController(mock_repo.Object);

            //Act
            var result = controller.Delete(null).Result;

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Test_Delete_IdNotNull_ReviewNull()
        {
            //Arange
            var id = "First";
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Review_Delete(id)).ReturnsAsync((Review) null);
            var controller = new ReviewController(mock_repo.Object);

            //Act
            var result = controller.Delete(id).Result;

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Test_DeleteConfirmed()
        {
            //Arange
            var id = "First";
            var mock_conf = new Mock<IConfiguration>();
            var mock_context = new Mock<GalleriaContext>(mock_conf.Object);
            var mock_appDBcontext = new Mock<ApplicationDbContext>(mock_conf.Object);
            var mock_repo = new Mock<ControllerRepository>(mock_context.Object, mock_appDBcontext.Object);
            mock_repo.Setup(x => x.Review_DeleteConfirmed(id).Result).Returns(It.IsAny<int>());
            var controller = new ReviewController(mock_repo.Object);

            //Act
            var result = controller.DeleteConfirmed(id).Result;

            //Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }
    }
}