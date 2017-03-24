//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Tearc.Service;
//using Moq;
//using Tearc.Data;
//using System.Collections.Generic;
//using Microsoft.AspNetCore.Mvc;

//namespace Tearc.Test
//{
//    [TestClass]
//    public class UsersControllerIndex
//    {
//        private readonly Mock<IUserService> _mockRepository;

//        public UsersControllerIndex()
//        {
//            _mockRepository = new Mock<IUserService>();
//        }

//        private List<ApplicationUser> GetTestUserCollection()
//        {
//            return new List<ApplicationUser>()
//            {
//              new ApplicationUser() {Name = "User 1" },
//              new ApplicationUser() {Name = "User 2" },
//            };
//        }
//        [TestMethod]
//        public void ReturnUsersInViewModel()
//        {
//            var mockRepository = new Mock<IUserService>();
//            mockRepository.Setup(r => r.List()).Returns(GetTestUserCollection());
//            var controller = new UserController(mockRepository.Object, null);
//            var result = controller.Index();
//            var viewResult = Assert.IsInstanceOfType<ViewResult>(result);
//            var viewModel = Assert.IsInstanceOfType<IEnumerable<ApplicationUser>>(
//              viewResult.ViewData.Model).ToList();
//            Assert.AreEqual("User 1", viewModel.First().Name);
//            Assert.AreEqual(2, viewModel.Count);
//        }
//    }
//}
