using System;
using System.Collections.Generic;

namespace Hra.Domain.Entity
{
    public partial class Ocupacion
    {
        public int OcupacionId { get; set; }
        public string? Denominacion { get; set; }
        public bool? Estado { get; set; }
    }
}
