using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Direccion: BaseEntity
    {
        public string Calle {get;set;}
        public int Numero {get;set;}
        public string Carrera {get;set;}
        public string Barrio {get;set;}
        public int Persona_id {get;set;}
        public virtual Persona PersonaNavigation {get;set;}
        public int TDireccion_id {get;set;}
        public virtual TipoDireccion TDireccionNavigation {get;set;}
    }
}