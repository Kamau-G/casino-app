using CasinoApp.Controllers;
using CasinoApp.Data;
using CasinoApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CasinoAppTest
{
    public class DBTests
    {
        TestCasinoDb db = new TestCasinoDb();
        [Fact]
        public void TestCreateUser()
        {
            var u = new AppUser { Name="Kamau",UserName= "Kamau" };
            var u2 = new AppUser { Name="Tlaloc", UserName = "Tlaloc" };
            //var controller = new LeaderBoardController(db);
            db.GetUsers().Add(u);
            db.GetUsers().Add(u2);
            Assert.NotEmpty(db.GetUsers());
           // Assert.IsType<ViewResult>(controller.Index(u.Name));
            var s = "NotInDataBase";
            //Assert.IsType<RedirectToActionResult>(controller.Index(s));

        }
    }
}