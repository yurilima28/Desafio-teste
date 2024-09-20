using Intelectah.Dapper;
using Intelectah.Models;
using Intelectah.ViewModel;


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
            if (!ValidarCPF(cliente.CPF))
            {
                throw new ArgumentException("O CPF informado é inválido.");
            }

            if (CPFExiste(cliente.CPF))
            {
                throw new ArgumentException("Já existe um cliente com este CPF.");
            }

            if (EmailExiste(cliente.Email))
            {
                throw new ArgumentException("Já existe um cliente com este email.");
            }

            _bancoContext.Clientes.Add(cliente);
            _bancoContext.SaveChanges();
            return cliente;
        }

        public ClientesModel Atualizar(ClientesModel cliente)
        {
            if (!ValidarCPF(cliente.CPF))
            {
                throw new ArgumentException("O CPF informado é inválido.");
            }

            if (CPFExiste(cliente.CPF, cliente.ClienteID))
            {
                throw new ArgumentException("Já existe um cliente com este CPF.");
            }

            if (EmailExiste(cliente.Email, cliente.ClienteID))
            {
                throw new ArgumentException("Já existe um cliente com este email.");
            }

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

        public bool EmailExiste(string email, int? clienteId = null)
        {
            return _bancoContext.Clientes.Any(u => u.Email == email && u.ClienteID != clienteId);
        }

        private bool ValidarCPF(string cpf)
        {

            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11)
            {
                return false;
            }

            if (cpf.All(c => c == cpf[0]))
            {
                return false;
            }

            int soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += (cpf[i] - '0') * (10 - i);
            }
            int primeiroDigito = 11 - (soma % 11);
            primeiroDigito = (primeiroDigito >= 10) ? 0 : primeiroDigito;

            if (primeiroDigito != (cpf[9] - '0'))
            {
                return false;
            }

            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += (cpf[i] - '0') * (11 - i);
            }
            int segundoDigito = 11 - (soma % 11);
            segundoDigito = (segundoDigito >= 10) ? 0 : segundoDigito;

            return segundoDigito == (cpf[10] - '0');

        }
    }
}
