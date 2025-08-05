using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using WebApplicationElevador.Interfaces;
using WebApplicationElevador.Models;
using WebApplicationElevador.Models.DTOs;
using WebApplicationElevador.Models.Enum;
using WebApplicationElevador.Models.View;

namespace ElevadorWebAplication.Controllers
{
    public class ElevadorController : Controller
    {
        private readonly IHttpClientHelper _httpClientHelper;
        private readonly IConfiguration _configuration;
        private readonly IUtility _utility;

        private string token = string.Empty;

        public ElevadorController(IHttpClientHelper httpClientHelper, IConfiguration configuration, IUtility utility)
        {
            _httpClientHelper = httpClientHelper;
            _configuration = configuration;
            _utility = utility;
        }
        public IActionResult Index()
        {
            var modelSelection = new ModeloElevadorView();

            if (TempData["ErrorMessage"] is string responseJsonError)
            {
                return View(modelSelection);
            }

            if (TempData["ElevadorResponse"] is string responseJson)
            {
                var response = JsonSerializer.Deserialize<ResponseApi>(responseJson);
                if (response is not null)
                {
                    var data = _utility.DeserializeData<ElevadorEstadoDTO>(response);
                    modelSelection.ElevadorEstado.PisoActual = data.Result.PisoActual;
                    modelSelection.ElevadorEstado.Puertas = data.Result.Puertas;
                    modelSelection.ElevadorEstado.DireccionActual = data.Result.DireccionActual;
                }
            }

            modelSelection.EstadoPuertaOptions = Enum.GetValues(typeof(EstadoPuerta))
                .Cast<EstadoPuerta>()
                .Select(e => new SelectListItem
                {
                    Text = e.ToString(),
                    Value = ((int)e).ToString(),
                    Selected = e == modelSelection.EstadoPuerta
                }).ToList();

            modelSelection.EstadoMovimientoOptions = Enum.GetValues(typeof(EstadoMovimiento))
                .Cast<EstadoMovimiento>()
                .Select(e => new SelectListItem
                {
                    Text = e.ToString(),
                    Value = ((int)e).ToString(),
                    Selected = e == modelSelection.EstadoMovimiento
                }).ToList();

            modelSelection.DireccionOptions = Enum.GetValues(typeof(DireccionElevador))
                .Cast<DireccionElevador>()
                .Select(e => new SelectListItem
                {
                    Text = e.ToString(),
                    Value = ((int)e).ToString(),
                    Selected = e == modelSelection.DireccionElevador
                });

            return View(modelSelection);
        }

        public async Task<IActionResult> LlamaElevador(SolicitudElevadorDTO solicitudElevadorDTO)
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
                    response = await _httpClientHelper.PostAsync<ResponseApi>("elevador/llamaElevador", solicitudElevadorDTO, token);
                }
                TempData["ElevadorResponse"] = JsonSerializer.Serialize(response);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Ocurrio un error:{ex.Message}";

                return RedirectToAction("Index");
            }
            
        }

        public async Task<IActionResult> Subir(SolicitudElevadorDTO solicitudElevadorDTO)
        {
            try
            {
                if(solicitudElevadorDTO.PisoSolicitado == 0 || 
                    solicitudElevadorDTO.PisoSolicitado == solicitudElevadorDTO.PisoActual)
                {
                    TempData["ErrorMessage"] = "Debes seleccionar un piso o selecciona uno diferente al actual";

                    return RedirectToAction("Index");
                }
                
                ResponseApi response = null;
                solicitudElevadorDTO.DireccionSolicitada = DireccionElevador.Subir;
                if (string.IsNullOrEmpty(token))
                {
                   token = await GetToken();
                } 
                if(token is not null)
                {
                    response = await _httpClientHelper.PostAsync<ResponseApi>("elevador/subir", solicitudElevadorDTO, token);
                }
                TempData["ElevadorResponse"] = JsonSerializer.Serialize(response);

               return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Ocurrio un error:{ex.Message}";

                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Bajar(SolicitudElevadorDTO solicitudElevadorDTO)
        {
            try
            {
                if (solicitudElevadorDTO.PisoSolicitado == 0 ||
                    solicitudElevadorDTO.PisoSolicitado == solicitudElevadorDTO.PisoActual)
                {
                    TempData["ErrorMessage"] = "Debes seleccionar un piso o selecciona uno diferente al actual";

                    return RedirectToAction("Index");
                }

                ResponseApi response = null;
                solicitudElevadorDTO.DireccionSolicitada = DireccionElevador.Subir;
                if (string.IsNullOrEmpty(token))
                {
                    token = await GetToken();
                }
                if (token is not null)
                {
                    response = await _httpClientHelper.PostAsync<ResponseApi>("elevador/bajar", solicitudElevadorDTO, token);
                }
                TempData["ElevadorResponse"] = JsonSerializer.Serialize(response);

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Ocurrio un error:{ex.Message}";

                return RedirectToAction("Index");
            }
        }


        private async Task<string?> GetToken()
        {
            var email = _configuration.GetValue<string>("User");
            var password = _configuration.GetValue<string>("Password");

            var loginDto = new LoginDTO { Correo = email!, Clave = password! };

            var loginResponse = await _httpClientHelper.PostAsync<ResponseApi>("usuario/login", loginDto);
            var getToken = _utility.DeserializeData<TokenDataDTO>(loginResponse);
            if (getToken is not null) 
            {
                return getToken.Result!.Token;
            }

            return null;
        }
    }
}
