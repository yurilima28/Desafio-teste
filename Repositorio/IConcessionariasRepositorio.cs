using Intelectah.Models;
using System.Collections.Generic;

namespace Intelectah.Repositorio
{
    public interface IConcessionariasRepositorio
    {
        ConcessionariasModel ListarPorId(int id, bool incluirExcluidos = false);
        List<ConcessionariasModel> BuscarTodos(bool incluirExcluidos = false);
        ConcessionariasModel Adicionar(ConcessionariasModel concessionaria);
        ConcessionariasModel Atualizar(ConcessionariasModel concessionaria);
        bool Apagar(int id); 
        bool Restaurar(int id); 
        ConcessionariasModel ObterPorNome(string nomeConcessionaria);
        bool VerificarNomeConcessionariaUnico(string nomeConcessionaria, int? concessionariaID = null);
    }
}
