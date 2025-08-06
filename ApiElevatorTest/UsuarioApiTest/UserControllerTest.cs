using AutoMapper;
using ElevadorAPI.Controllers;
using Interfaces;
using Interfaces.Utility;
using Models.DTOs;
using Models.Entities;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ApiElevadorTest.UsuarioApiTest
{
    public class UserControllerTest
    {
        private readonly Mock<IManagementUser> _managementUsuarioMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUtility> _utilityMock;
        private readonly UserController _controller;


        public UserControllerTest()
        {
            _managementUsuarioMock = new Mock<IManagementUser>();
            _mapperMock = new Mock<IMapper>();
            _utilityMock = new Mock<IUtility>();
            _controller = new UserController(_mapperMock.Object, _managementUsuarioMock.Object, _utilityMock.Object);
        }

        [Fact]
        public async Task Register_ReturnsSuccessResponse_WhenUserIsCreated()
        {
            // Arrange
            var usuarioDTO = new UserDTO { Name = "Test", Email = "test@mail.com", Password = "1234" };
            var usuario = new User { Name = "Test", Email = "test@mail.com", Password = "hashed", IsBlocked = false };
            var expectedResponse = new ApiResponse<ResponseAutenticationDTO> { Success = true, Data = new ResponseAutenticationDTO { Token = "token", Expiration = System.DateTime.Now } };

            _utilityMock.Setup(u => u.EncryptSHA256(It.IsAny<string>())).Returns("hashed");
            _mapperMock.Setup(m => m.Map<User>(usuarioDTO)).Returns(usuario);
            _managementUsuarioMock.Setup(m => m.UserRegistration(usuario)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Record(usuarioDTO);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal("token", result.Data.Token);
        }

        [Fact]
        public async Task Register_ReturnsErrorResponse_WhenExceptionThrown()
        {
            // Arrange
            var usuarioDTO = new UserDTO { Name = "Test", Email = "test@mail.com", Password = "1234" };
            var usuario = new User { Name = "Test", Email = "test@mail.com", Password = "hashed", IsBlocked = false };

            _utilityMock.Setup(u => u.EncryptSHA256(It.IsAny<string>())).Returns("hashed");
            _mapperMock.Setup(m => m.Map<User>(usuarioDTO)).Returns(usuario);
            _managementUsuarioMock.Setup(m => m.UserRegistration(usuario)).ThrowsAsync(new System.Exception("Error"));

            // Act
            var result = await _controller.Record(usuarioDTO);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Error", result.Errors[0]);
        }

        [Fact]
        public async Task Login_ReturnsSuccessResponse_WhenLoginIsValid()
        {
            // Arrange
            var loginDTO = new LoginDTO { Email = "test@mail.com", Password = "1234" };
            var expectedResponse = new ApiResponse<ResponseAutenticationDTO> { Success = true, Data = new ResponseAutenticationDTO { Token = "token", Expiration = System.DateTime.Now } };

            _utilityMock.Setup(u => u.EncryptSHA256(It.IsAny<string>())).Returns("hashed");
            _managementUsuarioMock.Setup(m => m.Login(It.IsAny<LoginDTO>())).ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Login(loginDTO);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal("token", result.Data.Token);
        }
    }
}
