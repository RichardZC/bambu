using System;
using System.Collections.Generic;

namespace Hra.Domain.Entity
{
    public partial class Caja
    {
        public Caja()
        {
            CajaDiario = new HashSet<CajaDiario>();
        }

        public int CajaId { get; set; }
        public string Denominacion { get; set; } = null!;
        public bool Estado { get; set; }
        public bool IndAbierto { get; set; }
        public int? CajeroId { get; set; }

        public virtual ICollection<CajaDiario> CajaDiario { get; set; }
    }
}
