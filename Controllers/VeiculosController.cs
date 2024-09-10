using Intelectah.Enums;
using Intelectah.Models;
using Intelectah.Repositorio;
using Intelectah.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Intelectah.Controllers
{
    public class VeiculosController : Controller
    {
        private readonly IVeiculosRepositorio _veiculosRepositorio;
        private readonly IFabricantesRepositorio _fabricantesRepositorio;

        public VeiculosController(IVeiculosRepositorio veiculosRepositorio, IFabricantesRepositorio fabricantesRepositorio)
        {
            _veiculosRepositorio = veiculosRepositorio;
            _fabricantesRepositorio = fabricantesRepositorio;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var veiculos = _veiculosRepositorio.BuscarTodos();
                var fabricantes = _fabricantesRepositorio.BuscarTodos();

                var viewModel = veiculos.Select(v => new VeiculosViewModel
                {
                    VeiculoID = v.VeiculoID,
                    ModeloVeiculo = v.ModeloVeiculo,
                    FabricanteID = v.FabricanteID,
                    AnoFabricacao = v.AnoFabricacao,
                    ValorVeiculo = v.ValorVeiculo,
                    Tipo = v.Tipo,
                    Descricao = v.Descricao,
                    Fabricantes = fabricantes.Select(f => new SelectListItem
                    {
                        Value = f.FabricanteID.ToString(),
                        Text = f.NomeFabricante
                    }).ToList(),
                    TiposVeiculos = Enum.GetValues(typeof(TipoVeiculo)).Cast<TipoVeiculo>()
                 .Select(t => new SelectListItem
                 {
                     Value = t.ToString(),
                     Text = t.ToString()
                 }).ToList()
                }).ToList();

                return View(viewModel);
            }
            catch (Exception erro)
            {
                ViewBag.ErrorMessage = "Ocorreu um erro ao buscar os veículos. Tente novamente mais tarde.";
                return View("Error");
            }
        }

        public IActionResult Criar()
        {
            var fabricantes = _fabricantesRepositorio.BuscarTodos();

            var viewModel = new VeiculosViewModel
            {
                Fabricantes = fabricantes.Select(f => new SelectListItem
                {
                    Value = f.FabricanteID.ToString(),
                    Text = f.NomeFabricante
                }).ToList(),

                TiposVeiculos = Enum.GetValues(typeof(TipoVeiculo))
                .Cast<TipoVeiculo>()
                .Select(t => new SelectListItem
                {
                    Value = ((int)t).ToString(),
                    Text = t.ToString()
                }).ToList()
            };
            return View(viewModel);
        }

        public IActionResult Editar(int id)
        {
            try
            {
                var veiculo = _veiculosRepositorio.ListarPorId(id);
                if (veiculo == null)
                {
                    return NotFound();
                }

                var fabricantes = _fabricantesRepositorio.BuscarTodos();

                var viewModel = new VeiculosViewModel
                {
                    VeiculoID = veiculo.VeiculoID,
                    ModeloVeiculo = veiculo.ModeloVeiculo,
                    FabricanteID = veiculo.FabricanteID,
                    AnoFabricacao = veiculo.AnoFabricacao,
                    Tipo = veiculo.Tipo,

                    Fabricantes = fabricantes.Select(f => new SelectListItem
                    {
                        Value = f.FabricanteID.ToString(),
                        Text = f.NomeFabricante
                    }).ToList(),

                    TiposVeiculos = Enum.GetValues(typeof(TipoVeiculo))
                        .Cast<TipoVeiculo>()
                        .Select(t => new SelectListItem
                        {
                            Value = ((int)t).ToString(),
                            Text = t.ToString()
                        }).ToList()
                };

                return View(viewModel);
            }
            catch (Exception erro)
            {
                ViewBag.ErrorMessage = "Ocorreu um erro ao buscar o veículo para edição. Tente novamente mais tarde.";
                return View("Error");
            }
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _veiculosRepositorio.Apagar(id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Veículo deletado com sucesso";
                }
                else
                {
                    TempData["MensagemErro"] = "Não foi possível deletar o veículo.";
                }
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível deletar o veículo, tente novamente. Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            try
            {
                var veiculo = _veiculosRepositorio.ListarPorId(id);
                if (veiculo == null)
                {
                    TempData["MensagemErro"] = "Veículo não encontrado.";
                    return RedirectToAction("Index");
                }

                var viewModel = new VeiculosViewModel
                {
                    VeiculoID = veiculo.VeiculoID,
                    ModeloVeiculo = veiculo.ModeloVeiculo,
                    FabricanteID = veiculo.FabricanteID,
                    AnoFabricacao = veiculo.AnoFabricacao,

                };
                return View(viewModel);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível obter os dados do veículo. Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Criar(VeiculosViewModel viewModel)
        {
            try
            {
                if (viewModel.ModeloVeiculo.Length > 100)
                {
                    ModelState.AddModelError(nameof(viewModel.ModeloVeiculo), "O modelo do veículo não pode exceder 100 caracteres.");
                }

                if (ModelState.IsValid)
                {
                    var veiculosModel = new VeiculosModel
                    {
                        VeiculoID = viewModel.VeiculoID,
                        ModeloVeiculo = viewModel.ModeloVeiculo,
                        FabricanteID = viewModel.FabricanteID,
                        AnoFabricacao = viewModel.AnoFabricacao,
                        ValorVeiculo = viewModel.ValorVeiculo,
                        Tipo = viewModel.Tipo,
                        Descricao = viewModel.Descricao,

                    };

                    _veiculosRepositorio.Adicionar(veiculosModel);
                    TempData["MensagemSucesso"] = "Veículo adicionado com sucesso.";
                    return RedirectToAction("Index");
                }

                return View(viewModel);
            }
            catch (Exception erro)
            {
                ModelState.AddModelError(string.Empty, $"Ocorreu um erro ao salvar o veículo: {erro.Message}");
                return View(viewModel);
            }
        }

        [HttpPost]
        public IActionResult Editar(VeiculosViewModel viewModel)
        {
            try
            {
                if (viewModel.ModeloVeiculo.Length > 100)
                {
                    ModelState.AddModelError(nameof(viewModel.ModeloVeiculo), "O modelo do veículo não pode exceder 100 caracteres.");
                }

                if (ModelState.IsValid)
                {
                    var veiculo = new VeiculosModel
                    {
                        VeiculoID = viewModel.VeiculoID,
                        ModeloVeiculo = viewModel.ModeloVeiculo,
                        FabricanteID = viewModel.FabricanteID,
                        AnoFabricacao = viewModel.AnoFabricacao,
                        Descricao = viewModel.Descricao,
                    };

                    _veiculosRepositorio.Atualizar(veiculo);
                    TempData["MensagemSucesso"] = "Veículo alterado com sucesso.";
                    return RedirectToAction("Index");
                }

                return View(viewModel);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao atualizar o veículo: {erro.Message}";
                return View(viewModel);
            }
        }
    }
}
