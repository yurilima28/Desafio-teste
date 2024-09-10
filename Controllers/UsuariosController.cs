using Intelectah.Models;
using Intelectah.Repositorio;
using Intelectah.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;

namespace Intelectah.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuariosRepositorio _usuariosRepositorio;

        public UsuariosController(IUsuariosRepositorio usuariosRepositorio)
        {
            _usuariosRepositorio = usuariosRepositorio;
        }

        public IActionResult Index()
        {
            try
            {
                var usuarios = _usuariosRepositorio.ObterTodosUsuarios();
                return View(usuarios);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ocorreu um erro ao carregar a lista de usuários: {erro.Message}";
                return View("Index");
            }
        }

        public IActionResult Criar()
        {
            UsuariosViewModel viewModel = new UsuariosViewModel();
            return View(viewModel);
        }

        public IActionResult Editar(int id)
        {
            try
            {
                var usuario = _usuariosRepositorio.ListarPorId(id);
                if (usuario == null)
                {
                    TempData["MensagemErro"] = "Usuário não encontrado.";
                    return RedirectToAction("Index");
                }

                return View(usuario);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao buscar usuário: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
        public IActionResult Deletar(int id)
        {
            try
            {
                var usuario = _usuariosRepositorio.ListarPorId(id);
                if (usuario == null)
                {
                    TempData["MensagemErro"] = "Usuário não encontrado.";
                    return RedirectToAction("Index");
                }

                return View(usuario);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao buscar usuário: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Criar(UsuariosViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                if (_usuariosRepositorio.VerificarEmailOuLoginExistente(viewModel.Email, viewModel.Login))
                {
                    ModelState.AddModelError(string.Empty, "Já existe um usuário com este email ou login.");
                    return View(viewModel);
                }

                UsuariosModel usuarioModel = new UsuariosModel
                {
                    NomeUsuario = viewModel.NomeUsuario,
                    Email = viewModel.Email,
                    Login = viewModel.Login,
                    Senha = viewModel.Senha
                };

                _usuariosRepositorio.AdicionarUsuario(usuarioModel);

                TempData["MensagemSucesso"] = "Usuário criado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException is SqlException sqlEx)
                {
                    if (sqlEx.Message.Contains("Cannot insert the value NULL into column 'Senha'"))
                    {
                        ModelState.AddModelError("", "O campo Senha é obrigatório.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Ocorreu um erro ao salvar o usuário. Por favor, tente novamente.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao salvar o usuário. Por favor, tente novamente.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro inesperado: {ex.Message}");
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Editar(UsuariosModel usuarioModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(usuarioModel);
                }

                if (_usuariosRepositorio.VerificarNomeUsuarioUnico(usuarioModel.NomeUsuario, usuarioModel.UsuarioID))
                {
                    ModelState.AddModelError(string.Empty, "O nome de usuário já está em uso.");
                    return View(usuarioModel);
                }

                _usuariosRepositorio.AtualizarUsuario(usuarioModel);
                TempData["MensagemSucesso"] = "Usuário atualizado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao atualizar usuário: {erro.Message}";
                return View(usuarioModel);
            }
        }

        [HttpPost]
        public IActionResult ApagarConfirmacao(int id)
        {
            try
            {
                var usuario = _usuariosRepositorio.ListarPorId(id);
                if (usuario == null)
                {
                    TempData["MensagemErro"] = "Usuário não encontrado.";
                    return RedirectToAction("Index");
                }

                if (_usuariosRepositorio.ApagarUsuario(id))
                {
                    TempData["MensagemSucesso"] = "Usuário deletado com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Erro ao deletar usuário.";
                }
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao deletar usuário: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
