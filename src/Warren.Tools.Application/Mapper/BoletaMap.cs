using Warren.Tools.Domain.DTO.Response;
using Warren.Tools.Domain.Entities;

namespace Warren.Tools.Application.Mapper
{
    public class BoletaMap : MappingProfile
    {
        public BoletaMap()
        {
            CreateMap<BoletaEntity, BoletaResponse>();
        }
        
    }
    
}
