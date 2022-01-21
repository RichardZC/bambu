using System;
using System.Collections.Generic;

namespace Hra.Domain.Entity
{
    public partial class Cliente
    {
        public int ClienteId { get; set; }
        public int PersonaId { get; set; }
        public string? Calificacion { get; set; }
        public bool Estado { get; set; }
        public string? Nota { get; set; }
        public string? DireccionNegocio { get; set; }
        public string? DireccionNegocioRef { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public int UsuarioRegId { get; set; }
        public bool Bloqueado { get; set; }

        public virtual Persona Persona { get; set; } = null!;
    }
}
