using System;
using System.Collections.Generic;

namespace Hra.Domain.Entity
{
    public partial class MovimientoCajaAnu
    {
        public int MovimientoCajaAnuId { get; set; }
        public int? MovimientoCajaId { get; set; }
        public string? Observacion { get; set; }
        public int? UsuarioRegId { get; set; }
        public DateTime? FechaReg { get; set; }

        public virtual MovimientoCaja? MovimientoCaja { get; set; }
    }
}
