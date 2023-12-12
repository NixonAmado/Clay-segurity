using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Contrato : BaseEntity
    {    
        public DateTime FechaContrato { get; set; }
        public DateTime FechaFin { get; set; }
        public int Cliente_id { get; set; }
        public virtual Persona ClienteNavigation { get; set; }
        public virtual Persona EmpleadoNavigation { get; set; }
        public int Estado_id { get; set; }
        public virtual Estado EstadoNavigation { get; set; }
        public virtual ICollection<Programacion> Programaciones { get; set;}
    }

}