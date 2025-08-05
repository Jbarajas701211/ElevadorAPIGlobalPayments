

using ElevadorAPI.Controllers;
using Interfaces;
using Models.DTOs;
using Models.Entities;
using Models.Enums;
using Moq;

namespace ApiElevadorTest.ElevadorApiTest
{
    public class ElevadorControllerTest
    {
        private readonly Mock<IElevador> _elevadorMock;
        private readonly ElevadorController _controller;

        public ElevadorControllerTest()
        {
            _elevadorMock = new Mock<IElevador>();
            _controller = new ElevadorController(_elevadorMock.Object);
        }

        [Fact]
        public async Task Subir_ReturnsSuccessResponse()
        {
            // Arrange
            var solicitud = new SolicitudElevadorDTO
            {
                PisoSolicitado = 3,
                DireccionSolicitada = DireccionElevador.Subir,
                PisoActual = 2,
                Puertas = EstadoPuerta.Cerrada,
                EstadoMovimiento = EstadoMovimiento.Moviendo,
                DireccionActual = DireccionElevador.Subir
            };
            var expected = new ApiResponse<ElevadorEstadoDTO>
            {
                Success = true,
                Data = new ElevadorEstadoDTO { PisoActual = 3, Puertas = EstadoPuerta.Cerrada, EstadoMovimiento = EstadoMovimiento.Moviendo, DireccionActual = DireccionElevador.Subir }
            };

            _elevadorMock.Setup(e => e.ElevadorHaciaArriba(solicitud)).ReturnsAsync(expected);

            // Act
            var result = await _controller.Subir(solicitud);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(3, result.Data.PisoActual);
        }

        [Fact]
        public async Task Bajar_ReturnsSuccessResponse()
        {
            // Arrange
            var solicitud = new SolicitudElevadorDTO
            {
                PisoSolicitado = 1,
                DireccionSolicitada = DireccionElevador.Bajar,
                PisoActual = 2,
                Puertas = EstadoPuerta.Cerrada,
                EstadoMovimiento = EstadoMovimiento.Moviendo,
                DireccionActual = DireccionElevador.Bajar
            };
            var expected = new ApiResponse<ElevadorEstadoDTO>
            {
                Success = true,
                Data = new ElevadorEstadoDTO { PisoActual = 1, Puertas = EstadoPuerta.Cerrada, EstadoMovimiento = EstadoMovimiento.Moviendo, DireccionActual = DireccionElevador.Bajar }
            };

            _elevadorMock.Setup(e => e.ElevadorHaciaAbajo(solicitud)).ReturnsAsync(expected);

            // Act
            var result = await _controller.Bajar(solicitud);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(1, result.Data.PisoActual);
        }

        [Fact]
        public async Task LlamaElevador_ReturnsSuccessResponse()
        {
            // Arrange
            var solicitud = new SolicitudElevadorDTO
            {
                PisoSolicitado = 2,
                DireccionSolicitada = DireccionElevador.Ninguna,
                PisoActual = 1,
                Puertas = EstadoPuerta.Cerrada,
                EstadoMovimiento = EstadoMovimiento.Parado,
                DireccionActual = DireccionElevador.Ninguna
            };
            var expected = new ApiResponse<ElevadorEstadoDTO>
            {
                Success = true,
                Data = new ElevadorEstadoDTO { PisoActual = 2, Puertas = EstadoPuerta.Cerrada, EstadoMovimiento = EstadoMovimiento.Parado, DireccionActual = DireccionElevador.Ninguna }
            };

            _elevadorMock.Setup(e => e.LlamaElevador(solicitud)).ReturnsAsync(expected);

            // Act
            var result = await _controller.LlamaElevador(solicitud);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.PisoActual);
        }
    }
}
