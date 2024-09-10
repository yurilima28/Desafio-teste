using Intelectah.Models;
using System.Collections.Generic;

namespace Intelectah.Repositorio
{
    public interface IClientesRepositorio
    {
        ClientesModel ListarPorId(int id, bool incluirExcluidos = false);
        List<ClientesModel> BuscarTodos(bool incluirExcluidos = false);
        ClientesModel Adicionar(ClientesModel cliente);
        ClientesModel Atualizar(ClientesModel cliente);
        bool Apagar(int id);
        bool Restaurar(int id); 
        ClientesModel ObterPorNome(string nomeCliente);
        bool VerificarNomeClienteUnico(string nomeCliente, int? clienteID = null);
        bool VerificarCpfUnico(string cpf, int? clienteID = null);
    }
}
