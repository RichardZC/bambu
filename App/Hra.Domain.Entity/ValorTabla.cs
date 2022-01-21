using System;
using System.Collections.Generic;

namespace Hra.Domain.Entity
{
    public partial class ValorTabla
    {
        public int TablaId { get; set; }
        public int ItemId { get; set; }
        public string? Denominacion { get; set; }
        public string? DesCorta { get; set; }
        public string? Valor { get; set; }
    }
}
