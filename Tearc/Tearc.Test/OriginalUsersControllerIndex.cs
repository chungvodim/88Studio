//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Tearc.Service;
//using Tearc.Data;
//using System.Collections.Generic;
//using Tearc.Repository;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using Tearc.Web.Controllers;

//namespace Tearc.Test
//{
//    [TestClass]
//    public class OriginalUsersControllerIndex
//    {
//        private readonly ApplicationDbContext _dbContext;

//        public OriginalUsersControllerIndex()
//        {
//            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
//            optionsBuilder.UseInMemoryDatabase();
//            _dbContext = new ApplicationDbContext(optionsBuilder.Options);

//            // add sample data
//            _dbContext.ApplicationUsers.Add(new ApplicationUser() { Name = "User 1" });
//            _dbContext.ApplicationUsers.Add(new ApplicationUser() { Name = "User 2" });
//            _dbContext.ApplicationUsers.Add(new ApplicationUser() { Name = "User 3" });
//            _dbContext.SaveChanges();
//        }

//        [TestMethod]
//        public void ReturnsDinnersInViewModel()
//        {
//            var controller = new UserController(_dbContext);

//            var result = controller.Index() as ViewResult;
//            var viewModel = (result.ViewData.Model as IEnumerable<ApplicationUser>).ToList();

//            Assert.AreEqual(1, viewModel.Count(d => d.Title == "User 1"));
//            Assert.AreEqual(3, viewModel.Count);
//        }
//    }
//}
