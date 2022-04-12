using System;
using System.Collections.Generic;

namespace Hra.Domain.Entity
{
    public partial class Grupo
    {
        public Grupo()
        {
            Miembro = new HashSet<Miembro>();
        }

        public int GrupoId { get; set; }
        public string Denominacion { get; set; } = null!;
        public DateTime? FechaInicio { get; set; }
        public int TallerId { get; set; }

        public virtual ICollection<Miembro> Miembro { get; set; }
    }
}
