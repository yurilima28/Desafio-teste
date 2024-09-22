using System.ComponentModel.DataAnnotations;

namespace Intelectah.ViewModel
{
    public class VendasViewModel
    {
        public int VendaId { get; set; }
        [Required(ErrorMessage ="Nome do clinte é obrigatório")]
        public int ClienteID { get; set; }
        public DateTime DataVenda { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "O valor da venda é obrigatório")]
        public decimal ValorTotal { get; set; }
        [Required(ErrorMessage = "Nome do vendedor é obrigatório")]
        public int UsuarioID { get; set; }
        [Required(ErrorMessage = "Nome da concessionária é obrigatório")]
        public int ConcessionariaID { get; set; }
        [Required(ErrorMessage = "Nome do fabricante é obrigatório")]
        public int FabricanteID { get; set; }
        [Required(ErrorMessage = "Modelo do veículo é obrigatório")]
        public int VeiculoID { get; set; }
        public string ProtocoloVenda { get; set; }
        public bool IsDeleted { get; set; }

        public string? NomeCliente { get; set; }
        public string? NomeUsario { get; set; }
        public string? NomeConcessionaria { get; set; }
        public string? NomeFabricante { get; set; }
        public string? NomeVeiculo { get; set; }

        public VendasViewModel()
        {
        }
        public VendasViewModel(int vendaId, int clienteId, DateTime dataVenda, decimal valorTotal, int usuarioId, string nomeCliente, string nomeUsuario, string nomeFabricante, string nomeConcessionaria, int fabricanteId, string protocoloVenda, int veiculoID, string nomeVeiculo, bool isDeleted = false)
        {
            VendaId = vendaId;
            ClienteID = clienteId;
            NomeCliente = nomeCliente;
            DataVenda = dataVenda;
            ValorTotal = valorTotal;
            UsuarioID = usuarioId;
            NomeUsario = nomeUsuario;
            FabricanteID = fabricanteId;
            NomeFabricante = nomeFabricante;
            ProtocoloVenda = protocoloVenda;
            VeiculoID = veiculoID;
            NomeVeiculo = nomeVeiculo;
            IsDeleted = isDeleted;
        }
    }
}
