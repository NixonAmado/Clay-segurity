using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistencia.Data;


namespace Application.Repository;

    public class ContratoRepository : GenericRepository<Contrato>, IContrato
    {
        private readonly DbAppContext _context;

        public ContratoRepository(DbAppContext context): base(context)
        {
            _context = context;
        }
          public override async Task<(int totalRegistros, IEnumerable<Contrato> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.Contratos as IQueryable<Contrato>;
    
                if(!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.Id.ToString() == search);
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