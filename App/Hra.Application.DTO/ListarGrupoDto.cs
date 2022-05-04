namespace Hra.Application.DTO
{
    public class ListarGrupoDto
    {
        public int GrupoId { get; set; }
        public string Denominacion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public int TallerId { get; set; }
        public decimal Costo { get; set; }
        public string Taller { get; set; }
    }
}
