using System;
using System.Collections.Generic;

namespace Hra.Domain.Entity
{
    public partial class Rol
    {
        public Rol()
        {
            RolMenu = new HashSet<RolMenu>();
        }

        public int RolId { get; set; }
        public string? Denominacion { get; set; }
        public bool Estado { get; set; }

        public virtual ICollection<RolMenu> RolMenu { get; set; }
    }
}
