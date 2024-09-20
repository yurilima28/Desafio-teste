using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Intelectah.ViewModel
{
    public class FabricantesViewModel
    {
        public int FabricanteID { get; set; }

        [Required(ErrorMessage = "O nome do fabricante é obrigatório")]
        public string NomeFabricante { get; set; }

        [Required(ErrorMessage = "O país de origem é obrigatório")]
        public string PaisOrigem { get; set; }

        [Required(ErrorMessage = "O ano de fundação é obrigatório")]
        public int AnoFundacao { get; set; }

        [Required(ErrorMessage = "A URL é obrigatória")]
        public string URL { get; set; }
        public bool IsDeleted { get; set; }
        public FabricantesViewModel() { }
        public FabricantesViewModel(int fabricanteID, string nomeFabricante, string paisOrigem, int anoFundacao, string url, bool isDeleted = false)
        {
            FabricanteID = fabricanteID;
            NomeFabricante = nomeFabricante;
            PaisOrigem = paisOrigem;
            AnoFundacao = anoFundacao;
            URL = url;
            IsDeleted = isDeleted;
        }
    }
}
