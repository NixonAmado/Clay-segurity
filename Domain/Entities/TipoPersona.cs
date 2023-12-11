using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TipoPersona : BaseEntity
    {
        public string Descripcion { get; set; }
        public virtual ICollection<Persona> Personas {get;set;}
    }
}