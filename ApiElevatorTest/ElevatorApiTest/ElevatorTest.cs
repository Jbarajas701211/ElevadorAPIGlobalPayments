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
    public class ElevatorTest
    {
        private readonly Elevator _elevador;

        public ElevatorTest()
        {
            _elevador = new Elevator();
        }

        [Fact]
        public async Task ElevadorHaciaArriba_ReturnsError_WhenPisoActualIs5()
        {
            var solicitud = new RequestElevatorDTO
            {
                CurrentFloor = 5,
                FloorRequired = 5,
                DirectionRequest = DirectionElevator.Up
            };

            var result = await _elevador.ElevatorUp(solicitud);

            Assert.False(result.Success);
            Assert.Contains("Yo cannot request upwards on the 5th floor", result.Errors);
        }

        [Fact]
        public async Task ElevadorHaciaArriba_ReturnsError_WhenPisoActualEqualsPisoSolicitado()
        {
            var solicitud = new RequestElevatorDTO
            {
                CurrentFloor = 3,
                FloorRequired = 3,
                DirectionRequest = DirectionElevator.Up
            };

            var result = await _elevador.ElevatorUp(solicitud);

            Assert.False(result.Success);
            Assert.Contains("You are already on the requested floor", result.Errors);
        }

        [Fact]
        public async Task ElevadorHaciaArriba_ReturnsSuccess_WhenValid()
        {
            var solicitud = new RequestElevatorDTO
            {
                CurrentFloor = 2,
                FloorRequired = 3,
                DirectionRequest = DirectionElevator.Up
            };

            var result = await _elevador.ElevatorUp(solicitud);

            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(3, result.Data.CurrentFloor);
            Assert.Equal(DirectionElevator.Up, result.Data.CurrentDirection);
            Assert.Equal(StateDoor.Close, result.Data.Doors);
            Assert.Equal(StateMovement.Moving, result.Data.StateMovement);
        }

        [Fact]
        public async Task ElevadorHaciaAbajo_ReturnsError_WhenPisoActualIs1()
        {
            var solicitud = new RequestElevatorDTO
            {
                CurrentFloor = 1,
                FloorRequired = 1,
                DirectionRequest = DirectionElevator.Down
            };

            var result = await _elevador.ElevatorDown(solicitud);

            Assert.False(result.Success);
            Assert.Contains("You cannot request a descent on the 1st floor", result.Errors);
        }

        [Fact]
        public async Task ElevadorHaciaAbajo_ReturnsError_WhenPisoActualEqualsPisoSolicitado()
        {
            var solicitud = new RequestElevatorDTO
            {
                CurrentFloor = 2,
                FloorRequired = 2,
                DirectionRequest = DirectionElevator.Down
            };

            var result = await _elevador.ElevatorDown(solicitud);

            Assert.False(result.Success);
            Assert.Contains("You are already on the requested floor", result.Errors);
        }

        [Fact]
        public async Task ElevadorHaciaAbajo_ReturnsSuccess_WhenValid()
        {
            var solicitud = new RequestElevatorDTO
            {
                CurrentFloor = 3,
                FloorRequired = 2,
                DirectionRequest = DirectionElevator.Down
            };

            var result = await _elevador.ElevatorDown(solicitud);

            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.CurrentFloor);
            Assert.Equal(DirectionElevator.Down, result.Data.CurrentDirection);
            Assert.Equal(StateDoor.Close, result.Data.Doors);
            Assert.Equal(StateMovement.Moving, result.Data.StateMovement);
        }

        [Fact]
        public async Task LlamaElevador_ReturnsSuccess_AndOpensDoor()
        {
            var solicitud = new RequestElevatorDTO
            {
                CurrentFloor = 4,
                FloorRequired = 2,
                DirectionRequest = DirectionElevator.None
            };

            var result = await _elevador.RequestElevator(solicitud);

            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(4, result.Data.CurrentFloor);
            Assert.Equal(StateDoor.Open, result.Data.Doors);
            Assert.Equal(StateMovement.Stop, result.Data.StateMovement);
            Assert.Equal(DirectionElevator.None, result.Data.CurrentDirection);
        }
    }
}
