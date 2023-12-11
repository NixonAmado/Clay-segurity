using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Programacion : BaseEntity
    {
        public int Contrato_id { get; set; }
        public virtual Contrato ContratoNavigation { get; set; }
        public int Turno_id { get; set; }
        public virtual Turno TurnoNavigation { get; set; }
        public int Empleado_id { get; set; }
        public virtual Persona EmpleadoNavigation { get; set; }
    
    }
}