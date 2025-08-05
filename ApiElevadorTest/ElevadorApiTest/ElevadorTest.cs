using Implementation;
using Models.DTOs;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiElevadorTest.ElevadorApiTest
{
    public class ElevadorTest
    {
        private readonly Elevador _elevador;

        public ElevadorTest()
        {
            _elevador = new Elevador();
        }

        [Fact]
        public async Task ElevadorHaciaArriba_ReturnsError_WhenPisoActualIs5()
        {
            var solicitud = new SolicitudElevadorDTO
            {
                PisoActual = 5,
                PisoSolicitado = 5,
                DireccionSolicitada = DireccionElevador.Subir
            };

            var result = await _elevador.ElevadorHaciaArriba(solicitud);

            Assert.False(result.Success);
            Assert.Contains("No se puede solicitar hacia arriba en el piso 5", result.Errors);
        }

        [Fact]
        public async Task ElevadorHaciaArriba_ReturnsError_WhenPisoActualEqualsPisoSolicitado()
        {
            var solicitud = new SolicitudElevadorDTO
            {
                PisoActual = 3,
                PisoSolicitado = 3,
                DireccionSolicitada = DireccionElevador.Subir
            };

            var result = await _elevador.ElevadorHaciaArriba(solicitud);

            Assert.False(result.Success);
            Assert.Contains("Ya se encuentra en el piso solicitado", result.Errors);
        }

        [Fact]
        public async Task ElevadorHaciaArriba_ReturnsSuccess_WhenValid()
        {
            var solicitud = new SolicitudElevadorDTO
            {
                PisoActual = 2,
                PisoSolicitado = 3,
                DireccionSolicitada = DireccionElevador.Subir
            };

            var result = await _elevador.ElevadorHaciaArriba(solicitud);

            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(3, result.Data.PisoActual);
            Assert.Equal(DireccionElevador.Subir, result.Data.DireccionActual);
            Assert.Equal(EstadoPuerta.Cerrada, result.Data.Puertas);
            Assert.Equal(EstadoMovimiento.Moviendo, result.Data.EstadoMovimiento);
        }

        [Fact]
        public async Task ElevadorHaciaAbajo_ReturnsError_WhenPisoActualIs1()
        {
            var solicitud = new SolicitudElevadorDTO
            {
                PisoActual = 1,
                PisoSolicitado = 1,
                DireccionSolicitada = DireccionElevador.Bajar
            };

            var result = await _elevador.ElevadorHaciaAbajo(solicitud);

            Assert.False(result.Success);
            Assert.Contains("No se puede solicitar hacia abajo en el piso 1", result.Errors);
        }

        [Fact]
        public async Task ElevadorHaciaAbajo_ReturnsError_WhenPisoActualEqualsPisoSolicitado()
        {
            var solicitud = new SolicitudElevadorDTO
            {
                PisoActual = 2,
                PisoSolicitado = 2,
                DireccionSolicitada = DireccionElevador.Bajar
            };

            var result = await _elevador.ElevadorHaciaAbajo(solicitud);

            Assert.False(result.Success);
            Assert.Contains("Ya se encuentra en el piso solicitado", result.Errors);
        }

        [Fact]
        public async Task ElevadorHaciaAbajo_ReturnsSuccess_WhenValid()
        {
            var solicitud = new SolicitudElevadorDTO
            {
                PisoActual = 3,
                PisoSolicitado = 2,
                DireccionSolicitada = DireccionElevador.Bajar
            };

            var result = await _elevador.ElevadorHaciaAbajo(solicitud);

            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.PisoActual);
            Assert.Equal(DireccionElevador.Bajar, result.Data.DireccionActual);
            Assert.Equal(EstadoPuerta.Cerrada, result.Data.Puertas);
            Assert.Equal(EstadoMovimiento.Moviendo, result.Data.EstadoMovimiento);
        }

        [Fact]
        public async Task LlamaElevador_ReturnsSuccess_AndOpensDoor()
        {
            var solicitud = new SolicitudElevadorDTO
            {
                PisoActual = 4,
                PisoSolicitado = 2,
                DireccionSolicitada = DireccionElevador.Ninguna
            };

            var result = await _elevador.LlamaElevador(solicitud);

            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(4, result.Data.PisoActual);
            Assert.Equal(EstadoPuerta.Abierta, result.Data.Puertas);
            Assert.Equal(EstadoMovimiento.Parado, result.Data.EstadoMovimiento);
            Assert.Equal(DireccionElevador.Ninguna, result.Data.DireccionActual);
        }
    }
}
