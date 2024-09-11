using Intelectah.Dapper;
using Intelectah.Models;


namespace Intelectah.Repositorio
{
    public class ClientesRepositorio : IClientesRepositorio
    {
        private readonly BancoContext _bancoContext;

        public ClientesRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public ClientesModel ListarPorId(int id, bool incluirExcluidos = false)
        {
            IQueryable<ClientesModel> query = _bancoContext.Clientes;

            if (!incluirExcluidos)
            {
                query = query.Where(c => !c.IsDeleted);
            }

            return query.FirstOrDefault(c => c.ClienteID == id);
        }

        public List<ClientesModel> BuscarTodos(bool incluirExcluidos = false)
        {
            IQueryable<ClientesModel> query = _bancoContext.Clientes;

            if (!incluirExcluidos)
            {
                query = query.Where(c => !c.IsDeleted);
            }

            return query.ToList();
        }

        public ClientesModel Adicionar(ClientesModel cliente)
        {
            _bancoContext.Clientes.Add(cliente);
            _bancoContext.SaveChanges();
            return cliente;
        }

        public ClientesModel Atualizar(ClientesModel cliente)
        {
            _bancoContext.Clientes.Update(cliente);
            _bancoContext.SaveChanges();
            return cliente;
        }

        public bool Apagar(int id)
        {
            var cliente = ListarPorId(id);
            if (cliente != null)
            {
                cliente.IsDeleted = true; 
                _bancoContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Restaurar(int id)
        {
            var cliente = ListarPorId(id, incluirExcluidos: true);
            if (cliente != null && cliente.IsDeleted)
            {
                cliente.IsDeleted = false; 
                _bancoContext.SaveChanges();
                return true;
            }
            return false;
        }

        public ClientesModel ObterPorNome(string nomeCliente)
        {
            return _bancoContext.Clientes.FirstOrDefault(c => c.Nome == nomeCliente && !c.IsDeleted);
        }

        public bool CPFExiste(string cpf, int? clienteId = null)
        {
            return _bancoContext.Clientes.Any(c => c.CPF == cpf && (!clienteId.HasValue || c.ClienteID != clienteId.Value));
        }
    }
}
