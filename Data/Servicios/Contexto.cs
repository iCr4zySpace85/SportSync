namespace SportSync.Data.Servicios
{
    public class Contexto
    {
        public Contexto(string valor)
        {
            Conexion = valor;

        }
        public string Conexion { get; }
    }
}
