using System;
using System.Collections.Generic;

namespace Hra.Domain.Entity
{
    public partial class Cliente
    {
        public int ClienteId { get; set; }
        public int PersonaId { get; set; }
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
        public DateTime FechaReg { get; set; }
        public int UsuarioRegId { get; set; }
        public int Estado { get; set; }
        public bool Activo { get; set; }

        public virtual Grupo? Grupo { get; set; }
        public virtual Persona Persona { get; set; } = null!;
    }
}
