using System;
using System.Collections.Generic;

namespace Hra.Domain.Entity
{
    public partial class Archivo
    {
        public int ArchivoId { get; set; }
        public int PersonaId { get; set; }
        public DateTime Fecha { get; set; }
        public string Archivo1 { get; set; } = null!;

        public virtual Persona Persona { get; set; } = null!;
    }
}
