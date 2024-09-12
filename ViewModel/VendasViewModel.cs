using Intelectah.Models;

namespace Intelectah.ViewModel
{
    public class VendasViewModel
    {
        public int VendaId { get; set; }
        public int ClienteID { get; set; }
        public DateTime DataVenda { get; set; } = DateTime.Now;
        public decimal ValorTotal { get; set; }
        public int UsuarioID { get; set; }
        public int ConcessionariaID { get; set; }
        public int FabricanteID { get; set; }
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
