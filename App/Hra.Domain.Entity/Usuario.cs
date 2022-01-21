using System;
using System.Collections.Generic;

namespace Hra.Domain.Entity
{
    public partial class Usuario
    {
        public Usuario()
        {
            CajaDiario = new HashSet<CajaDiario>();
        }

        public int UsuarioId { get; set; }
        public int PersonaId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual Persona Persona { get; set; } = null!;
        public virtual ICollection<CajaDiario> CajaDiario { get; set; }
    }
}
