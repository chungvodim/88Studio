//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Tearc.Data;
//using Moq;
//using Microsoft.AspNetCore.Mvc;
//using Tearc.Service;
//using Xunit;
//using Tearc.Web.Controllers;

//namespace Tearc.XTest
//{
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
//        [Fact]
//        public void ReturnUsersInViewModel()
//        {
//            var mockRepository = new Mock<IUserService>();
//            mockRepository.Setup(r => r.List()).Returns(GetTestUserCollection());
//            var controller = new UserController(mockRepository.Object, null);
//            var result = controller.Index();
//            var viewResult = Assert.IsType<ViewResult>(result);
//            var viewModel = Assert.IsType<IEnumerable<ApplicationUser>>(
//              viewResult.ViewData.Model).ToList();
//            Assert.Equal("User 1", viewModel.First().Name);
//            Assert.Equal(2, viewModel.Count);
//        }
//    }
//}
