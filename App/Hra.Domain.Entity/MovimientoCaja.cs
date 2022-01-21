using System;
using System.Collections.Generic;

namespace Hra.Domain.Entity
{
    public partial class MovimientoCaja
    {
        public MovimientoCaja()
        {
            MovimientoCajaAnu = new HashSet<MovimientoCajaAnu>();
        }

        public int MovimientoCajaId { get; set; }
        public int CajaDiarioId { get; set; }
        public string Operacion { get; set; } = null!;
        public decimal ImportePago { get; set; }
        public int? PersonaId { get; set; }
        public string? Descripcion { get; set; }
        public bool IndEntrada { get; set; }
        public bool Estado { get; set; }
        public int UsuarioRegId { get; set; }
        public DateTime FechaReg { get; set; }

        public virtual CajaDiario CajaDiario { get; set; } = null!;
        public virtual Persona? Persona { get; set; }
        public virtual ICollection<MovimientoCajaAnu> MovimientoCajaAnu { get; set; }
    }
}
