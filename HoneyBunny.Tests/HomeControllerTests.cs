using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using HoneyBunny.Controllers;
using HoneyBunny.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HoneyBunny.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        Mock<IRepository> _repoMock = new Mock<IRepository>();

        [TestInitialize]
        public void Init()
        {
            var categoryList = new List<Category>();

            _repoMock.Setup(r => r.CategoryListAsync()).Returns(Task.FromResult(categoryList));
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController(_repoMock.Object);

            // Act
            var result = controller.Index();
            var viewResult = result as Task<ActionResult>;
            //var resModel = viewResult.Model as IEnumerable<Category>;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(viewResult);
            //Assert.AreEqual("Index", viewResult.ViewName);
            //Assert.IsNotNull(resModel);
            //CollectionAssert.AreEqual(new[] { "Toys", "Planes" }, resModel.Select(i => i.Name).ToArray());
        }
    }
}
