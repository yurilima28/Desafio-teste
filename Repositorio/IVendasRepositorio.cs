using Intelectah.Models;

namespace Intelectah.Repositorio
{
    public interface IVendasRepositorio
    {
        VendasModel ListarPorId(int id, bool incluirExcluidos = false);
        List<VendasModel> BuscarTodos(bool incluirExcluidos = false);
        VendasModel Adicionar(VendasModel venda);
        VendasModel Atualizar(VendasModel venda);
        bool Apagar(int id);
        bool Restaurar(int id);
        VendasModel ObterPorProtocolo(string protocoloVenda);
        bool VerificarProtocoloUnico(string protocoloVenda);
    }
}
