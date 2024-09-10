using Intelectah.Models;
using System.Collections.Generic;

namespace Intelectah.Repositorio
{
    public interface IUsuariosRepositorio
    {
        UsuariosModel BuscarPorLogin(string login);
        UsuariosModel ListarPorId(int id, bool incluirExcluidos = false);
        List<UsuariosModel> ObterTodosUsuarios(bool incluirExcluidos = false);
        UsuariosModel AdicionarUsuario(UsuariosModel usuario);
        UsuariosModel AtualizarUsuario(UsuariosModel usuario);
        bool ApagarUsuario(int usuarioId);
        bool RestaurarUsuario(int usuarioId);
        UsuariosModel ObterUsuarioPorId(int usuarioId);
        bool UsuarioExiste(string nomeUsuario);
        bool VerificarNomeUsuarioUnico(string nomeUsuario, int? usuarioId = null);
        bool VerificarEmailOuLoginExistente(string email, string login);
    }
}
