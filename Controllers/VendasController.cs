using Intelectah.Models;
using Intelectah.Repositorio;
using Intelectah.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Intelectah.Controllers
{
    public class VendasController : Controller
    {
        private readonly IVendasRepositorio _vendasRepositorio;
        private readonly IClientesRepositorio _clientesRepositorio;
        private readonly IUsuariosRepositorio _usuariosRepositorio;
        private readonly IConcessionariasRepositorio _concessionariasRepositorio;
        private readonly IFabricantesRepositorio _fabricantesRepositorio;
        private readonly IVeiculosRepositorio _veiculosRepositorio;

        public VendasController(
            IVendasRepositorio vendasRepositorio,
            IClientesRepositorio clientesRepositorio,
            IUsuariosRepositorio usuariosRepositorio,
            IConcessionariasRepositorio concessionariasRepositorio,
            IFabricantesRepositorio fabricantesRepositorio,
            IVeiculosRepositorio veiculosRepositorio)
        {
            _vendasRepositorio = vendasRepositorio;
            _clientesRepositorio = clientesRepositorio;
            _usuariosRepositorio = usuariosRepositorio;
            _concessionariasRepositorio = concessionariasRepositorio;
            _fabricantesRepositorio = fabricantesRepositorio;
            _veiculosRepositorio = veiculosRepositorio;
        }

        public IActionResult Index()
        {
            var vendas = _vendasRepositorio.BuscarTodos();

            var vendasViewModel = vendas.Select(v => new VendasViewModel
            {
                VendaId = v.VendaId,
                ClienteID = v.ClienteID,
                NomeCliente = v.Cliente?.Nome, 
                DataVenda = v.DataVenda,
                ValorTotal = v.ValorTotal,
                UsuarioID = v.UsuarioID,
                NomeUsuario = v.Usuario?.NomeUsuario,
                ConcessionariaID = v.ConcessionariaID,
                NomeConcessionaria = v.Concessionaria?.Nome, 
                FabricanteID = v.FabricanteID,
                NomeFabricante = v.Fabricante?.NomeFabricante, 
                VeiculoID = v.VeiculoID,
                ModeloVeiculo = v.Veiculo?.ModeloVeiculo, 
                ProtocoloVenda = v.ProtocoloVenda,
                IsDeleted = v.IsDeleted
            }).ToList();

            return View(vendasViewModel);
        }


        public IActionResult Criar()
        {
            try
            {
                var viewModel = new VendasViewModel
                {
                    ProtocoloVenda = GerarProtocoloVenda()
                };
                PrepararDadosDropdowns(viewModel);
                return View(viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Erro ao carregar a página: {ex.Message}";
                return View("Error");
            }
        }

        public IActionResult Editar(int id)
        {
            var venda = _vendasRepositorio.ListarPorId(id);
            if (venda == null)
            {
                TempData["MensagemErro"] = "Venda não encontrada.";
                return RedirectToAction("Index");
            }

            var vendaViewModel = MapearParaViewModel(venda);
            PrepararDadosDropdowns(vendaViewModel, venda.FabricanteID);
            return View(vendaViewModel);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            try
            {
                var venda = _vendasRepositorio.ListarPorId(id);
                if (venda == null)
                {
                    TempData["MensagemErro"] = "Venda não encontrado.";
                    return RedirectToAction("Index");
                }

                var viewModel = new VendasViewModel
                {
                    VendaId = venda.VendaId,
                    ClienteID = venda.ClienteID,
                    UsuarioID = venda.UsuarioID,
                    ConcessionariaID = venda.ConcessionariaID,

                };
                return View(viewModel);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível obter os dados da venda. Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Criar(VendasViewModel vendasViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    vendasViewModel.ProtocoloVenda = GerarProtocoloVenda();
                    if (vendasViewModel.ProtocoloVenda == null)
                    {
                        throw new Exception("Não foi possível gerar o protocolo de venda.");
                    }
                    var vendaModel = MapearParaModel(vendasViewModel);
                    _vendasRepositorio.Adicionar(vendaModel);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"Erro ao salvar a venda: {ex.Message}";
                    PrepararDadosDropdowns(vendasViewModel);
                    return View(vendasViewModel);
                }
            }
            PrepararDadosDropdowns(vendasViewModel);
            return View(vendasViewModel);
        }

        [HttpPost]
        public IActionResult Editar(VendasViewModel vendasViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var vendaModel = MapearParaModel(vendasViewModel);
                    _vendasRepositorio.Atualizar(vendaModel);
                    TempData["MensagemSucesso"] = "Venda atualizada com sucesso!";
                    return RedirectToAction("Index");
                }
                catch (Exception erro)
                {
                    TempData["MensagemErro"] = "Ocorreu um erro ao atualizar a venda. Por favor, tente novamente.";
                }
            }
            PrepararDadosDropdowns(vendasViewModel);
            return View(vendasViewModel);
        }

        [HttpPost]
        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _vendasRepositorio.Apagar(id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Venda excluida com sucesso";
                }
                else
                {
                    TempData["MensagemErro"] = "Não foi possível deletar a venda.";
                }
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível deletar a venda, tente novamente. Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
        
        public JsonResult BuscarPorFabricante(int fabricanteId)
        {
            var modelos = _veiculosRepositorio.ObterModelosPorFabricante(fabricanteId);

            if (modelos != null && modelos.Any())
            {
                var listaModelos = modelos.Select(m => new
                {
                    value = m.VeiculoID, 
                    text = m.ModeloVeiculo 
                }).ToList();
                return Json(new { sucesso = true, data = listaModelos });
            }
            return Json(new { sucesso = false, mensagem = "Nenhum modelo encontrado." });
        }

        private void PrepararDadosDropdowns(VendasViewModel vendasViewModel, int? fabricanteId = null)
        {
            ViewBag.Clientes = ObterSelectList(_clientesRepositorio.BuscarTodos(), c => c.ClienteID.ToString(), c => c.Nome);
            ViewBag.Concessionarias = ObterSelectList(_concessionariasRepositorio.BuscarTodos(), co => co.ConcessionariaID.ToString(), co => co.Nome);
            ViewBag.Fabricantes = ObterSelectList(_fabricantesRepositorio.BuscarTodos(), f => f.FabricanteID.ToString(), f => f.NomeFabricante);
            ViewBag.Usuarios = ObterSelectList(_usuariosRepositorio.ObterTodosUsuarios(), u => u.UsuarioID.ToString(), u => u.NomeUsuario);
            ViewBag.Modelos = ObterSelectList(_veiculosRepositorio.BuscarTodos(), v => v.ModeloVeiculo.ToString(), v => v.ModeloVeiculo);

            if (fabricanteId.HasValue)
            {
                ViewBag.Modelos = ObterSelectList(_veiculosRepositorio.ObterModelosPorFabricante(fabricanteId.Value), v => v.VeiculoID.ToString(), v => v.ModeloVeiculo);
            }
            else
            {
                ViewBag.Modelos = new List<SelectListItem>();
            }
        }

        private IEnumerable<SelectListItem> ObterSelectList<T>(IEnumerable<T> items, Func<T, string> valueSelector, Func<T, string> textSelector)
        {
            return items.Select(item => new SelectListItem
            {
                Value = valueSelector(item),
                Text = textSelector(item)
            }).ToList();
        }

        private VendasViewModel MapearParaViewModel(VendasModel venda)
        {
            return new VendasViewModel
            {
                VendaId = venda.VendaId,
                ClienteID = venda.ClienteID,
                DataVenda = venda.DataVenda,
                ValorTotal = venda.ValorTotal,
                UsuarioID = venda.UsuarioID,
                ConcessionariaID = venda.ConcessionariaID,
                FabricanteID = venda.FabricanteID,
                VeiculoID = venda.VeiculoID,
                ProtocoloVenda = venda.ProtocoloVenda,
            };
        }

        private VendasModel MapearParaModel(VendasViewModel viewModel)
        {
            return new VendasModel
            {
                VendaId = viewModel.VendaId,
                ClienteID = viewModel.ClienteID,
                DataVenda = viewModel.DataVenda,
                ValorTotal = viewModel.ValorTotal,
                UsuarioID = viewModel.UsuarioID,
                ConcessionariaID = viewModel.ConcessionariaID,
                FabricanteID = viewModel.FabricanteID,
                VeiculoID = viewModel.VeiculoID,
                ProtocoloVenda = viewModel.ProtocoloVenda,
            };
        }

        private string GerarProtocoloVenda()
        {
            try
            {
                var protocolo = DateTime.Now.ToString("yyyyMMddHHmmss");
                return protocolo;
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = "Ocorreu um erro ao gerar o protocolo de venda. Por favor, tente novamente.";
                return null;
            }
        }

    }
}
