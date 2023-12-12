## Consultas requeridas
1) Listar todos los empleados de la empresa de seguridad
```
/API/Persona/GetAllEmployeeFromCompany
```

```
    public async Task<IEnumerable<Persona>> GetAllEmployeeFromCompany()
    {
        return await _context.Personas 
                        .Include(p => p.TPersonaNavigation)   
                        .Where(p => p.TPersonaNavigation.Descripcion.ToUpper() == "EMPLEADO")
                        .ToListAsync();
    }
```

2) Listar todos los empleados que son X
```
/API/Persona/GetAllEmployeebyCategory/{category}
```
```
    public async Task<IEnumerable<Persona>> GetAllEmployeebyCategory(string categoria)
    {
        return await _context.Personas 
                        .Include(p => p.TPersonaNavigation)   
                        .Where(p => p.CPersonaNavigation.Nombre.ToUpper() == categoria.ToUpper())
                        .ToListAsync();
    }
```
3) Listar los numeros de contacto  de un empleado que sea X
```
/API/Persona/GetAllPhoneNumEmployeeByCategory/{category}
```
```
    public async Task<IEnumerable<Persona>> GetAllPhoneNumEmployeeByCategory(string categoria)
    {
        return await _context.Personas 
                        .Include(p => p.Contactos)   
                        .Where(p => p.CPersonaNavigation.Nombre.ToUpper() == categoria.ToUpper())
                        .ToListAsync();
    }
```
4) Listar todos clientes que vivan en la ciudad de Bucaramanga
```
http://localhost:5174/API/Persona/GetCustomersByCity/bucaramanga
```
```
    public async Task<IEnumerable<Persona>> GetCustomersByCity(string city)
    {
        return await _context.Personas 
                        .Include(p => p.TPersonaNavigation)   
                        .Where(p => p.TPersonaNavigation.Descripcion.ToUpper() == "CLIENTE" &&
                                    p.CiudadNavigation.NombreCiudad.ToUpper() == city.ToUpper())
                        .ToListAsync();
    }
```

5) Listar todos emplados que vivan en Giron y Piedecuesta.
```
API/Persona/GetCustomersByDirection/Giron/piedecuesta
```
```
    public async Task<IEnumerable<Persona>> GetCustomersByDirection(string direction1, string direction2)
    {
        return await _context.Personas 
                        .Include(p => p.TPersonaNavigation)   
                        .Where(p => p.TPersonaNavigation.Descripcion.ToUpper() == "EMPLEADO" &&
                                    p.Direcciones.Any(d => d.Barrio.ToUpper() == direction1.ToUpper() ||
                                                           d.Barrio.ToUpper() == direction2.ToUpper() ))
                        .ToListAsync();
        }
```
6) Listar todos clientes que tengan mas de X años ade antiguedad.
```
API/Persona/GetCustomersByantiquity/{quantity}
```
7) Listar Todos los contratos cuyo estado es activo. Se debe mostrar el Nro de contrato el nombre del cliente y el empleado que registro el contrato.
```
/API/Contrato/GetContractByStatus
```
```
    public async Task<IEnumerable<Contrato>> GetContractByStatus()
    {
        return await _context.Contratos 
                        .Include(c => c.ClienteNavigation)
                        .Include(c => c.EmpleadoNavigation)
                        .Where(p => p.EstadoNavigation.Descripcion.ToUpper() == "ACTIVO")
                        .ToListAsync();
    }
```


### Uso de Json Web Token
Ya que no se cargan usuarios en la base de datos por medio de csvs, es necesesario crearlo. Por defecto el rol de usuario va ser Empleado, el cual puede hacer peticiones a todo el CRUD menos a los enpoints especiales. Cuando se prueben los endpoints es necesario que el usuario tenga el rol de Administrador el cual se le asigna por medio del addrole.

Los datos necesarios para poder hacer post a los endpoints de JWT y en general se encuetran más facilmente en el Swagger, que se incializa por medio de dotnet watch run.

Nota: He tenido inconvenientes con la autorización, ya que, en vez de lanzarme la respuesta 401 o 403, me lanza 404 Not found, pero si le pasamos en token igualmente funcionará.

Si el token caduca, el programa esta diseñado para que por medio de su refresh token pueda generar otro.

creamos la cookie y apartir de ese refresh token se generan nuevos tokens
Si el token expira podemos generar cuantos queramos con el mismo refresh token. Si el refresh token expira se debe generar otro token desde el endpoint token.

- Duración del refresh token: 1 hora
- Duración del token de acceso: 2 minuto
Si se presentan inconvenientes a la hora de generar un nuevo token desde el refresh token hay que borrar las cookies.
