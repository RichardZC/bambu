namespace Hra.Application.DTO
{
    public class ListarMiembroDto
    {
        public int MiembroId { get; set; }
        public int PersonaId { get; set; }
        public string Dni { get; set; }
        public string Miembro { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public string Grupo { get; set; }
        public string Nivel { get; set; }
        public string Estado { get; set; }
        public int EstadoId { get; set; }
        public int NivelId { get; set; }
    }
}
