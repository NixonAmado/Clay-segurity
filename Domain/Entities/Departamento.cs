using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Departamento : BaseEntity
    {
        public string NombreDep {get;set;}
        public int Pais_id {get;set;}
        public virtual Pais PaisNavigation {get;set;}
        public virtual ICollection<Ciudad> Ciudades {get;set;}

    }
}