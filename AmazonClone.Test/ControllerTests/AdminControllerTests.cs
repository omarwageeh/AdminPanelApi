using AmazonClone.Api.Controllers;
using AmazonClone.Api.Models;
using AmazonClone.Model;
using AmazonClone.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Threading.Tasks;
using Xunit;


namespace AmazonClone.Test.ControllerTests
{
    public class AdminControllerTests
    {
        [Fact]
        public async Task Login_ReturnsOkWithToken_WhenValidUserCredentials()
        {
            // Arrange
            var configMock = new Mock<IConfiguration>();
            configMock.SetupGet(c => c["Jwt:Key"]).Returns("ByYM000OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SNM");
            configMock.SetupGet(c => c["Jwt:Issuer"]).Returns("http://localhost:7268");
            configMock.SetupGet(c => c["Jwt:Audience"]).Returns("http://localhost:7268");

            var userServiceMock = new Mock<IUserService>();
            userServiceMock
                .Setup(us => us.LoginUser(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new User() /* Replace with your actual User object */);

            var controller = new AdminController(configMock.Object, userServiceMock.Object);

            // Act
            var result = await controller.Login(new UserModel() { Email = "email", Password = "pass"}) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var token = result.Value as dynamic;
            Assert.NotNull(token);
        }
        [Fact]
        public async Task Login_ReturnsNotFound_WhenInValidUserCredentials()
        {
            // Arrange
            var configMock = new Mock<IConfiguration>();
            configMock.SetupGet(c => c["Jwt:Key"]).Returns("ByYM000OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SNM");
            configMock.SetupGet(c => c["Jwt:Issuer"]).Returns("http://localhost:7268");
            configMock.SetupGet(c => c["Jwt:Audience"]).Returns("http://localhost:7268");

            var userServiceMock = new Mock<IUserService>();
            userServiceMock
                .Setup(us => us.LoginUser(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((User)null!);

            var controller = new AdminController(configMock.Object, userServiceMock.Object);

            // Act
            var result = await controller.Login(new UserModel() { Email = "email", Password = "pass" }) as UnauthorizedResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(401, result.StatusCode);
        }
        [Fact]
        public async Task Register_ReturnsOk_WhenRegistrationSucceeds()
        {
            // Arrange
            var configMock = new Mock<IConfiguration>();
            var userServiceMock = new Mock<IUserService>();

            configMock.SetupGet(c => c["Jwt:Key"]).Returns("ByYM000OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SNM");
            configMock.SetupGet(c => c["Jwt:Issuer"]).Returns("http://localhost:7268");
            configMock.SetupGet(c => c["Jwt:Audience"]).Returns("http://localhost:7268");

            // Replace with the expected admin object after registration
            userServiceMock.Setup(us => us.Register(It.IsAny<Admin>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            var controller = new AdminController(configMock.Object, userServiceMock.Object);

            // Act
            var result = await controller.Register(new RegisterModel() { Email="email@gmail.com", FullName="fullname", IsActive=false, JobTitle="title", Password="pass@1234" }) as OkResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Register_ReturnsBadRequest_WhenRegistrationFails()
        {
            // Arrange
            var configMock = new Mock<IConfiguration>();
            var userServiceMock = new Mock<IUserService>();

            // Replace with your actual configuration values
            configMock.SetupGet(c => c["Jwt:Key"]).Returns("ByYM000OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SNM");
            configMock.SetupGet(c => c["Jwt:Issuer"]).Returns("http://localhost:7268");
            configMock.SetupGet(c => c["Jwt:Audience"]).Returns("http://localhost:7268");

            // Replace with the expected admin object after registration
            userServiceMock.Setup(us => us.Register(It.IsAny<Admin>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            var controller = new AdminController(configMock.Object, userServiceMock.Object);

            // Act
            var result = await controller.Register(new RegisterModel() { Email="email@gmail.com", FullName="fullname", IsActive=false, JobTitle="title", Password="pass@1234" }) as BadRequestResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }
    }
}
