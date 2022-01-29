namespace Hra.App.Models
{
    public interface IConstante
    {
        public string UsaDominio { get; }
        public string Dominio { get; }
        public string Ldap { get; }
        public string TokenApiPeru { get; }

    }
    public class Constantes : IConstante
    {
        public string UsaDominio { get; set; }
        public string Dominio { get; set; }
        public string Ldap { get; set; }
        public string TokenApiPeru { get; set; }
    }
}
