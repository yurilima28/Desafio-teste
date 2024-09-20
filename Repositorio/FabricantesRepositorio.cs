using Intelectah.Dapper;
using Intelectah.Models;

namespace Intelectah.Repositorio
{
    public class FabricantesRepositorio : IFabricantesRepositorio
    {
        private readonly BancoContext _bancoContext;

        public FabricantesRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public FabricantesModel ListarPorId(int id, bool incluirExcluidos = false)
        {
            IQueryable<FabricantesModel> query = _bancoContext.Fabricantes;

            if (!incluirExcluidos)
            {
                query = query.Where(f => !f.IsDeleted);
            }

            return query.FirstOrDefault(f => f.FabricanteID == id);
        }

        public List<FabricantesModel> BuscarTodos(bool incluirExcluidos = false)
        {
            IQueryable<FabricantesModel> query = _bancoContext.Fabricantes;

            if (!incluirExcluidos)
            {
                query = query.Where(f => !f.IsDeleted);
            }

            return query.ToList();
        }

        public FabricantesModel Adicionar(FabricantesModel fabricante)
        {
            if (_bancoContext.Fabricantes.Any(f => f.NomeFabricante == fabricante.NomeFabricante && f.FabricanteID != fabricante.FabricanteID))
            {
                throw new ArgumentException("O nome do fabricante já existe.");
            }

            if (fabricante == null)
            {
                throw new ArgumentNullException(nameof(fabricante), "O fabricante não pode ser nulo.");
            }

            if (fabricante.NomeFabricante.Length > 100)
            {
                throw new ArgumentException("O nome do fabricante não pode exceder 100 caracteres.");
            }
         
            const int AnoMinimo = 1886; 
            int anoAtual = DateTime.Now.Year;

            if (fabricante.AnoFundacao < AnoMinimo || fabricante.AnoFundacao > anoAtual)
            {
                throw new ArgumentException($"O ano de fundação deve ser um valor entre {AnoMinimo} e {anoAtual}.");
            }

            _bancoContext.Fabricantes.Add(fabricante);
            _bancoContext.SaveChanges();
            return fabricante;
        }

        public FabricantesModel Atualizar(FabricantesModel fabricante)
        {
            if (fabricante.NomeFabricante.Length > 100)
            {
                throw new ArgumentException("O nome do fabricante não pode exceder 100 caracteres.");
            }

            if (_bancoContext.Fabricantes.Any(f => f.NomeFabricante == fabricante.NomeFabricante && f.FabricanteID != fabricante.FabricanteID))
            {
                throw new ArgumentException("O nome do fabricante já existe.");
            }

            _bancoContext.Fabricantes.Update(fabricante);
            _bancoContext.SaveChanges();
            return fabricante;
        }

        public bool Apagar(int id)
        {
            var fabricante = ListarPorId(id);
            if (fabricante != null)
            {
                fabricante.IsDeleted = true; 
                _bancoContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Restaurar(int id)
        {
            var fabricante = ListarPorId(id, incluirExcluidos: true);
            if (fabricante != null && fabricante.IsDeleted)
            {
                fabricante.IsDeleted = false; 
                _bancoContext.SaveChanges();
                return true;
            }
            return false;
        }

        public FabricantesModel ObterPorNome(string nomeFabricante)
        {
            return _bancoContext.Fabricantes.FirstOrDefault(f => f.NomeFabricante == nomeFabricante && !f.IsDeleted);
        }

        public bool VerificarNomeFabricanteUnico(string nomeFabricante, int? fabricanteID = null)
        {
            return !_bancoContext.Fabricantes.Any(f => f.NomeFabricante == nomeFabricante && f.FabricanteID != fabricanteID && !f.IsDeleted);
        }
    }
}
