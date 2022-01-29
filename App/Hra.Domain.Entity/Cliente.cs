using System;
using System.Collections.Generic;

namespace Hra.Domain.Entity
{
    public partial class Cliente
    {
        public int ClienteId { get; set; }
        public int PersonaId { get; set; }
        public string Apodo { get; set; } = null!;
        public int? WariBasico { get; set; }
        public int? WariAvanzado { get; set; }
        public int? WariPl1 { get; set; }
        public int? WariPl2 { get; set; }
        public int? WariPl3 { get; set; }
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
        public bool Bloqueado { get; set; }
        public bool Estado { get; set; }

        public virtual Persona Persona { get; set; } = null!;
    }
}
