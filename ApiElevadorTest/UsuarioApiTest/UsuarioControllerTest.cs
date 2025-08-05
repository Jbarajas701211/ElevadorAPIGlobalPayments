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
    public class UsuarioControllerTest
    {
        private readonly Mock<IManagementUsuario> _managementUsuarioMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUtility> _utilityMock;
        private readonly UsuarioController _controller;


        public UsuarioControllerTest()
        {
            _managementUsuarioMock = new Mock<IManagementUsuario>();
            _mapperMock = new Mock<IMapper>();
            _utilityMock = new Mock<IUtility>();
            _controller = new UsuarioController(_mapperMock.Object, _managementUsuarioMock.Object, _utilityMock.Object);
        }

        [Fact]
        public async Task Registrarse_ReturnsSuccessResponse_WhenUserIsCreated()
        {
            // Arrange
            var usuarioDTO = new UsuarioDTO { Nombre = "Test", Correo = "test@mail.com", Clave = "1234" };
            var usuario = new Usuario { Nombre = "Test", Correo = "test@mail.com", Clave = "hashed", EsBloqueado = false };
            var expectedResponse = new ApiResponse<RespuestaAutenticacionDTO> { Success = true, Data = new RespuestaAutenticacionDTO { Token = "token", Expiracion = System.DateTime.Now } };

            _utilityMock.Setup(u => u.encriptarSHA256(It.IsAny<string>())).Returns("hashed");
            _mapperMock.Setup(m => m.Map<Usuario>(usuarioDTO)).Returns(usuario);
            _managementUsuarioMock.Setup(m => m.RegistroUsuario(usuario)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Registrarse(usuarioDTO);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal("token", result.Data.Token);
        }

        [Fact]
        public async Task Registrarse_ReturnsErrorResponse_WhenExceptionThrown()
        {
            // Arrange
            var usuarioDTO = new UsuarioDTO { Nombre = "Test", Correo = "test@mail.com", Clave = "1234" };
            var usuario = new Usuario { Nombre = "Test", Correo = "test@mail.com", Clave = "hashed", EsBloqueado = false };

            _utilityMock.Setup(u => u.encriptarSHA256(It.IsAny<string>())).Returns("hashed");
            _mapperMock.Setup(m => m.Map<Usuario>(usuarioDTO)).Returns(usuario);
            _managementUsuarioMock.Setup(m => m.RegistroUsuario(usuario)).ThrowsAsync(new System.Exception("Error"));

            // Act
            var result = await _controller.Registrarse(usuarioDTO);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Error", result.Errors[0]);
        }

        [Fact]
        public async Task Login_ReturnsSuccessResponse_WhenLoginIsValid()
        {
            // Arrange
            var loginDTO = new LoginDTO { Correo = "test@mail.com", Clave = "1234" };
            var expectedResponse = new ApiResponse<RespuestaAutenticacionDTO> { Success = true, Data = new RespuestaAutenticacionDTO { Token = "token", Expiracion = System.DateTime.Now } };

            _utilityMock.Setup(u => u.encriptarSHA256(It.IsAny<string>())).Returns("hashed");
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
