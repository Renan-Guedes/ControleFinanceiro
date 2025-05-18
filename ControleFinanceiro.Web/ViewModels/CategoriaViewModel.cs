namespace ControleFinanceiro.Web.ViewModels
{
    public class CategoriaViewModel
    {
        public long Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string? Descricao { get; set; }

        public long UsuarioId { get; set; }
    }
}
