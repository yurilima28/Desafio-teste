using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ValidationModel;

namespace Intelectah.Models
{
    public class ClientesModel
    {
        [Key]
        public int ClienteID { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [CPFValidation(ErrorMessage = "O CPF informado é inválido.")]
        public string CPF { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        [MaxLength(100, ErrorMessage = "O e-mail deve ter no máximo 100 caracteres.")]
        public string Email { get; set; }

        [Required]
        [MaxLength(16)]
        [Phone(ErrorMessage = "Número de telefone inválido.")]
        public string Telefone { get; set; }
        public bool IsDeleted { get; set; }


        public ICollection<VendasModel> Vendas { get; set; }

        public ClientesModel()
        {
            Vendas = new List<VendasModel>();
        }

        public ClientesModel(string nome, string cpf, string telefone, string email, bool isDeleted = false)
        {
            Nome = nome;
            CPF = cpf;
            Telefone = telefone;
            Email = email;
            Vendas = new List<VendasModel>();
            IsDeleted = isDeleted;
        }

    }
}
