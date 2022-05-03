using System;
using System.Collections.Generic;

namespace Hra.Domain.Entity
{
    public partial class Miembro
    {
        public Miembro()
        {
            Archivo = new HashSet<Archivo>();
            Mensaje = new HashSet<Mensaje>();
        }

        public int MiembroId { get; set; }
        public int GrupoId { get; set; }
        public int NivelId { get; set; }
        public int PersonaId { get; set; }
        public DateTime Fecha { get; set; }
        public int EstadoId { get; set; }

        public virtual Grupo Grupo { get; set; } = null!;
        public virtual Persona Persona { get; set; } = null!;
        public virtual ICollection<Archivo> Archivo { get; set; }
        public virtual ICollection<Mensaje> Mensaje { get; set; }
    }
}
