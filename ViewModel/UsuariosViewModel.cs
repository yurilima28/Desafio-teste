using Intelectah.Enums;
using System.ComponentModel.DataAnnotations;

namespace Intelectah.ViewModel
{
    public class UsuariosViewModel
    {
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "O login é obrigatório.")]
        public string Login { get; set; }

        public bool SenhaValida(string senha)
        {
            return Senha == senha;
        }

        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        public string NomeUsuario { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O nível de acesso é obrigatório.")]
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
