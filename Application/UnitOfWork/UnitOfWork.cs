using Application.Repository;
using Domain.Entities;
using Domain.Interfaces;
using Persistencia.Data;

namespace Application.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly DbAppContext _context;
        private RoleRepository _roles;
        private UserRepository _users;
        private CategoriaPersonaRepository _CategoriaPersonas;
        private CiudadRepository _Ciudades;
        private ContactoPersonaRepository _Contactos;
        private ContratoRepository _Contratos;
        private DepartamentoRepository _Departamentos;
        private DireccionRepository _Direcciones;
        private EstadoRepository _Estados;
        private PaisRepository _Paises;
        private PersonaRepository _Personas;
        private ProgramacionRepository _Programaciones;
        private TipoContactoRepository _TipoContactos;
        private TipoDireccionRepository _TipoDirecciones;
        private TipoPersonaRepository _TipoPersonas;
        private TurnoRepository _Turnos;        

        public UnitOfWork(DbAppContext context)
        {
            _context = context;
        }

        public IRole Roles
        {
            get
            {
                if(_roles == null)
                {
                    _roles = new RoleRepository(_context);
                }
                return _roles;
            }
        }
        public IUser Users{
            get{
                if(_users == null)
                {
                    _users = new UserRepository(_context); 
                }
                return _users;
            }
        }

        public ICategoriaPersona CategoriaPersonas{
            get{
                if(_CategoriaPersonas == null)
                {
                    _CategoriaPersonas = new CategoriaPersonaRepository(_context); 
                }
                return _CategoriaPersonas;
            }
        }
        public ICiudad Ciudades{
            get{
                if(_Ciudades == null)
                {
                    _Ciudades = new CiudadRepository(_context); 
                }
                return _Ciudades;
            }
        }
        public IContactoPersona Contactos{
            get{
                if(_Contactos == null)
                {
                    _Contactos = new ContactoPersonaRepository(_context); 
                }
                return _Contactos;
            }
        }

        public IContrato Contratos{
            get{
                if(_Contratos == null)
                {
                    _Contratos = new ContratoRepository(_context); 
                }
                return _Contratos;
            }
        }
        public IDepartamento Departamentos{
            get{
                if(_Departamentos == null)
                {
                    _Departamentos = new DepartamentoRepository(_context); 
                }
                return _Departamentos;
            }
        }
        public IDireccion Direcciones{
            get{
                if(_Direcciones == null)
                {
                    _Direcciones = new DireccionRepository(_context); 
                }
                return _Direcciones;
            }
        }
        public IEstado Estados{
            get{
                if(_Estados == null)
                {
                    _Estados = new EstadoRepository(_context); 
                }
                return _Estados;
            }
        }
        public IPais Paises{
            get{
                if(_Paises == null)
                {
                    _Paises = new PaisRepository(_context); 
                }
                return _Paises;
            }
        }
        public IPersona Personas{
            get{
                if(_Personas == null)
                {
                    _Personas = new PersonaRepository(_context); 
                }
                return _Personas;
            }
        }
        
        public IProgramacion Programaciones{
            get{
                if(_Programaciones == null)
                {
                    _Programaciones = new ProgramacionRepository(_context); 
                }
                return _Programaciones;
            }
        }        
        public ITipoContacto TipoContactos{
            get{
                if(_TipoContactos == null)
                {
                    _TipoContactos = new TipoContactoRepository(_context); 
                }
                return _TipoContactos;
            }
        }
        public ITipoDireccion TipoDirecciones{
            get{
                if(_TipoDirecciones == null)
                {
                    _TipoDirecciones = new TipoDireccionRepository(_context); 
                }
                return _TipoDirecciones;
            }
        }
        public ITipoPersona TipoPersonas{
            get{
                if(_TipoPersonas == null)
                {
                    _TipoPersonas = new TipoPersonaRepository(_context); 
                }
                return _TipoPersonas;
            }
        }
        
public ITurno Turnos{
            get{
                if(_Turnos == null)
                {
                    _Turnos = new TurnoRepository(_context); 
                }
                return _Turnos;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }


        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        
    }
}