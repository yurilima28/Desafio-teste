using Intelectah.Enums;

namespace Intelectah.ViewModel
{
    public class UsuariosViewModel
    {
        public int UsuarioId { get; set; }

        public string Login { get; set; }

        public bool SenhaValida(string senha)
        {
            return Senha == senha;
        }
        public string NomeUsuario { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public PerfilEnum NivelAcesso { get; set; }
        public bool IsDeleted { get; set; }

        public UsuariosViewModel() { }
        public UsuariosViewModel(string nomeUsuario, string senha, string email, PerfilEnum nivelAcesso, string login, bool isDeleted = false)
        {
            NomeUsuario = nomeUsuario;
            Senha = senha;
            Email = email;
            NivelAcesso = nivelAcesso;
            Login = login;
            IsDeleted = isDeleted;
        }
    }
}
