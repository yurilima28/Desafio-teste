using Intelectah.Models;

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
        bool CPFExiste(string cpf, int? clienteId = null );
        bool EmailExiste(string email, int? clienteID = null);
    }
}
