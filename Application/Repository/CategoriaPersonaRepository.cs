using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;


namespace Application.Repository;

    public class CategoriaPersonaRepository : GenericRepository<CategoriaPersona>, ICategoriaPersona
    {
        private readonly DbAppContext _context;

        public CategoriaPersonaRepository(DbAppContext context): base(context)
        {
            _context = context;
        }
          public override async Task<(int totalRegistros, IEnumerable<CategoriaPersona> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.CategoriaPersonas as IQueryable<CategoriaPersona>;
    
                if(!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.Nombre.ToLower() == search.ToLower());
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