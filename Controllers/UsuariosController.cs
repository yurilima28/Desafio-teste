﻿using Intelectah.Enums;
using Intelectah.Models;
using Intelectah.Repositorio;
using Intelectah.ViewModel;
using Microsoft.AspNetCore.Mvc;

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

                var viewModel = usuarios.Select(u => new UsuariosModel
                {
                    UsuarioID = u.UsuarioID,
                    NomeUsuario = u.NomeUsuario,
                    Login = u.Login,
                    Email = u.Email,
                    NivelAcesso = (PerfilEnum)u.NivelAcesso
                }).ToList();

                return View(viewModel);
            }
            catch (Exception erro)
            {
                ViewBag.ErrorMessage = "Ocorreu um erro ao buscar os usuários. Tente novamente mais tarde.";
                return View("Error");
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

                var viewModel = new UsuariosViewModel
                {
                    UsuarioId = usuario.UsuarioID,
                    NomeUsuario = usuario.NomeUsuario,
                    Email = usuario.Email,
                    Login = usuario.Login,
                    Senha = usuario.Senha
                };

                return View(viewModel);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao buscar usuário: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            try
            {
                var usuario = _usuariosRepositorio.ListarPorId(id, incluirExcluidos: false);
                if (usuario == null)
                {
                    TempData["MensagemErro"] = "Usuário não encontrado.";
                    return RedirectToAction("Index");
                }

                var viewModel = new UsuariosViewModel
                {
                    UsuarioId = usuario.UsuarioID,
                    NomeUsuario = usuario.NomeUsuario,
                    Email = usuario.Email,
                    Login = usuario.Login,
                    NivelAcesso = usuario.NivelAcesso
                };

                return View(viewModel);
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
                var usuarioModel = new UsuariosModel
                {
                    NomeUsuario = viewModel.NomeUsuario,
                    Email = viewModel.Email,
                    Login = viewModel.Login,
                    Senha = viewModel.Senha,
                    NivelAcesso = viewModel.NivelAcesso
                };

                _usuariosRepositorio.AdicionarUsuario(usuarioModel);

                TempData["MensagemSucesso"] = "Usuário criado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                if (erro.Message.Contains("email"))
                {
                    ModelState.AddModelError("Email", "Já existe um usuário com este email.");
                }
                else if (erro.Message.Contains("login"))
                {
                    ModelState.AddModelError("Login", "Já existe um usuário com este login.");
                }
                else
                {
                    ModelState.AddModelError("", "Erro ao criar usuário: " + erro.Message);
                }
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Editar(UsuariosViewModel viewModel)
        {
            try
            {
                var usuarioExistente = _usuariosRepositorio.ListarPorId(viewModel.UsuarioId);
                if (usuarioExistente == null)
                {
                    TempData["MensagemErro"] = "Usuário não encontrado.";
                    return RedirectToAction("Index");
                }

                usuarioExistente.NomeUsuario = viewModel.NomeUsuario;
                usuarioExistente.Email = viewModel.Email;
                usuarioExistente.Login = viewModel.Login;
                usuarioExistente.Senha = viewModel.Senha;
                usuarioExistente.NivelAcesso = viewModel.NivelAcesso;

                _usuariosRepositorio.AtualizarUsuario(usuarioExistente);

                TempData["MensagemSucesso"] = "Usuário atualizado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                ModelState.AddModelError("", erro.Message);
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ApagarConfirmado(int id)
        {
            try
            {
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
