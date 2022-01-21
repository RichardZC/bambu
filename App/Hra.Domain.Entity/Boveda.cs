using System;
using System.Collections.Generic;

namespace Hra.Domain.Entity
{
    public partial class Boveda
    {
        public Boveda()
        {
            BovedaMov = new HashSet<BovedaMov>();
        }

        public int BovedaId { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal Entradas { get; set; }
        public decimal Salidas { get; set; }
        public decimal SaldoFinal { get; set; }
        public DateTime FechaIniOperacion { get; set; }
        public DateTime? FechaFinOperacion { get; set; }
        public bool IndCierre { get; set; }
        public bool IndTemporal { get; set; }

        public virtual ICollection<BovedaMov> BovedaMov { get; set; }
    }
}
