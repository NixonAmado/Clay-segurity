using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistencia.Data;


namespace Application.Repository;

    public class PersonaRepository : GenericRepository<Persona>, IPersona
    {
        private readonly DbAppContext _context;

        public PersonaRepository(DbAppContext context): base(context)
        {
            _context = context;
        }

        //Listar todos los empleados de la empresa de seguridad
        public async Task<IEnumerable<Persona>> GetAllEmployeeFromCompany()
        {
            return await _context.Personas 
                            .Include(p => p.TPersonaNavigation)   
                            .Where(p => p.TPersonaNavigation.Descripcion.ToUpper() == "EMPLEADO")
                            .ToListAsync();
        }
        
        //2) Listar todos los empleados que son vigilantes
        public async Task<IEnumerable<Persona>> GetAllEmployeebyCategory(string categoria)
        {
            return await _context.Personas 
                            .Include(p => p.TPersonaNavigation)   
                            .Where(p => p.CPersonaNavigation.Nombre.ToUpper() == categoria.ToUpper())
                            .ToListAsync();
        }

        //3) Listar los numeros de contacto  de un empleado que sea vigilante
        public async Task<IEnumerable<Persona>> GetAllPhoneNumEmployeeByCategory(string categoria)
        {
            return await _context.Personas 
                            .Include(p => p.Contactos)   
                            .Where(p => p.CPersonaNavigation.Nombre.ToUpper() == categoria.ToUpper())
                            .ToListAsync();
        }

        //4) Listar todos clientes que vivan en la ciudad de Bucaramanga
        public async Task<IEnumerable<Persona>> GetCustomersByCity(string city)
        {
            return await _context.Personas 
                            .Include(p => p.TPersonaNavigation)   
                            .Where(p => p.TPersonaNavigation.Descripcion.ToUpper() == "CLIENTE" &&
                                        p.CiudadNavigation.NombreCiudad.ToUpper() == city.ToUpper())
                            .ToListAsync();
        }
        //5) Listar todos empleados que vivan en X y x.
        public async Task<IEnumerable<Persona>> GetCustomersByDirection(string direction1, string direction2)
        {
            return await _context.Personas 
                            .Include(p => p.TPersonaNavigation)   
                            .Where(p => p.TPersonaNavigation.Descripcion.ToUpper() == "EMPLEADO" &&
                                        p.Direcciones.Any(d => d.Barrio.ToUpper() == direction1.ToUpper() ||
                                                               d.Barrio.ToUpper() == direction2.ToUpper() ))
                            .ToListAsync();
        }
        //6) Listar todos clientes que tengan mas de 5 años ade antiguedad.
        public async Task<IEnumerable<Persona>> GetCustomersByantiquity(int quantity)
        {
            var fechaActual =  DateTime.Now;
            return await _context.Personas 
                            .Include(p => p.TPersonaNavigation)   
                            .Where(p => p.TPersonaNavigation.Descripcion.ToUpper() == "CLIENTE" &&
                                        (fechaActual.Year - p.DateReg.Year) > quantity ).ToListAsync();
        }
          public override async Task<(int totalRegistros, IEnumerable<Persona> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.Personas as IQueryable<Persona>;
    
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