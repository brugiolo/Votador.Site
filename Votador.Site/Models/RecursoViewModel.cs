namespace Votador.Site.Models
{
    public class RecursoViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
    }

    public class RecursoVotosViewModel
    {
        public RecursoViewModel Recurso { get; set; }
        public int NumeroDeVotos { get; set; }
    }
}
