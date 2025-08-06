using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using WebApplicationElevador.Interfaces;
using WebApplicationElevador.Models;
using WebApplicationElevador.Models.DTOs;
using WebApplicationElevador.Models.Enum;
using WebApplicationElevador.Models.View;

namespace WebApplicationElevador.Controllers
{
    public class ElevatorController : Controller
    {
        private readonly IHttpClientHelper _httpClientHelper;
        private readonly IConfiguration _configuration;
        private readonly IUtility _utility;

        private string token = string.Empty;

        public ElevatorController(IHttpClientHelper httpClientHelper, IConfiguration configuration, IUtility utility)
        {
            _httpClientHelper = httpClientHelper;
            _configuration = configuration;
            _utility = utility;
        }
        public IActionResult Index()
        {
            var modelSelection = new ModelElevatorView();

            if (TempData["ErrorMessage"] is string responseJsonError)
            {
                return View(modelSelection);
            }

            if (TempData["ElevatorResponse"] is string responseJson)
            {
                var response = JsonSerializer.Deserialize<ResponseApi>(responseJson);
                if (response is not null)
                {
                    var data = _utility.DeserializeData<StateElevatorDTO>(response);
                    modelSelection.StateElevator.CurrentFloor = data.Result.CurrentFloor;
                    modelSelection.StateElevator.Doors = data.Result.Doors;
                    modelSelection.StateElevator.CurrentDirection = data.Result.CurrentDirection;
                }
            }

            modelSelection.StateDoorOptions = Enum.GetValues(typeof(StateDoor))
                .Cast<StateDoor>()
                .Select(e => new SelectListItem
                {
                    Text = e.ToString(),
                    Value = ((int)e).ToString(),
                    Selected = e == modelSelection.StateDoor
                }).ToList();

            modelSelection.StateMovementOptions = Enum.GetValues(typeof(StateMovement))
                .Cast<StateMovement>()
                .Select(e => new SelectListItem
                {
                    Text = e.ToString(),
                    Value = ((int)e).ToString(),
                    Selected = e == modelSelection.StateMovement
                }).ToList();

            modelSelection.DirectionOptions = Enum.GetValues(typeof(DirectionElevator))
                .Cast<DirectionElevator>()
                .Select(e => new SelectListItem
                {
                    Text = e.ToString(),
                    Value = ((int)e).ToString(),
                    Selected = e == modelSelection.DirectionElevator
                });

            return View(modelSelection);
        }

        public async Task<IActionResult> RequestElevator(RequestElevatorDTO solicitudElevadorDTO)
        {
            try
            {
                ResponseApi response = null;

                if (string.IsNullOrEmpty(token))
                {
                    token = await GetToken();
                }

                if (token is not null)
                {
                    response = await _httpClientHelper.PostAsync<ResponseApi>("elevator/requestElevator", solicitudElevadorDTO, token);
                }
                TempData["ElevatorResponse"] = JsonSerializer.Serialize(response);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error ocurred:{ex.Message}";

                return RedirectToAction("Index");
            }
            
        }

        public async Task<IActionResult> Up(RequestElevatorDTO requiredElevatorDTO)
        {
            try
            {
                if(requiredElevatorDTO.FloorRequired == 0 || 
                    requiredElevatorDTO.FloorRequired == requiredElevatorDTO.CurrentFloor)
                {
                    TempData["ErrorMessage"] = "You must select a floor or select a different one than the current one";

                    return RedirectToAction("Index");
                }
                
                ResponseApi response = null;
                requiredElevatorDTO.DirectionRequest = DirectionElevator.Up;
                if (string.IsNullOrEmpty(token))
                {
                   token = await GetToken();
                } 
                if(token is not null)
                {
                    response = await _httpClientHelper.PostAsync<ResponseApi>("elevator/up", requiredElevatorDTO, token);
                }
                TempData["ElevatorResponse"] = JsonSerializer.Serialize(response);

               return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Ocurrio un error:{ex.Message}";

                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Down(RequestElevatorDTO requiredElevatorDTO)
        {
            try
            {
                if (requiredElevatorDTO.FloorRequired == 0 ||
                    requiredElevatorDTO.FloorRequired == requiredElevatorDTO.CurrentFloor)
                {
                    TempData["ErrorMessage"] = "You must select a floor or select a different one than the current one";

                    return RedirectToAction("Index");
                }

                ResponseApi response = null;
                requiredElevatorDTO.DirectionRequest = DirectionElevator.Up;
                if (string.IsNullOrEmpty(token))
                {
                    token = await GetToken();
                }
                if (token is not null)
                {
                    response = await _httpClientHelper.PostAsync<ResponseApi>("elevator/down", requiredElevatorDTO, token);
                }
                TempData["ElevatorResponse"] = JsonSerializer.Serialize(response);

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred:{ex.Message}";

                return RedirectToAction("Index");
            }
        }


        private async Task<string?> GetToken()
        {
            var email = _configuration.GetValue<string>("User");
            var password = _configuration.GetValue<string>("Password");

            var loginDto = new LoginDTO { Email = email!, Password = password! };

            var loginResponse = await _httpClientHelper.PostAsync<ResponseApi>("user/login", loginDto);
            var getToken = _utility.DeserializeData<TokenDataDTO>(loginResponse);
            if (getToken is not null) 
            {
                return getToken.Result!.Token;
            }

            return null;
        }
    }
}
