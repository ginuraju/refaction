using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using refactor_me.Controllers;
using refactor_me.Services;

namespace refactor_me.Tests
{
    [TestClass]
    public class TestProductController
    {
        private readonly ProductController _productController;
        private readonly Mock<IProductService> _productService;

        public TestProductController()
        {
            _productService = new Mock<IProductService>();
            _productService.Setup(p => p.GetAllProductsAsync()).Returns(() => Task.Run(() => GetTestProducts()));
            _productService.Setup(p => p.GetAllProductOptionsAsync(It.IsAny<Guid>()))
                .Returns(() => Task.Run(() => GetTestProductOptions()));
            _productController = new ProductController(_productService.Object);

            SetupControllerForTests(_productController);
        }

        public static void SetupControllerForTests(ApiController controller)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/products");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary {{"controller", "products"}});
            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }

        [TestMethod]
        public void GetAllProductsAsync_ShouldReturnHttpNotFoundIfNoProducts()
        {
            _productService.Setup(p => p.GetAllProductsAsync()).Returns(() => Task.Run(() => GetEmptyTestProducts()));
            var result =
                _productController.GetAllProducts().GetAwaiter().GetResult().ExecuteAsync(new CancellationToken(true));
            Assert.AreEqual(result.Result.StatusCode, HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void GetAllProductsAsync_ShouldReturnHttpOkIfProductsExists()
        {
            _productService.Setup(p => p.GetAllProductsAsync()).Returns(() => Task.Run(() => GetTestProducts()));
            var result =
                _productController.GetAllProducts().GetAwaiter().GetResult().ExecuteAsync(new CancellationToken(true));
            Assert.AreEqual(result.Result.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void GetAllProductsAsync_ShouldReturnAllProducts()
        {
            var testProducts = GetTestProducts();
            var result =
                _productController.GetAllProducts().GetAwaiter().GetResult().ExecuteAsync(new CancellationToken(true));
            var resultList = JsonConvert.DeserializeObject<Item>(result.Result.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(testProducts.Count, resultList.Items.Count);
        }

        [TestMethod]
        public void GetAllProductOptionsAsync_ShouldReturnHttpNotFoundIfNoProducts()
        {
            _productService.Setup(p => p.GetAllProductOptionsAsync(new Guid()))
                .Returns(() => Task.Run(() => GetEmptyTestProductOptions()));
            var result =
                _productController.GetProductOptions(new Guid())
                    .GetAwaiter()
                    .GetResult()
                    .ExecuteAsync(new CancellationToken(true));
            Assert.AreEqual(result.Result.StatusCode, HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void GetAllProductOptionsAsync_ShouldReturnHttpOkIfProductsExists()
        {
            _productService.Setup(p => p.GetAllProductOptionsAsync(new Guid()))
                .Returns(() => Task.Run(() => GetTestProductOptions()));
            var result =
                _productController.GetProductOptions(new Guid())
                    .GetAwaiter()
                    .GetResult()
                    .ExecuteAsync(new CancellationToken(true));
            Assert.AreEqual(result.Result.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void GetAllProductOptionsAsync_ShouldReturnAllProductOptions()
        {
            var testProducts = GetTestProductOptions();
            var result =
                _productController.GetProductOptions(new Guid())
                    .GetAwaiter()
                    .GetResult()
                    .ExecuteAsync(new CancellationToken(true));
            var resultList = JsonConvert.DeserializeObject<Item>(result.Result.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(testProducts.Count, resultList.Items.Count);
        }

        private static IList<Product> GetTestProducts()
        {
            var products = new List<Product>
            {
                new Product {Id = new Guid(), Name = "Demo1", Price = 1},
                new Product {Id = new Guid(), Name = "Demo2", Price = 2},
                new Product {Id = new Guid(), Name = "Demo3", Price = 3}
            };
            return products;
        }

        private static IList<Product> GetEmptyTestProducts()
        {
            var products = new List<Product>();
            return products;
        }

        private static IList<ProductOption> GetTestProductOptions()
        {
            var products = new List<ProductOption>
            {
                new ProductOption {Id = new Guid(), Name = "Demo1", Description = "1"},
                new ProductOption {Id = new Guid(), Name = "Demo2", Description = "2"},
                new ProductOption {Id = new Guid(), Name = "Demo3", Description = "3"}
            };
            return products;
        }

        private static IList<ProductOption> GetEmptyTestProductOptions()
        {
            var products = new List<ProductOption>();
            return products;
        }
    }

    public class Item
    {
        public List<Product> Items { get; set; }
    }
}