using System;
using System.Collections.Generic;

namespace Hra.Domain.Entity
{
    public partial class BovedaMov
    {
        public int MovimientoBovedaId { get; set; }
        public int BovedaId { get; set; }
        public string CodOperacion { get; set; } = null!;
        public string? Glosa { get; set; }
        public decimal Importe { get; set; }
        public bool IndEntrada { get; set; }
        public bool Estado { get; set; }
        public int? CajaDiarioId { get; set; }
        public int UsuarioRegId { get; set; }
        public DateTime FechaReg { get; set; }

        public virtual Boveda Boveda { get; set; } = null!;
    }
}
