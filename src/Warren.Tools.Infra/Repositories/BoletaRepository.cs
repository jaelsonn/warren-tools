using Microsoft.EntityFrameworkCore;
using Warren.Tools.Domain.Entities;
using Warren.Tools.Domain.Interfaces.Repositories;
using Warren.Tools.Infra.Context;

namespace Warren.Tools.Infra.Repositories
{
    public class BoletaRepository : IBoletaRepository
    {

        private readonly DatabaseContext _context;

        public BoletaRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BoletaEntity>> GetAll(DateTime dtInicio, DateTime dtFim)
        {
            return await _context.Boletas.Where(x => x.DateLiquidation >= dtInicio && x.DateLiquidation <= dtFim).ToListAsync();
        }
    }
}
