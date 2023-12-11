using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ContactoPersona :BaseEntity
    {
        public string Descripcion {get;set;}
        public int Persona_id {get;set;}
        public virtual Persona PersonaNavigation {get;set;}
        public int TContacto_id {get;set;}
        public virtual TipoContacto TContactoNavigation {get;set;}
    
    }
}