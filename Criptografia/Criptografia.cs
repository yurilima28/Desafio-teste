using System.Security.Cryptography;
using System.Text;

namespace Desafio_teste.Criptografia
{
    public static class Criptografia
    {
        public static string GerarHas(this string valor)
        {
            var hash = SHA1.Create();
            var enconding = new ASCIIEncoding();
            var array = enconding.GetBytes(valor);

            array = hash.ComputeHash(array);

            var strHexa = new StringBuilder();

            foreach (var item in array)
            {
                strHexa.Append(item.ToString("x2"));
            }
            return strHexa.ToString();
        }
    }
}
