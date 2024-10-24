using Microsoft.EntityFrameworkCore;
using Warren.Tools.Domain.Interfaces.Repositories;
using Warren.Tools.Domain.Models;
using Warren.Tools.Infra.Context;

namespace Warren.Tools.Infra.Repositories
{
    public class PuRepository : IPuRepository
    {
        private readonly DatabaseContext _context;

        public PuRepository(DatabaseContext context)
        {
            _context = context;
        }        
        public async Task<IEnumerable<Pu550Entity>> GetAll(DateTime dtInicio, DateTime dtFim)
        {
            return await _context.Pu550.Where(x => x.MovementDate >= dtInicio && x.MovementDate <= dtFim).ToListAsync();
        }        
    }
}
