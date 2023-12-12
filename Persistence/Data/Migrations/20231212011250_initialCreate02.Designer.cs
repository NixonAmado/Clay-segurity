﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistencia.Data;

#nullable disable

namespace Persistence.Data.Migrations
{
    [DbContext(typeof(DbAppContext))]
    [Migration("20231212011250_initialCreate02")]
    partial class initialCreate02
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Domain.Entities.CategoriaPersona", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)");

                    b.HasKey("Id");

                    b.ToTable("Categoria_persona", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Ciudad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Departamento_id")
                        .HasColumnType("int");

                    b.Property<string>("NombreCiudad")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Nombre_Ciudad");

                    b.HasKey("Id");

                    b.HasIndex("Departamento_id");

                    b.ToTable("Ciudad", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.ContactoPersona", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)");

                    b.Property<int>("Persona_id")
                        .HasColumnType("int");

                    b.Property<int>("TContacto_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Persona_id");

                    b.ToTable("Contacto_persona", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Contrato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Cliente_id")
                        .HasColumnType("int");

                    b.Property<int?>("EmpleadoNavigationId")
                        .HasColumnType("int");

                    b.Property<int>("Empleado_id")
                        .HasColumnType("int");

                    b.Property<int>("Estado_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaContrato")
                        .HasColumnType("DATETIME");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("DATETIME");

                    b.HasKey("Id");

                    b.HasIndex("Cliente_id");

                    b.HasIndex("EmpleadoNavigationId");

                    b.HasIndex("Estado_id");

                    b.ToTable("Contrato", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Departamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("NombreDep")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Nombre_Departamento");

                    b.Property<int>("Pais_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Pais_id");

                    b.ToTable("Departamento", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Direccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Barrio")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Calle")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Carrera")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<int>("Persona_id")
                        .HasColumnType("int");

                    b.Property<int?>("TDireccionNavigationId")
                        .HasColumnType("int");

                    b.Property<int>("TDireccion_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TDireccionNavigationId");

                    b.HasIndex("TDireccion_id");

                    b.ToTable("Direccion", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Estado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Estado", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Pais", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("NombrePais")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Nombre_Pais");

                    b.HasKey("Id");

                    b.ToTable("Pais", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Persona", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CategoriaPersona_id")
                        .HasColumnType("int");

                    b.Property<int>("Ciudad_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateReg")
                        .HasMaxLength(100)
                        .HasColumnType("DATETIME");

                    b.Property<int>("IdTPersona")
                        .HasColumnType("int");

                    b.Property<string>("Id_Persona")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("id_Persona");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaPersona_id");

                    b.HasIndex("Ciudad_id");

                    b.HasIndex("IdTPersona");

                    b.ToTable("Persona", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Programacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Contrato_id")
                        .HasColumnType("int");

                    b.Property<int>("Empleado_id")
                        .HasColumnType("int");

                    b.Property<int>("Turno_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Contrato_id");

                    b.HasIndex("Empleado_id");

                    b.HasIndex("Turno_id");

                    b.ToTable("Programacion", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("DateTime");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("DateTime");

                    b.Property<int>("IdUserFk")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Revoked")
                        .HasColumnType("DateTime");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.HasKey("Id");

                    b.HasIndex("IdUserFk");

                    b.ToTable("RefreshToken", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.HasKey("Id");

                    b.ToTable("Role", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Administrador"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Empleado"
                        });
                });

            modelBuilder.Entity("Domain.Entities.TipoContacto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)");

                    b.HasKey("Id");

                    b.ToTable("Tipo_contacto", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.TipoDireccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)");

                    b.HasKey("Id");

                    b.ToTable("Tipo_direccion", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.TipoPersona", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)");

                    b.HasKey("Id");

                    b.ToTable("Tipo_persona", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Turno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<TimeOnly>("HoraTurnoF")
                        .HasColumnType("TIME");

                    b.Property<TimeOnly>("HoraTurnoI")
                        .HasColumnType("TIME");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)");

                    b.HasKey("Id");

                    b.ToTable("Turno", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("user_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("password");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.UserRole", b =>
                {
                    b.Property<int>("IdRolFk")
                        .HasColumnType("int");

                    b.Property<int>("IdUserFk")
                        .HasColumnType("int");

                    b.HasKey("IdRolFk", "IdUserFk");

                    b.HasIndex("IdUserFk");

                    b.ToTable("UsersRoles");
                });

            modelBuilder.Entity("Domain.Entities.Ciudad", b =>
                {
                    b.HasOne("Domain.Entities.Departamento", "DepartamentoNavigation")
                        .WithMany("Ciudades")
                        .HasForeignKey("Departamento_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DepartamentoNavigation");
                });

            modelBuilder.Entity("Domain.Entities.ContactoPersona", b =>
                {
                    b.HasOne("Domain.Entities.Persona", "PersonaNavigation")
                        .WithMany("Contactos")
                        .HasForeignKey("Persona_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.TipoContacto", "TContactoNavigation")
                        .WithMany("Contactos")
                        .HasForeignKey("Persona_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PersonaNavigation");

                    b.Navigation("TContactoNavigation");
                });

            modelBuilder.Entity("Domain.Entities.Contrato", b =>
                {
                    b.HasOne("Domain.Entities.Persona", "ClienteNavigation")
                        .WithMany("Contratos")
                        .HasForeignKey("Cliente_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Persona", "EmpleadoNavigation")
                        .WithMany()
                        .HasForeignKey("EmpleadoNavigationId");

                    b.HasOne("Domain.Entities.Estado", "EstadoNavigation")
                        .WithMany("Contratos")
                        .HasForeignKey("Estado_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClienteNavigation");

                    b.Navigation("EmpleadoNavigation");

                    b.Navigation("EstadoNavigation");
                });

            modelBuilder.Entity("Domain.Entities.Departamento", b =>
                {
                    b.HasOne("Domain.Entities.Pais", "PaisNavigation")
                        .WithMany("Departamentos")
                        .HasForeignKey("Pais_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaisNavigation");
                });

            modelBuilder.Entity("Domain.Entities.Direccion", b =>
                {
                    b.HasOne("Domain.Entities.TipoDireccion", "TDireccionNavigation")
                        .WithMany("Direcciones")
                        .HasForeignKey("TDireccionNavigationId");

                    b.HasOne("Domain.Entities.Persona", "PersonaNavigation")
                        .WithMany("Direcciones")
                        .HasForeignKey("TDireccion_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PersonaNavigation");

                    b.Navigation("TDireccionNavigation");
                });

            modelBuilder.Entity("Domain.Entities.Persona", b =>
                {
                    b.HasOne("Domain.Entities.CategoriaPersona", "CPersonaNavigation")
                        .WithMany("Personas")
                        .HasForeignKey("CategoriaPersona_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Ciudad", "CiudadNavigation")
                        .WithMany("Personas")
                        .HasForeignKey("Ciudad_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.TipoPersona", "TPersonaNavigation")
                        .WithMany("Personas")
                        .HasForeignKey("IdTPersona")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CPersonaNavigation");

                    b.Navigation("CiudadNavigation");

                    b.Navigation("TPersonaNavigation");
                });

            modelBuilder.Entity("Domain.Entities.Programacion", b =>
                {
                    b.HasOne("Domain.Entities.Contrato", "ContratoNavigation")
                        .WithMany("Programaciones")
                        .HasForeignKey("Contrato_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Persona", "EmpleadoNavigation")
                        .WithMany("Programaciones")
                        .HasForeignKey("Empleado_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Turno", "TurnoNavigation")
                        .WithMany("Programaciones")
                        .HasForeignKey("Turno_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ContratoNavigation");

                    b.Navigation("EmpleadoNavigation");

                    b.Navigation("TurnoNavigation");
                });

            modelBuilder.Entity("Domain.Entities.RefreshToken", b =>
                {
                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("IdUserFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.UserRole", b =>
                {
                    b.HasOne("Domain.Entities.Role", "Role")
                        .WithMany("UsersRoles")
                        .HasForeignKey("IdRolFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("UsersRoles")
                        .HasForeignKey("IdUserFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.CategoriaPersona", b =>
                {
                    b.Navigation("Personas");
                });

            modelBuilder.Entity("Domain.Entities.Ciudad", b =>
                {
                    b.Navigation("Personas");
                });

            modelBuilder.Entity("Domain.Entities.Contrato", b =>
                {
                    b.Navigation("Programaciones");
                });

            modelBuilder.Entity("Domain.Entities.Departamento", b =>
                {
                    b.Navigation("Ciudades");
                });

            modelBuilder.Entity("Domain.Entities.Estado", b =>
                {
                    b.Navigation("Contratos");
                });

            modelBuilder.Entity("Domain.Entities.Pais", b =>
                {
                    b.Navigation("Departamentos");
                });

            modelBuilder.Entity("Domain.Entities.Persona", b =>
                {
                    b.Navigation("Contactos");

                    b.Navigation("Contratos");

                    b.Navigation("Direcciones");

                    b.Navigation("Programaciones");
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.Navigation("UsersRoles");
                });

            modelBuilder.Entity("Domain.Entities.TipoContacto", b =>
                {
                    b.Navigation("Contactos");
                });

            modelBuilder.Entity("Domain.Entities.TipoDireccion", b =>
                {
                    b.Navigation("Direcciones");
                });

            modelBuilder.Entity("Domain.Entities.TipoPersona", b =>
                {
                    b.Navigation("Personas");
                });

            modelBuilder.Entity("Domain.Entities.Turno", b =>
                {
                    b.Navigation("Programaciones");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("RefreshTokens");

                    b.Navigation("UsersRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
