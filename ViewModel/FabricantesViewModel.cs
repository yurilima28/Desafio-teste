using Microsoft.AspNetCore.Mvc.Rendering;

namespace Intelectah.ViewModel
{
    public class FabricantesViewModel
    {
        public int FabricanteID { get; set; }
        public string NomeFabricante { get; set; }
        public string PaisOrigem { get; set; }
        public int AnoFundacao { get; set; }
        public string URL { get; set; }
        public IEnumerable<SelectListItem> OpcaoPaises { get; set; } = new List<SelectListItem>();
        public bool IsDeleted { get; set; }
        public FabricantesViewModel() { }
        public FabricantesViewModel(int fabricanteID, string nomeFabricante, string paisOrigem, int anoFundacao, string url, IEnumerable<SelectListItem> opcaoPaises, bool isDeleted = false)
        {
            FabricanteID = fabricanteID;
            NomeFabricante = nomeFabricante;
            PaisOrigem = paisOrigem;
            AnoFundacao = anoFundacao;
            URL = url;
            OpcaoPaises = opcaoPaises;
            IsDeleted = isDeleted;
        }
    }
}
