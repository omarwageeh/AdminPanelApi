
namespace AmazonClone.Test.ControllerTests
{
    using AmazonClone.Api.Controllers;
    using AmazonClone.Dto;
    using AmazonClone.Model;
    using AmazonClone.Service;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.CodeAnalysis;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Xunit;

    public class ProductsControllerTests
    {
        [Fact]
        public async Task GetProducts_ReturnsOkWithProducts()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();
            var categoryId = Guid.NewGuid();
            productServiceMock.Setup(ps => ps.GetProducts(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(new List<Product> {  new Product
                    {
                        Id = Guid.NewGuid(),
                        NameAr = "NameAr",
                        NameEn = "NameEn",
                        StockQuantity = 2,
                        UnitPrice = 12,
                        IsDeleted = false,
                        CreatedOn = DateTime.Now,
                        CategoryId = categoryId,
                        Category = new Category("Cat") { Id = categoryId},
                    }});

            var controller = new ProductsController(productServiceMock.Object);

            // Act
            var result = await controller.GetProducts(null) as ActionResult<IEnumerable<ProductDto>>;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
            var products = (result.Result as OkObjectResult)?.Value as IEnumerable<ProductDto>;
            Assert.NotNull(products);
            Assert.NotEmpty(products);
        }

        [Fact]
        public async Task GetProduct_ReturnsOkWithProduct()
        {
            // Arrange
            var productId = Guid.NewGuid(); // Replace with an existing product ID in your database
            var categoryId = Guid.NewGuid();
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(ps => ps.GetProducts(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(new List<Product> {
                    new Product
                    {
                        Id = productId,
                        NameAr = "NameAr",
                        NameEn = "NameEn",
                        StockQuantity = 2,
                        UnitPrice = 12,
                        IsDeleted = false,
                        CreatedOn = DateTime.Now,
                        CategoryId = categoryId,
                        Category = new Category("Cat") { Id = categoryId},
                    }}
                );

            var controller = new ProductsController(productServiceMock.Object);

            // Act
            var result = await controller.GetProduct(productId) as ActionResult<ProductDto>;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var product = (result.Result as OkObjectResult)?.Value as ProductDto;
            Assert.NotNull(product);
            Assert.Equal(productId, product.Id);
        }

        [Fact]
        public async Task PostProduct_ReturnsCreatedAtAction()
        {
            // Arrange
            var product = new Product { Id = new Guid(), NameAr = "NameAr", NameEn = "NameEn", StockQuantity = 2, UnitPrice = 12, IsDeleted = false, CreatedOn = DateTime.Now }; // Replace with an existing product ID in your database
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(ps => ps.AddProduct(It.IsAny<Product>()));

            var controller = new ProductsController(productServiceMock.Object);

            // Act
            var result = await controller.PostProduct(product) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("GetProduct", result.ActionName);
            Assert.NotNull(result.RouteValues);
            Assert.True(result.RouteValues.ContainsKey("id"));
            Assert.Equal(result.RouteValues.GetValueOrDefault("id"), product.Id);
            var prod = result?.Value as Product;
            Assert.NotNull(prod);
            Assert.Equal(prod, product);

        }

        [Fact]
        public async Task PutProduct_ReturnsNoContentOnSuccessfulUpdate()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(ps => ps.UpdateProduct(It.IsAny<Product>()));

            var controller = new ProductsController(productServiceMock.Object);

            var existingProductId = Guid.NewGuid(); // Replace with an existing product ID
            var updatedProduct = new Product { Id = existingProductId, NameAr = "NameAr", NameEn = "NameEn", StockQuantity = 2, UnitPrice = 12, IsDeleted = false, CreatedOn = DateTime.Now };

            // Act
            var result = await controller.PutProduct(existingProductId, updatedProduct) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async Task PutProduct_ReturnsBadRequestOnFailure()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(ps => ps.UpdateProduct(It.IsAny<Product>()));

            var controller = new ProductsController(productServiceMock.Object);

            var existingProductId = Guid.NewGuid(); // Replace with an existing product ID
            var updatedProduct = new Product
            {
                Id = Guid.NewGuid(),
                NameAr = "NameAr",
                NameEn = "NameEn",
                StockQuantity = 2,
                UnitPrice = 12,
                IsDeleted = false,
                CreatedOn = DateTime.Now
            };

            // Act
            var result = await controller.PutProduct(existingProductId, updatedProduct) as BadRequestResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }
        //TODO
        // Add Unit Test for DeleteMethod


    }
}
