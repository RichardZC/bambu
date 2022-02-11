using System;
using System.Collections.Generic;

namespace Hra.Domain.Entity
{
    public partial class Grupo
    {
        public Grupo()
        {
            Cliente = new HashSet<Cliente>();
        }

        public int GrupoId { get; set; }
        public string Denominacion { get; set; } = null!;
        public DateTime? FechaInicio { get; set; }

        public virtual ICollection<Cliente> Cliente { get; set; }
    }
}
