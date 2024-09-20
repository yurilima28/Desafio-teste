using Intelectah.Models;
using Intelectah.Repositorio;
using Intelectah.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Intelectah.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IClientesRepositorio _clientesRepositorio;
        public ClientesController(IClientesRepositorio clientesRepositorio)
        {
            _clientesRepositorio = clientesRepositorio;
        }

        public IActionResult Index()
        {
            var clientes = _clientesRepositorio.BuscarTodos();

            var viewModel = clientes.Select(c => new ClientesViewModel
            {
                ClienteID = c.ClienteID,
                Nome = c.Nome,
                CPF = c.CPF,
                Telefone = c.Telefone,
                Email = c.Email
            }).ToList();

            return View(viewModel);
        }

        public IActionResult Criar()
        {
            var viewModel = new ClientesViewModel();

            ViewData["FormAction"] = "Criar";
            return View(viewModel);
        }

        public IActionResult Editar(int id)
        {
            var cliente = _clientesRepositorio.ListarPorId(id);

            if (cliente == null)
            {
                TempData["MensagemErro"] = "Cliente não encontrado.";
                return RedirectToAction("Index");
            }

            var viewModel = new ClientesViewModel
            {
                ClienteID = cliente.ClienteID,
                Nome = cliente.Nome,
                CPF = cliente.CPF,
                Telefone = cliente.Telefone,
                Email = cliente.Email
            };

            ViewData["FormAction"] = "Editar";
            return View(viewModel);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            var cliente = _clientesRepositorio.ListarPorId(id);

            if (cliente == null)
            {
                TempData["MensagemErro"] = "Cliente não encontrado.";
                return RedirectToAction("Index");
            }

            var viewModel = new ClientesViewModel
            {
                ClienteID = cliente.ClienteID,
                Nome = cliente.Nome,
                CPF = cliente.CPF,
                Telefone = cliente.Telefone,
                Email = cliente.Email
            };

            return View(cliente);
        }

        [HttpPost]
        public IActionResult Criar(ClientesViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var clienteModel = new ClientesModel
                    {
                        Nome = viewModel.Nome,
                        CPF = viewModel.CPF,
                        Telefone = viewModel.Telefone,
                        Email = viewModel.Email
                    };

                    _clientesRepositorio.Adicionar(clienteModel);
                    TempData["MensagemSucesso"] = "Cliente criado com sucesso.";
                    return RedirectToAction("Index");
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(viewModel);
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = $"Erro ao criar o cliente: {ex.Message}";
                    return View(viewModel);
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Editar(ClientesViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var clienteModel = new ClientesModel
                    {
                        ClienteID = viewModel.ClienteID,
                        Nome = viewModel.Nome,
                        CPF = viewModel.CPF,
                        Telefone = viewModel.Telefone,
                        Email = viewModel.Email
                    };

                    _clientesRepositorio.Atualizar(clienteModel);
                    TempData["MensagemSucesso"] = "Cliente atualizado com sucesso.";
                    return RedirectToAction("Index");
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(viewModel);
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = $"Erro ao atualizar o cliente: {ex.Message}";
                    return View(viewModel);
                }
            }

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult Apagar(int id)
        {
            if (id <= 0)
            {
                TempData["MensagemErro"] = "ID inválido.";
                return RedirectToAction("Index");
            }

            try
            {
                bool apagado = _clientesRepositorio.Apagar(id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Cliente deletado com sucesso.";
                }
                else
                {
                    TempData["MensagemErro"] = "Não foi possível deletar o cliente. O cliente pode não existir ou pode haver dependências.";
                }
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível deletar o cliente. Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }


    }
}

