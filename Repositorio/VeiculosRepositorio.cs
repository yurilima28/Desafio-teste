using Intelectah.Dapper;
using Intelectah.Models;
using Microsoft.EntityFrameworkCore;


namespace Intelectah.Repositorio
{
    public class VeiculosRepositorio : IVeiculosRepositorio
    {
        private readonly BancoContext _bancoContext;

        public VeiculosRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public VeiculosModel ListarPorId(int id, bool incluirExcluidos = false)
        {
            IQueryable<VeiculosModel> query = _bancoContext.Veiculos
                .Include(v => v.Fabricante);

            if (!incluirExcluidos)
            {
                query = query.Where(v => !v.IsDeleted);
            }

            return query.FirstOrDefault(v => v.VeiculoID == id);
        }

        public List<VeiculosModel> BuscarTodos()
        {
            return _bancoContext.Veiculos
                .Include(v => v.Fabricante)
                .Where(v => !v.IsDeleted)
                .ToList();
        }

        public VeiculosModel Adicionar(VeiculosModel veiculo)
        {
            _bancoContext.Veiculos.Add(veiculo);
            _bancoContext.SaveChanges();
            return veiculo;
        }

        public VeiculosModel Atualizar(VeiculosModel veiculo)
        {
            _bancoContext.Veiculos.Update(veiculo);
            _bancoContext.SaveChanges();
            return veiculo;
        }

        public bool Apagar(int id)
        {
            var veiculo = ListarPorId(id);
            if (veiculo != null)
            {
                veiculo.IsDeleted = true;
                _bancoContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Restaurar(int id)
        {
            var veiculo = ListarPorId(id, incluirExcluidos: true);
            if (veiculo != null && veiculo.IsDeleted)
            {
                veiculo.IsDeleted = false;
                _bancoContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<VeiculosModel> BuscarPorFabricante(int fabricanteId)
        {
            return _bancoContext.Veiculos.Where(v => v.FabricanteID == fabricanteId).ToList();

        }

        public bool VerificarSeVeiculoVendido(int veiculoId)
        {
            return false;
        }

        public List<VeiculosModel> ObterModelosPorFabricante(int fabricanteId)
        {
            return _bancoContext.Veiculos.Where(v => v.FabricanteID == fabricanteId && !v.IsDeleted).ToList();
        }
    }
}
