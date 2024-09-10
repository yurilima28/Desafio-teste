using System.ComponentModel.DataAnnotations;

namespace Intelectah.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Digite o login")]
        public string Login {  get; set; }

        [Required(ErrorMessage = "Digite a senha")]
        public string Senha { get; set; }
        public bool IsDeleted { get; set; }

        public LoginModel() { }

        public LoginModel(string login, string senha, bool isDeleted = false)
        {
            Login = login;
            Senha = senha;
            IsDeleted = isDeleted;
        }
    }
}
