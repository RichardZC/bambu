namespace Hra.Application.DTO
{
    public class ListarMensajeDto
    {

        public int MensajeId { get; set; }
        public DateTime Fecha { get; set; }
        public string? Nivel { get; set; }
        public string? Nota { get; set; }
    }
}
