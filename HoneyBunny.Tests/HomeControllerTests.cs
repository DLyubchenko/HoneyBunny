using System.Linq;
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
            var categoryList = new List<Category>
            {
                new Category("Category1", "Category1", "Description1", 0){ Id = 1},
                new Category("Category2", "Category1/Category2", "Description2", 1){ Id = 2},
                new Category("Category3", "Category3", "Description3", 0){ Id = 3}
            };

            _repoMock.Setup(r => r.CategoryListAsync()).Returns(Task.FromResult(categoryList));
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController(_repoMock.Object);

            // Act
            Task<ViewResult> task = controller.Index();
            var resModel = task.Result.Model as List<Category>;

            //Assert
            Assert.IsNotNull(task);
            Assert.IsNotNull(task.Result);
            //Assert.AreEqual("Index", task.Result.ViewName);
            Assert.IsNotNull(resModel);

            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, resModel.Select(i => i.Id).ToArray());
            CollectionAssert.AreEqual(new[] { "Category1", "Category2", "Category3" }, resModel.Select(i => i.Name).ToArray());
            CollectionAssert.AreEqual(new[] { "Category1", "Category1/Category2", "Category3" }, resModel.Select(i => i.FullName).ToArray());
            CollectionAssert.AreEqual(new[] { "Description1", "Description2", "Description3" }, resModel.Select(i => i.Description).ToArray());
            CollectionAssert.AreEqual(new[] { 0, 1, 0 }, resModel.Select(i => i.ParentId).ToArray());
        }
    }
}
