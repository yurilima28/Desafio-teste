using System.ComponentModel.DataAnnotations;

namespace Intelectah.Models
{
    public class ConcessionariasModel
    {
        [Key]
        public int ConcessionariaID { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "O nome da concessionária deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "O endereço completo deve ter no máximo 255 caracteres.")]
        public string EnderecoCompleto { get; set; }

        [Required(ErrorMessage = "A cidade da concessionária é obrigatório")]

        [MaxLength(50, ErrorMessage = "A cidade deve ter no máximo 50 caracteres.")]

        public string Cidade { get; set; }

        [Required(ErrorMessage = "O estado da concessionária é obrigatório")]
        [MaxLength(50, ErrorMessage = "O estado deve ter no máximo 50 caracteres.")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "O CPE da concessionária é obrigatório")]
        [MaxLength(10, ErrorMessage = "O CEP deve ter no máximo 10 caracteres.")]
        public string CEP { get; set; }

        [Required(ErrorMessage = "O telefone da concessionária é obrigatório")]
        [MaxLength(15, ErrorMessage = "O telefone deve ter no máximo 15 caracteres.")]
        public string Telefone { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        [MaxLength(100, ErrorMessage = "O e-mail deve ter no máximo 100 caracteres.")]
        public string Email { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A capacidade máxima de veículos deve ser um valor positivo.")]
        public int CapacidadeMax{ get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<VendasModel> Vendas { get; set; }  
        public virtual ICollection<VeiculosModel> Veiculos { get; set; }
        public virtual ICollection<FabricantesModel> Fabricantes { get; set; }

        public ConcessionariasModel()
        {
            Veiculos = new HashSet<VeiculosModel>();
        }

        public ConcessionariasModel(int concessionariaID, string nome, string enderecoCompleto, string cidade, string estado, string cep, string telefone, string email, int capacidadeMax, bool isDeleted = false)
        {
            ConcessionariaID = concessionariaID;
            Nome = nome;
            EnderecoCompleto = enderecoCompleto;
            Cidade = cidade;
            Estado = estado;
            CEP = cep;
            Telefone = telefone;
            Email = email;
            CapacidadeMax = capacidadeMax;
            IsDeleted = isDeleted;
        }
    }
}
