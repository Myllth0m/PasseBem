namespace WebMVC.Models
{
    public class CidadeViewModel
    {
        public CidadeViewModel(string nome, string uf, decimal temperaturaMinima, decimal temperaturaMaxima)
        {
            Nome = nome;
            UF = uf;
            TemperaturaMinima = temperaturaMinima;
            TemperaturaMaxima = temperaturaMaxima;
        }
        public CidadeViewModel()
        {

        }

        public string Nome { get; set; }
        public string UF { get; set; }
        public decimal TemperaturaMinima { get; set; }
        public decimal TemperaturaMaxima { get; set; }
    }
}