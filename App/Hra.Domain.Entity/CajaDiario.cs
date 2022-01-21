using System;
using System.Collections.Generic;

namespace Hra.Domain.Entity
{
    public partial class CajaDiario
    {
        public CajaDiario()
        {
            MovimientoCaja = new HashSet<MovimientoCaja>();
        }

        public int CajaDiarioId { get; set; }
        public int CajaId { get; set; }
        public int UsuarioAsignadoId { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal Entradas { get; set; }
        public decimal Salidas { get; set; }
        public decimal SaldoFinal { get; set; }
        public DateTime FechaIniOperacion { get; set; }
        public DateTime? FechaFinOperacion { get; set; }
        public bool IndCierre { get; set; }
        public bool TransBoveda { get; set; }

        public virtual Caja Caja { get; set; } = null!;
        public virtual Usuario UsuarioAsignado { get; set; } = null!;
        public virtual ICollection<MovimientoCaja> MovimientoCaja { get; set; }
    }
}
