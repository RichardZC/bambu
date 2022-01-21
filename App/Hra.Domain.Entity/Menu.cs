using System;
using System.Collections.Generic;

namespace Hra.Domain.Entity
{
    public partial class Menu
    {
        public Menu()
        {
            RolMenu = new HashSet<RolMenu>();
        }

        public int MenuId { get; set; }
        public string? Denominacion { get; set; }
        public string? Modulo { get; set; }
        public string? Url { get; set; }
        public string? Icono { get; set; }
        public bool? IndPadre { get; set; }
        public decimal? Orden { get; set; }
        public decimal? Referencia { get; set; }

        public virtual ICollection<RolMenu> RolMenu { get; set; }
    }
}
