using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warren.Tools.Domain.Entities;
using Warren.Tools.Domain.Models;

namespace Warren.Tools.Domain.Interfaces.Repositories
{
    public interface IPuRepository
    {
        Task<IEnumerable<Pu550Entity>> GetAll(DateTime dtInicio, DateTime dtFim);
    }
}
