namespace SportSync.Models.ViewModels
{
    public class PanelEquipoCoach
    {
        public string TorneoNombre { get; set; }
        public string Categoria { get; set; }
        public int NumeroJugadores { get; set; }
        public List<Jugadore> Jugadores { get; set; }
    }
}
