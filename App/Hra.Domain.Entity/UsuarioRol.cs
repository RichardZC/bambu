using System;
using System.Collections.Generic;

namespace Hra.Domain.Entity
{
    public partial class UsuarioRol
    {
        public int UsuarioRolId { get; set; }
        public int? UsuarioId { get; set; }
        public int? RolId { get; set; }

        public virtual Rol? Rol { get; set; }
        public virtual Usuario? Usuario { get; set; }
    }
}
