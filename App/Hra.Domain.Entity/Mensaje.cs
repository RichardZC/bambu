using System;
using System.Collections.Generic;

namespace Hra.Domain.Entity
{
    public partial class Mensaje
    {
        public int MensajeId { get; set; }
        public int MiembroId { get; set; }
        public DateTime Fecha { get; set; }
        public string Nota { get; set; } = null!;
        public int NivelId { get; set; }

        public virtual Miembro Miembro { get; set; } = null!;
    }
}
