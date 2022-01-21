using System;
using System.Collections.Generic;

namespace Hra.Domain.Entity
{
    public partial class TipoOperacion
    {
        public int TipoOperacionId { get; set; }
        public string Codigo { get; set; } = null!;
        public string Denominacion { get; set; } = null!;
        public bool IndEntrada { get; set; }
        public bool IndCajaDiario { get; set; }
        public bool IndBoveda { get; set; }
    }
}
