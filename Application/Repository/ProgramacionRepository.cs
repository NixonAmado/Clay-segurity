using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistencia.Data;


namespace Application.Repository;

    public class ProgramacionRepository : GenericRepository<Programacion>, IProgramacion
    {
        private readonly DbAppContext _context;

        public ProgramacionRepository(DbAppContext context): base(context)
        {
            _context = context;
        }
          public override async Task<(int totalRegistros, IEnumerable<Programacion> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.Programaciones as IQueryable<Programacion>;
    
                if(!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.Id.ToString() == search.ToLower());
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