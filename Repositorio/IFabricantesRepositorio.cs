using Intelectah.Models;

namespace Intelectah.Repositorio
{
    public interface IFabricantesRepositorio
    {
        FabricantesModel ListarPorId(int id, bool incluirExcluidos = false);
        List<FabricantesModel> BuscarTodos(bool incluirExcluidos = false);
        FabricantesModel Adicionar(FabricantesModel fabricante);
        FabricantesModel Atualizar(FabricantesModel fabricante);
        bool Apagar(int id); 
        bool Restaurar(int id); 
        FabricantesModel ObterPorNome(string nomeFabricante);
        bool VerificarNomeFabricanteUnico(string nomeFabricante, int? fabricanteID = null);
    }
}
