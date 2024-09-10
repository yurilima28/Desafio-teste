using Intelectah.Repositorio;
using System.ComponentModel.DataAnnotations;

public class ValidationModel
{
    public class UniqueNomeFabricanteAttribute : ValidationAttribute
    {
        public UniqueNomeFabricanteAttribute()
        {
            ErrorMessage = "O nome do fabricante já está em uso.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var fabricantesRepositorio = (IFabricantesRepositorio)validationContext.GetService(typeof(IFabricantesRepositorio));
                string nomeFabricante = value.ToString();

                var existingFabricante = fabricantesRepositorio.VerificarNomeFabricanteUnico(nomeFabricante);
                if (!existingFabricante)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }

    public class AnoFundacaoAttribute : ValidationAttribute
    {
        private const int AnoMinimo = 1886;

        public AnoFundacaoAttribute()
        {
            ErrorMessage = $"O ano de fundação deve ser um valor entre {AnoMinimo} e o ano atual.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var anoFundacao = (int?)value;
            var anoAtual = DateTime.Now.Year;

            if (anoFundacao == null || anoFundacao < AnoMinimo || anoFundacao > anoAtual)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
    public class AnoFabricacaoAttribute : ValidationAttribute
    {

        public AnoFabricacaoAttribute()
        {
            ErrorMessage = "O ano de fabricação deve ser um valor entre o ano atual e o próximo ano.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is int anoFabricacao)
            {
                var anoAtual = DateTime.Now.Year;
                if (anoFabricacao > anoAtual)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
  
    public class CPFValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string cpf = value as string;

            cpf = cpf.Replace(".", "").Replace("-", "");

            if (!IsValidCPF(cpf))
            {
                return new ValidationResult("CPF inválido.");
            }

            return ValidationResult.Success;
        }

        private bool IsValidCPF(string cpf)
        {
            if (cpf.Length != 11)
                return false;

            if (cpf == new string('0', 11) || cpf == new string('1', 11) ||
                cpf == new string('2', 11) || cpf == new string('3', 11) ||
                cpf == new string('4', 11) || cpf == new string('5', 11) ||
                cpf == new string('6', 11) || cpf == new string('7', 11) ||
                cpf == new string('8', 11) || cpf == new string('9', 11))
                return false;

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(cpf[i].ToString()) * (10 - i);

            int resto = soma % 11;
            int digito1 = (resto < 2) ? 0 : 11 - resto;

            if (digito1 != int.Parse(cpf[9].ToString()))
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(cpf[i].ToString()) * (11 - i);

            resto = soma % 11;
            int digito2 = (resto < 2) ? 0 : 11 - resto;

            return digito2 == int.Parse(cpf[10].ToString());
        }
    }
}
