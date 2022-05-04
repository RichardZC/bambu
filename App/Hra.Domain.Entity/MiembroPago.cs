using System;
using System.Collections.Generic;

namespace Hra.Domain.Entity
{
    public partial class MiembroPago
    {
        public int MiembroPagoId { get; set; }
        public int MiembroId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Importe { get; set; }
        public bool IndPago { get; set; }

        public virtual Miembro Miembro { get; set; } = null!;
    }
}
