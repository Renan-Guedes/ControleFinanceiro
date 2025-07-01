using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Web.ViewModels
{
    public class BancoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "* obrigatório")]
        [DisplayName("Nome do Banco")]
        public string Nome { get; set; } = string.Empty;

        public bool Ativo { get; set; } = true;
    }
}
