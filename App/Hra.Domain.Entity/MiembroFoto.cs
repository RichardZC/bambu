using System;
using System.Collections.Generic;

namespace Hra.Domain.Entity
{
    public partial class MiembroFoto
    {
        public int MiembroFotoId { get; set; }
        public int MiembroId { get; set; }
        public string Foto { get; set; } = null!;

        public virtual Miembro Miembro { get; set; } = null!;
    }
}
