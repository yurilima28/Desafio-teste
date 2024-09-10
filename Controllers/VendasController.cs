using Intelectah.Models;
using Intelectah.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace Intelectah.Controllers
{
    public class VendasController : Controller
    {
        private readonly IVendasRepositorio _vendasRepositorio;

        public VendasController(IVendasRepositorio vendasRepositorio)
        {
            _vendasRepositorio = vendasRepositorio;
        }

        public IActionResult Index()
        {
            try
            {
                var vendas = _vendasRepositorio.BuscarTodos();
                return View(vendas);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ocorreu um erro ao carregar a lista de vendas: {erro.Message}";
                return View("Error");
            }
        }

        public IActionResult Detalhes(int id)
        {
            try
            {
                var venda = _vendasRepositorio.ListarPorId(id);
                if (venda == null)
                {
                    TempData["MensagemErro"] = "Venda não encontrada.";
                    return RedirectToAction("Index");
                }

                return View(venda);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao buscar venda: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(VendasModel vendaModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(vendaModel);
                }

                _vendasRepositorio.Adicionar(vendaModel);
                TempData["MensagemSucesso"] = "Venda criada com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao criar venda: {erro.Message}";
                return View(vendaModel);
            }
        }

        public IActionResult Editar(int id)
        {
            try
            {
                var venda = _vendasRepositorio.ListarPorId(id);
                if (venda == null)
                {
                    TempData["MensagemErro"] = "Venda não encontrada.";
                    return RedirectToAction("Index");
                }

                return View(venda);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao buscar venda: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Editar(VendasModel vendaModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(vendaModel);
                }

                _vendasRepositorio.Atualizar(vendaModel);
                TempData["MensagemSucesso"] = "Venda atualizada com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao atualizar venda: {erro.Message}";
                return View(vendaModel);
            }
        }

        public IActionResult Deletar(int id)
        {
            try
            {
                var venda = _vendasRepositorio.ListarPorId(id);
                if (venda == null)
                {
                    TempData["MensagemErro"] = "Venda não encontrada.";
                    return RedirectToAction("Index");
                }

                return View(venda);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao buscar venda: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult DeletarConfirmado(int id)
        {
            try
            {
                var venda = _vendasRepositorio.ListarPorId(id);
                if (venda == null)
                {
                    TempData["MensagemErro"] = "Venda não encontrada.";
                    return RedirectToAction("Index");
                }

                if (_vendasRepositorio.Apagar(id))
                {
                    TempData["MensagemSucesso"] = "Venda deletada com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Erro ao deletar venda.";
                }

                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao deletar venda: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Restaurar(int id)
        {
            try
            {
                var venda = _vendasRepositorio.ListarPorId(id, incluirExcluidos: true);
                if (venda == null || !venda.IsDeleted)
                {
                    TempData["MensagemErro"] = "Venda não encontrada ou não está excluída.";
                    return RedirectToAction("Index");
                }

                if (_vendasRepositorio.Restaurar(id))
                {
                    TempData["MensagemSucesso"] = "Venda restaurada com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Erro ao restaurar venda.";
                }

                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao restaurar venda: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
