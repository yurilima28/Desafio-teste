using Intelectah.Dapper;
using Intelectah.Models;
using Microsoft.EntityFrameworkCore;

namespace Intelectah.Repositorio
{
    public class VendasRepositorio : IVendasRepositorio
    {
        private readonly BancoContext _bancoContext;

        public VendasRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public VendasModel ListarPorId(int id, bool incluirExcluidos = false)
        {
            IQueryable<VendasModel> query = _bancoContext.Vendas;

            if (!incluirExcluidos)
            {
                query = query.Where(v => !v.IsDeleted);
            }

            return query
                .Include(v => v.Cliente)
                .Include(v => v.Usuario)
                .Include(v => v.Concessionaria)
                .Include(v => v.Fabricante)
                .Include(v => v.Veiculo)
                .FirstOrDefault(v => v.VendaId == id);
        }

        public List<VendasModel> BuscarTodos(bool incluirExcluidos = false)
        {
            IQueryable<VendasModel> query = _bancoContext.Vendas;

            if (!incluirExcluidos)
            {
                query = query.Where(v => !v.IsDeleted);
            }

            return query
                .Include(v => v.Cliente)
                .Include(v => v.Usuario)
                .Include(v => v.Concessionaria)
                .Include(v => v.Fabricante)
                .Include(v => v.Veiculo)
                .ToList();
        }

        public VendasModel Adicionar(VendasModel venda)
        {
            _bancoContext.Vendas.Add(venda);
            _bancoContext.SaveChanges();
            return venda;
        }

        public VendasModel Atualizar(VendasModel venda)
        {
            _bancoContext.Vendas.Update(venda);
            _bancoContext.SaveChanges();
            return venda;
        }

        public bool Apagar(int id)
        {
            var venda = ListarPorId(id);
            if (venda != null)
            {
                venda.IsDeleted = true;
                _bancoContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Restaurar(int id)
        {
            var venda = ListarPorId(id, incluirExcluidos: true);
            if (venda != null && venda.IsDeleted)
            {
                venda.IsDeleted = false;
                _bancoContext.SaveChanges();
                return true;
            }
            return false;
        }

        public VendasModel ObterPorProtocolo(string protocoloVenda)
        {
            return _bancoContext.Vendas
                .FirstOrDefault(v => v.ProtocoloVenda == protocoloVenda && !v.IsDeleted);
        }

        public bool VerificarProtocoloUnico(string protocoloVenda)
        {
            return !_bancoContext.Vendas.Any(v => v.ProtocoloVenda == protocoloVenda && !v.IsDeleted);
        }
    }
}
