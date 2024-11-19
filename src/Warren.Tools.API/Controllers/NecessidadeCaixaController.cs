using System.Text.Json;
using System.Text.Json.Serialization;
using Com.Warren.Services.Response;
using Microsoft.AspNetCore.Mvc;
using Warren.Tools.Domain.DTO.Request;
using Warren.Tools.Domain.DTO.Response;
using Warren.Tools.Domain.Interfaces.Services;

namespace Warren.Tools.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class NecessidadeCaixaController : ControllerBase 
    {
        private readonly IBoletaService _boletaservice;

        public NecessidadeCaixaController(IBoletaService boletaservice)
        {
            _boletaservice = boletaservice;
        }

        [HttpGet("getAllBoletasIntradias")]
        [ProducesResponseType(typeof(List<BoletaResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBoletas()
        {
            var response = await _boletaservice.GetAllBoletas();
            return Ok(response); 
        }

        [HttpPost("postCalculoNecessidadeCaixaPorRange")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(NecessidadeCaixaResponse))]
        public async Task<IActionResult> PostNecessidadeCaixaPorRange(List<BoletasIds> request)
        {
            var result = await _boletaservice.CalculoNecessidadeCaixaPorLista(request);

            return Ok(result);
        }
        
        [HttpPost("postCalculoNecessidadeCaixaTodasBoletas")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(NecessidadeCaixaResponse))]
        public async Task<IActionResult> PostNecessidadeCaixaTodas()
        {
            var result = await _boletaservice.CalculoNecessidadeCaixaTodasBoletas();
            
            return Ok(result);
        }
    }
}
