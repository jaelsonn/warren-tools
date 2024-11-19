using Com.Warren.Services.Response;
using Warren.Tools.Domain.DTO.Request;
using Warren.Tools.Domain.Entities;

namespace Warren.Tools.Domain.Interfaces.Services
{
    public interface IBoletaService
    {
        Task<NecessidadeCaixaResponse> CalculoNecessidadeCaixaPorLista(List<BoletasIds> request);
        Task<List<BoletaEntity>> GetAllBoletas();
        Task<NecessidadeCaixaResponse> CalculoNecessidadeCaixaTodasBoletas();
    }
}
