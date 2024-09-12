using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Intelectah.Models
{
    public class VendasModel
    {
        [Key]
        public int VendaId { get; set; }

        [Required(ErrorMessage = "O cliente é obrigatório.")]
        [ForeignKey("Cliente")]
        public int ClienteID { get; set; }

        [Required(ErrorMessage = "A data da venda é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime DataVenda { get; set; }

        [Required(ErrorMessage = "O valor total é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor total deve ser maior que zero.")]
        [DataType(DataType.Currency)]
        public decimal ValorTotal { get; set; }

        [Required]
        [MaxLength(20)]
        public string ProtocoloVenda { get; set; }
        public bool IsDeleted { get; set; }

        public int UsuarioID { get; set; }
        public int ConcessionariaID { get; set; }
        public int FabricanteID { get; set; }
        public int VeiculoID { get; set; }

        public virtual UsuariosModel Usuario { get; set; }
        public virtual ClientesModel Cliente { get; set; }
        public virtual ConcessionariasModel Concessionaria { get; set; }
        public virtual VeiculosModel Veiculo { get; set; }
        public virtual FabricantesModel Fabricante { get; set; }

        public VendasModel()
        {
            ProtocoloVenda = GerarProtocoloVenda();
        }

        public VendasModel(int clienteID, decimal valorTotal, DateTime dataVenda, int usuarioID, int concessionarID, int fabricanteID, int veiculoID, string protocoloVenda, bool isDeleted = false)
        {
            ClienteID = clienteID;
            ValorTotal = valorTotal;
            DataVenda = dataVenda;
            UsuarioID = usuarioID;
            ConcessionariaID = concessionarID;
            FabricanteID = fabricanteID;
            VeiculoID = veiculoID;
            ProtocoloVenda = GerarProtocoloVenda();
            IsDeleted = isDeleted;
        }
        public string GerarProtocoloVenda()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }
}
