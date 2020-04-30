namespace Votador.Site.Models
{
    public class VotoViewModel
    {
        public int Id { get; set; }
        public string UsuarioEmail { get; set; }
        public string Comentario { get; set; }

        public RecursoViewModel Recurso { get; set; }
    }
}
