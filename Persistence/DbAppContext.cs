using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistencia.Data;

public class DbAppContext: DbContext
{
    public DbAppContext(DbContextOptions options) : base(options)
    {

    }
    public DbSet<Role> Roles{get;set;}
    public DbSet<User> Users {get;set;}
    public DbSet<UserRole> UsersRoles {get;set;}
    public DbSet<CategoriaPersona> CategoriaPersonas  {get;set;}
    public DbSet<Ciudad> Ciudades  {get;set;}
    public DbSet<ContactoPersona> Contactos  {get;set;}
    public DbSet<Contrato> Contratos  {get;set;}
    public DbSet<Departamento> Departamentos  {get;set;}
    public DbSet<Direccion> Direcciones  {get;set;}
    public DbSet<Estado> Estados  {get;set;}
    public DbSet<Pais> Paises  {get;set;}
    public DbSet<Persona> Personas  {get;set;}
    public DbSet<Programacion> Programaciones  {get;set;}
    public DbSet<TipoContacto> TipoContactos  {get;set;}
    public DbSet<TipoDireccion> TipoDirecciones  {get;set;}
    public DbSet<TipoPersona> TipoPersonas  {get;set;}
    public DbSet<Turno> Turnos  {get;set;}



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var roles = new[]
        {
            new Role { Id = 1, Description = "Administrador" },
            new Role { Id = 2, Description = "Empleado" },
            // Agrega otros roles según tus necesidades
        };

        modelBuilder.Entity<Role>().HasData(roles);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}