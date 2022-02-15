using System;
using System.Collections.Generic;

namespace Hra.Domain.Entity
{
    public partial class Persona
    {
        public Persona()
        {
            MovimientoCaja = new HashSet<MovimientoCaja>();
            Usuario = new HashSet<Usuario>();
        }

        public int PersonaId { get; set; }
        public string Nombre { get; set; } = null!;
        public string ApePaterno { get; set; } = null!;
        public string ApeMaterno { get; set; } = null!;
        public string NombreCompleto { get; set; } = null!;
        public string? Apodo { get; set; }
        public string NumeroDocumento { get; set; } = null!;
        public string Sexo { get; set; } = null!;
        public string? Email { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Direccion { get; set; }
        public string? DireccionRef { get; set; }
        public string? Celular { get; set; }
        public int? EstadoCivilId { get; set; }
        public string? Apoderado { get; set; }
        public string? ApoderadoDni { get; set; }
        public string? ApoderadoCelular { get; set; }
        public int? ApoderadoParentescoId { get; set; }
        public bool Activo { get; set; }
        public int NroHijos { get; set; }
        public string? CentroTrabajo { get; set; }
        public string? PuestoCargo { get; set; }
        public int? GrupoId { get; set; }
        public int? NivelId { get; set; }
        public int? PersonaReferenciaId { get; set; }
        public int? TipoAlimentacionId { get; set; }
        public string? AlergiaEnfermedad { get; set; }
        public bool IndTerapiaPsicologica { get; set; }
        public string? DxTerapiaPsicologica { get; set; }
        public DateTime? FechaTerapiaPsicologica { get; set; }
        public bool IndTerapiaPsiquiatrica { get; set; }
        public string? DxTerapiaPsiquiatrica { get; set; }
        public DateTime? FechaTerapiaPsiquiatrica { get; set; }
        public string? Nota { get; set; }
        public int? UsuarioRegId { get; set; }
        public DateTime? FechaReg { get; set; }
        public int EstadoId { get; set; }

        public virtual Grupo? Grupo { get; set; }
        public virtual ICollection<MovimientoCaja> MovimientoCaja { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
