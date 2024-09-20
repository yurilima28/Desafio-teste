using System.ComponentModel.DataAnnotations;

namespace Intelectah.ViewModel
{
    public class ConcessionariasViewModel
    {
        public int ConcessionariaID { get; set; }

        [Required(ErrorMessage = "O nome da concessionária é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O Endereço da concessionária é obrigatório")]
        public EnderecoViewModel Endereco { get; set; } = new EnderecoViewModel();

        [Required(ErrorMessage = "O telefone da concessionária é obrigatório")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O E-mail da concessionária é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A capacidade máxima da concessionária é obrigatório")]
        public int CapacidadeMax { get; set; }
        public bool IsDeleted { get; set; }

        public ConcessionariasViewModel()
        {
        }

        public ConcessionariasViewModel(int concessionariaID, string nome, string telefone, string email, int capacidadeMax, bool isDeleted = false)
        {
            ConcessionariaID = concessionariaID;
            Nome = nome;
            Telefone = telefone;
            Email = email;
            CapacidadeMax = capacidadeMax;
            IsDeleted = isDeleted;
            Endereco = new EnderecoViewModel();
        }
    }

    public class EnderecoViewModel
    {
        [Required(ErrorMessage = "O endereço completo é obrigatório")]
        public string EnderecoCompleto { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O estado é obrigatório")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório")]
        public string CEP { get; set; }
    }
}
