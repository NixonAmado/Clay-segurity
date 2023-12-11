using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Ciudad : BaseEntity
    {
        public string NombreCiudad { get; set;}
        public int Departamento_id { get; set; }
        public virtual Departamento DepartamentoNavigation {get;set;}
        public virtual ICollection<Persona> Personas { get; set; }
    }
}