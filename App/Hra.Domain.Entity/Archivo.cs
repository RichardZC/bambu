using System;
using System.Collections.Generic;

namespace Hra.Domain.Entity
{
    public partial class Archivo
    {
        public int ArchivoId { get; set; }
        public int MiembroId { get; set; }
        public DateTime Fecha { get; set; }
        public string Nombre { get; set; } = null!;
        public int EvidenciaId { get; set; }

        public virtual Miembro Miembro { get; set; } = null!;
    }
}
