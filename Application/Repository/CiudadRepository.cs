using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistencia.Data;


namespace Application.Repository;

    public class CiudadRepository : GenericRepository<Ciudad>, ICiudad
    {
        private readonly DbAppContext _context;

        public CiudadRepository(DbAppContext context): base(context)
        {
            _context = context;
        }
          public override async Task<(int totalRegistros, IEnumerable<Ciudad> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.Ciudades as IQueryable<Ciudad>;
    
                if(!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.NombreCiudad.ToLower() == search.ToLower());
                }
    
                query = query.OrderBy(p => p.Id);
                var totalRegistros = await query.CountAsync();
                var registros = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
    
                return (totalRegistros, registros);
            }
    }