using Warren.Tools.Domain.Entities;

namespace Warren.Tools.Domain.Interfaces.Repositories
{
    public interface IBoletaRepository
    {

        Task<IEnumerable<BoletaEntity>> GetAll(DateTime dtInicio, DateTime dtFim);
    }
}
