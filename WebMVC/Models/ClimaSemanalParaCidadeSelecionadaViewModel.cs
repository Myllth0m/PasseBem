namespace WebMVC.Models
{
    public class ClimaSemanalParaCidadeSelecionadaViewModel
    {
        public string NomeDaCidade { get; set; }
        public string DiaDaSemana { get; set; }
        public string TempoClimatico { get; set; }
        public decimal TemperaturaMinima { get; set; }
        public decimal TemperaturaMaxima { get; set; }
    }
}