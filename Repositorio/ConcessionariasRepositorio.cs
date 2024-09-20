using Intelectah.Dapper;
using Intelectah.Models;

namespace Intelectah.Repositorio
{
    public class ConcessionariasRepositorio : IConcessionariasRepositorio
    {
        private readonly BancoContext _bancoContext;

        public ConcessionariasRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public ConcessionariasModel ListarPorId(int id, bool incluirExcluidos = false)
        {
            IQueryable<ConcessionariasModel> query = _bancoContext.Concessionarias;

            if (!incluirExcluidos)
            {
                query = query.Where(c => !c.IsDeleted);
            }

            return query.FirstOrDefault(c => c.ConcessionariaID == id);
        }

        public List<ConcessionariasModel> BuscarTodos(bool incluirExcluidos = false)
        {
            IQueryable<ConcessionariasModel> query = _bancoContext.Concessionarias;

            if (!incluirExcluidos)
            {
                query = query.Where(c => !c.IsDeleted);
            }

            return query.ToList();
        }

        public ConcessionariasModel Adicionar(ConcessionariasModel concessionaria)
        {
            if (concessionaria.Nome.Length > 100)
            {
                throw new ArgumentException("O nome da concessionária não pode exceder 100 caracteres.");
            }

            if (_bancoContext.Concessionarias.Any(c => c.Nome == concessionaria.Nome))
            {
                throw new ArgumentException("O nome da concessionária já existe.");
            }

            _bancoContext.Concessionarias.Add(concessionaria);
            _bancoContext.SaveChanges();
            return concessionaria;
        }

        public ConcessionariasModel Atualizar(ConcessionariasModel concessionaria)
        {
            if (concessionaria.Nome.Length > 100)
            {
                throw new ArgumentException("O nome da concessionária não pode exceder 100 caracteres.");
            }

            if (_bancoContext.Concessionarias.Any(c => c.Nome == concessionaria.Nome && c.ConcessionariaID != concessionaria.ConcessionariaID))
            {
                throw new ArgumentException("O nome da concessionária já existe.");
            }

            _bancoContext.Concessionarias.Update(concessionaria);
            _bancoContext.SaveChanges();
            return concessionaria;
        }

        public bool Apagar(int id)
        {
            var concessionaria = ListarPorId(id);
            if (concessionaria != null)
            {
                concessionaria.IsDeleted = true;
                _bancoContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Restaurar(int id)
        {
            var concessionaria = ListarPorId(id, incluirExcluidos: true);
            if (concessionaria != null && concessionaria.IsDeleted)
            {
                concessionaria.IsDeleted = false;
                _bancoContext.SaveChanges();
                return true;
            }
            return false;
        }

        public ConcessionariasModel ObterPorNome(string nomeConcessionaria)
        {
            return _bancoContext.Concessionarias
                .FirstOrDefault(c => c.Nome == nomeConcessionaria && !c.IsDeleted);
        }

        public bool VerificarNomeConcessionariaUnico(string nomeConcessionaria, int? concessionariaID = null)
        {
            return !_bancoContext.Concessionarias
                .Any(c => c.Nome == nomeConcessionaria && c.ConcessionariaID != concessionariaID && !c.IsDeleted);
        }
    }
}
