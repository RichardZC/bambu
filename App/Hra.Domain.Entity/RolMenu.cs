using System;
using System.Collections.Generic;

namespace Hra.Domain.Entity
{
    public partial class RolMenu
    {
        public int RolMenuId { get; set; }
        public int? RolId { get; set; }
        public int? MenuId { get; set; }

        public virtual Menu? Menu { get; set; }
        public virtual Rol? Rol { get; set; }
    }
}
