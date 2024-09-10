using Intelectah.Models;
using Intelectah.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace Intelectah.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuariosRepositorio _usuariosRepositorio;

        public LoginController(IUsuariosRepositorio usuariosRepositorio)
        {
            _usuariosRepositorio = usuariosRepositorio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Index");
                }

                var usuario = _usuariosRepositorio.BuscarPorLogin(loginModel.Login);
                if (usuario == null || !usuario.SenhaValida(loginModel.Senha))
                {
                    TempData["MensagemErro"] = "Usuário e/ou senha inválidos, tente novamente.";
                    return View("Index");
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não conseguimos realizar seu login: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
