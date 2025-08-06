

using ElevadorAPI.Controllers;
using Interfaces;
using Models.DTOs;
using Models.Entities;
using Models.Enums;
using Moq;

namespace ApiElevatorTest.ElevatorApiTest
{
    public class ElevatorControllerTest
    {
        private readonly Mock<IElevator> _elevatorMock;
        private readonly ElevatorController _controller;

        public ElevatorControllerTest()
        {
            _elevatorMock = new Mock<IElevator>();
            _controller = new ElevatorController(_elevatorMock.Object);
        }

        [Fact]
        public async Task Up_ReturnsSuccessResponse()
        {
            // Arrange
            var solicitud = new RequestElevatorDTO
            {
                FloorRequired = 3,
                DirectionRequest = DirectionElevator.Up,
                CurrentFloor = 2,
                Doors = StateDoor.Close,
                StateMovement = StateMovement.Moving,
                CurrentDirection = DirectionElevator.Up
            };
            var expected = new ApiResponse<ElevatorStateDTO>
            {
                Success = true,
                Data = new ElevatorStateDTO { CurrentFloor = 3, Doors = StateDoor.Close, StateMovement = StateMovement.Moving, CurrentDirection = DirectionElevator.Up }
            };

            _elevatorMock.Setup(e => e.ElevatorUp(solicitud)).ReturnsAsync(expected);

            // Act
            var result = await _controller.Up(solicitud);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(3, result.Data.CurrentFloor);
        }

        [Fact]
        public async Task Down_ReturnsSuccessResponse()
        {
            // Arrange
            var solicitud = new RequestElevatorDTO
            {
                FloorRequired = 1,
                DirectionRequest = DirectionElevator.Down,
                CurrentFloor = 2,
                Doors = StateDoor.Close,
                StateMovement = StateMovement.Moving,
                CurrentDirection = DirectionElevator.Down
            };
            var expected = new ApiResponse<ElevatorStateDTO>
            {
                Success = true,
                Data = new ElevatorStateDTO { CurrentFloor = 1, Doors = StateDoor.Close, StateMovement = StateMovement.Moving, CurrentDirection = DirectionElevator.Down }
            };

            _elevatorMock.Setup(e => e.ElevatorDown(solicitud)).ReturnsAsync(expected);

            // Act
            var result = await _controller.Down(solicitud);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(1, result.Data.CurrentFloor);
        }

        [Fact]
        public async Task RequestElevator_ReturnsSuccessResponse()
        {
            // Arrange
            var solicitud = new RequestElevatorDTO
            {
                FloorRequired = 2,
                DirectionRequest = DirectionElevator.None,
                CurrentFloor = 1,
                Doors = StateDoor.Close,
                StateMovement = StateMovement.Stop,
                CurrentDirection = DirectionElevator.None
            };
            var expected = new ApiResponse<ElevatorStateDTO>
            {
                Success = true,
                Data = new ElevatorStateDTO { CurrentFloor = 2, Doors = StateDoor.Close, StateMovement = StateMovement.Stop, CurrentDirection = DirectionElevator.None }
            };

            _elevatorMock.Setup(e => e.RequestElevator(solicitud)).ReturnsAsync(expected);

            // Act
            var result = await _controller.RequestElevator(solicitud);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.CurrentFloor);
        }
    }
}
