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
        //7) Listar Todos los contratos cuyo estado es activo. Se debe mostrar el Nro de contrato el nombre del cliente y el empleado que registro el contrato.
        public async Task<IEnumerable<Contrato>> GetContractByStatus()
        {
            return await _context.Contratos 
                            .Include(c => c.ClienteNavigation)
                            .Include(c => c.EmpleadoNavigation)
                            .Where(p => p.EstadoNavigation.Descripcion.ToUpper() == "ACTIVO")
                            .ToListAsync();
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