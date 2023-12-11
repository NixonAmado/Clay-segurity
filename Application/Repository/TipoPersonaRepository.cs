using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistencia.Data;


namespace Application.Repository;

    public class TipoPersonaRepository : GenericRepository<TipoPersona>, ITipoPersona
    {
        private readonly DbAppContext _context;

        public TipoPersonaRepository(DbAppContext context): base(context)
        {
            _context = context;
        }
          public override async Task<(int totalRegistros, IEnumerable<TipoPersona> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.TipoPersonas as IQueryable<TipoPersona>;
    
                if(!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.Descripcion.ToLower() == search.ToLower());
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