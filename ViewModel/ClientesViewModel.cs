using System.ComponentModel.DataAnnotations;

namespace Intelectah.ViewModel
{
    public class ClientesViewModel
    {
        public int ClienteID { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        public string Email { get; set; }
        public bool IsDeleted { get; set; }

        public ClientesViewModel() { }
        public ClientesViewModel(int clienteID, string nome, string cpf, string telefone, string email, bool isDeleted = false)
        {
            ClienteID = clienteID;
            Nome = nome;
            CPF = cpf;
            Telefone = telefone;
            Email = email; 
            IsDeleted = isDeleted;
        }
    }
}
