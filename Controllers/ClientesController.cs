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
            if (viewModel.Nome.Length > 100)
            {
                ModelState.AddModelError(nameof(viewModel.Nome), "O nome do cliente não pode exceder 100 caracteres.");
            }

            if (!_clientesRepositorio.VerificarNomeClienteUnico(viewModel.Nome))
            {
                ModelState.AddModelError(nameof(viewModel.Nome), "O nome do cliente já existe.");
            }
            if (_clientesRepositorio.VerificarCpfUnico(viewModel.CPF))
            {
                ModelState.AddModelError(nameof(viewModel.CPF), "O CPF já está cadastrado.");
            }

            if (!ValidarCPF(viewModel.CPF))
            {
                ModelState.AddModelError(nameof(viewModel.CPF), "O CPF informado é inválido.");
            }

            if (ModelState.IsValid)
            {
                var cliente = new ClientesModel
                {
                    Nome = viewModel.Nome,
                    CPF = viewModel.CPF,
                    Telefone = viewModel.Telefone,
                    Email = viewModel.Email
                };

                _clientesRepositorio.Adicionar(cliente);
                TempData["MensagemSucesso"] = "Cliente cadastrado com sucesso.";
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Editar(ClientesViewModel viewModel)
        {
            if (viewModel.Nome.Length > 100)
            {
                ModelState.AddModelError(nameof(viewModel.Nome), "O nome do cliente não pode exceder 100 caracteres.");
            }
          
            if (!ValidarCPF(viewModel.CPF))
            {
                ModelState.AddModelError(nameof(viewModel.CPF), "O CPF informado é inválido.");
            }
            if (_clientesRepositorio.VerificarCpfUnico(viewModel.CPF, viewModel.ClienteID))
            {
                ModelState.AddModelError(nameof(viewModel.CPF), "O CPF já está cadastrado.");
                return View(viewModel);
            }
           
            if (ModelState.IsValid)
            {
                var cliente = new ClientesModel
                {
                    ClienteID = viewModel.ClienteID,
                    Nome = viewModel.Nome,
                    CPF = viewModel.CPF,
                    Telefone = viewModel.Telefone,
                    Email = viewModel.Email
                };

                _clientesRepositorio.Atualizar(cliente);
                TempData["MensagemSucesso"] = "Cliente atualizado com sucesso.";
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _clientesRepositorio.Apagar(id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Cliente deletado com sucesso.";
                }
                else
                {
                    TempData["MensagemErro"] = "Não foi possível deletar o cliente.";
                }
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível deletar o cliente, tente novamente. Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        private bool ValidarCPF(string cpf)
        {
            return cpf.Length == 11 && cpf.All(char.IsDigit);
        }
    }
}

