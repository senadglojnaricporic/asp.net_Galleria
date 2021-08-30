using Xunit;
using Galleria.Areas.Identity.Pages.Account.Manage;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Galleria.Tests
{
    public class UnitTest_ManageDataModel
    {
        [Fact]
        public void Test_OnGet()
        {
            //Arrange
            var pageModel = new ManageDataModel();

            //Act
            var result = pageModel.OnGet();

            //Assert
            var pageResult = Assert.IsType<PageResult>(result);

        }
    }
}