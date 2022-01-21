using System;
using System.Collections.Generic;

namespace Hra.Domain.Entity
{
    public partial class Persona
    {
        public Persona()
        {
            Cliente = new HashSet<Cliente>();
            MovimientoCaja = new HashSet<MovimientoCaja>();
            Usuario = new HashSet<Usuario>();
        }

        public int PersonaId { get; set; }
        public string Nombre { get; set; } = null!;
        public string? ApePaterno { get; set; }
        public string? ApeMaterno { get; set; }
        public string? NombreCompleto { get; set; }
        public string TipoDocumento { get; set; } = null!;
        public string NumeroDocumento { get; set; } = null!;
        public string? Codigo { get; set; }
        public string? Sexo { get; set; }
        public string TipoPersona { get; set; } = null!;
        public string? Email { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Direccion { get; set; }
        public string? DireccionRef { get; set; }
        public bool Estado { get; set; }
        public string? Celular { get; set; }
        public string? Conyugue { get; set; }
        public string? ConyugueDni { get; set; }
        public string? ConyugueCelular { get; set; }
        public int? EstadoCivilId { get; set; }
        public int? DistritoId { get; set; }

        public virtual ICollection<Cliente> Cliente { get; set; }
        public virtual ICollection<MovimientoCaja> MovimientoCaja { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
