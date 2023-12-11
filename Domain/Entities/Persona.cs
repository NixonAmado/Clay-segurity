using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Persona : BaseEntity
    {
        public string Id_Persona {get;set;}
        public string Nombre {get;set;}
        public DateTime DateReg {get;set;}
        public int IdTPersona {get;set;}
        public virtual TipoPersona TPersonaNavigation {get;set;}
        public int CategoriaPersona_id {get;set;}
        public virtual CategoriaPersona CPersonaNavigation {get;set;}
        public int Ciudad_id {get;set;}
        public virtual Ciudad CiudadNavigation {get;set;}
        public virtual ICollection<Direccion> Direcciones {get;set;}
        public virtual ICollection<ContactoPersona> Contactos {get;set;}
        public virtual ICollection<Contrato> Contratos {get;set;}
        public virtual ICollection<Programacion> Programaciones { get; set;}


    }
}