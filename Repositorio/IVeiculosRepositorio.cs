using Intelectah.Models;

namespace Intelectah.Repositorio
{
    public interface IVeiculosRepositorio
    {
        VeiculosModel ListarPorId(int id, bool incluirExcluidos = false);
        List<VeiculosModel> BuscarTodos();
        VeiculosModel Adicionar(VeiculosModel veiculo);
        VeiculosModel Atualizar(VeiculosModel veiculo);
        bool Apagar(int id);
        bool Restaurar(int id); 
        List<VeiculosModel> BuscarPorFabricante(int fabricanteId);
        bool VerificarSeVeiculoVendido(int veiculoId);
        List<VeiculosModel> ObterModelosPorFabricante(int fabricanteId);
    }
}
