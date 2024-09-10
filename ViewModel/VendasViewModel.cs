using Intelectah.Models;

namespace Intelectah.ViewModel
{
    public class VendasViewModel
    {
        public int VendaId { get; set; }
        public int ClienteID { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal ValorTotal { get; set; }
        public int UsuarioID { get; set; }
        public int ConcessionariaID { get; set; }
        public int FabricanteID { get; set; }
        public int VeiculoID { get; set; }
        public string ProtocoloVenda { get; set; }
        public bool IsDeleted { get; set; }

        public VendasViewModel()
        {
        }
        public VendasViewModel(int vendaId, int clienteId, DateTime dataVenda, decimal valorTotal, int usuarioId, string nomeCliente, string nomeUsuario, string nomeConcessionaria, int fabricanteId, string protocoloVenda, bool isDeleted = false)
        {
            VendaId = vendaId;
            ClienteID = clienteId;
            DataVenda = dataVenda;
            ValorTotal = valorTotal;
            UsuarioID = usuarioId;
            FabricanteID = fabricanteId;
            ProtocoloVenda = protocoloVenda;
            IsDeleted = isDeleted;
        }
    }
}
