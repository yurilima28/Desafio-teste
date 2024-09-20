using Intelectah.Models;
using Intelectah.Repositorio;
using Intelectah.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Intelectah.Controllers
{
    public class FabricantesController : Controller
    {
        private readonly IFabricantesRepositorio _fabricantesRepositorio;

        public FabricantesController(IFabricantesRepositorio fabricantesRepositorio)
        {
            _fabricantesRepositorio = fabricantesRepositorio;
        }

        public IActionResult Index()
        {
            try
            {
                var fabricantes = _fabricantesRepositorio.BuscarTodos();
                var viewModel = fabricantes.Select(f => new FabricantesViewModel
                {
                    FabricanteID = f.FabricanteID,
                    NomeFabricante = f.NomeFabricante,
                    PaisOrigem = f.PaisOrigem,
                    AnoFundacao = f.AnoFundacao,
                    URL = f.URL,
                }).ToList();
                return View(viewModel);
            }
            catch (Exception erro)
            {
                ViewBag.ErrorMessage = "Ocorreu um erro ao buscar os fabricantes. Tente novamente mais tarde.";
                return View("Error");
            }
        }

        public IActionResult Criar()
        {
            var viewModel = new FabricantesViewModel
            {
            };
            return View(viewModel);
        }

        public IActionResult Editar(int id)
        {
            try
            {
                var fabricante = _fabricantesRepositorio.ListarPorId(id);
                if (fabricante == null)
                {
                    return NotFound();
                }

                var viewModel = new FabricantesViewModel
                {
                    FabricanteID = fabricante.FabricanteID,
                    NomeFabricante = fabricante.NomeFabricante,
                    PaisOrigem = fabricante.PaisOrigem,
                    AnoFundacao = fabricante.AnoFundacao,
                    URL = fabricante.URL,
                };
                return View(viewModel);
            }
            catch (Exception erro)
            {
                ViewBag.ErrorMessage = "Ocorreu um erro ao buscar o fabricante para edição. Tente novamente mais tarde.";
                return View("Error");
            }
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            try
            {
                var fabricante = _fabricantesRepositorio.ListarPorId(id);
                if (fabricante == null)
                {
                    TempData["MensagemErro"] = "Fabricante não encontrado.";
                    return RedirectToAction("Index");
                }

                var viewModel = new FabricantesViewModel
                {
                    FabricanteID = fabricante.FabricanteID,
                    NomeFabricante = fabricante.NomeFabricante,
                    PaisOrigem = fabricante.PaisOrigem,
                    AnoFundacao = fabricante.AnoFundacao,
                    URL = fabricante.URL,
                };
                return View(viewModel);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível obter os dados do fabricante. Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Criar(FabricantesViewModel viewModel)
        {
            try
            {
                if (viewModel.NomeFabricante.Length > 100)
                {
                    ModelState.AddModelError(nameof(viewModel.NomeFabricante), "O nome do fabricante não pode exceder 100 caracteres.");
                }
                if (!_fabricantesRepositorio.VerificarNomeFabricanteUnico(viewModel.NomeFabricante))
                {
                    ModelState.AddModelError(nameof(viewModel.NomeFabricante), "O nome do fabricante já existe.");
                }

                if (ModelState.IsValid)
                {
                    var fabricantesModel = new FabricantesModel
                    {
                        FabricanteID = viewModel.FabricanteID,
                        NomeFabricante = viewModel.NomeFabricante,
                        PaisOrigem = viewModel.PaisOrigem,
                        AnoFundacao = viewModel.AnoFundacao,
                        URL = viewModel.URL
                    };

                    _fabricantesRepositorio.Adicionar(fabricantesModel);
                    TempData["MensagemSucesso"] = "Fabricante adicionado com sucesso.";
                    return RedirectToAction("Index");
                }

                return View(viewModel);
            }
            catch (Exception erro)
            {
                ModelState.AddModelError(string.Empty, $"Ocorreu um erro ao salvar o fabricante: {erro.Message}");
                return View(viewModel);
            }
        }

        [HttpPost]
        public IActionResult Editar(FabricantesViewModel viewModel)
        {
            try
            {
                if (viewModel.NomeFabricante.Length > 100)
                {
                    ModelState.AddModelError(nameof(viewModel.NomeFabricante), "O nome do fabricante não pode exceder 100 caracteres.");
                }
                if (!_fabricantesRepositorio.VerificarNomeFabricanteUnico(viewModel.NomeFabricante))
                {
                    ModelState.AddModelError(nameof(viewModel.NomeFabricante), "O nome do fabricante já existe.");
                }

                if (ModelState.IsValid)
                {
                    var fabricante = new FabricantesModel
                    {
                        FabricanteID = viewModel.FabricanteID,
                        NomeFabricante = viewModel.NomeFabricante,
                        PaisOrigem = viewModel.PaisOrigem,
                        AnoFundacao = viewModel.AnoFundacao,
                        URL = viewModel.URL
                    };

                    _fabricantesRepositorio.Atualizar(fabricante);
                    TempData["MensagemSucesso"] = "Fabricante alterado com sucesso.";
                    return RedirectToAction("Index");
                }

                return View(viewModel);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao atualizar o fabricante: {erro.Message}";
                return View(viewModel);
            }
        }

        [HttpPost]
        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _fabricantesRepositorio.Apagar(id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Fabricante deletado com sucesso";
                }
                else
                {
                    TempData["MensagemErro"] = "Não foi possível deletar o fabricante.";
                }
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível deletar o fabricante, tente novamente. Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}

