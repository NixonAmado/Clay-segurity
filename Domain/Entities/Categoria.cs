using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CategoriaPersona : BaseEntity
    {
        public string Nombre { get; set; }
        public virtual ICollection<Persona> Personas { get;set;}
    }
}