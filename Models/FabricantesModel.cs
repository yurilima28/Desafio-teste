using System.ComponentModel.DataAnnotations;
using static ValidationModel;

namespace Intelectah.Models
{
    public class FabricantesModel
    {
        [Key]
        public int FabricanteID { get; set; }

        [Required(ErrorMessage = "O nome do fabricante é obrigatório")]
        [MaxLength(100)]
        [UniqueNomeFabricante]
        public string NomeFabricante { get; set; }

        [Required(ErrorMessage = "O país de origem é obrigatório")]
        public string PaisOrigem { get; set; }

        [Required(ErrorMessage = "O ano de fundação é obrigatório")]
        [AnoFundacao]
        public int AnoFundacao { get; set; }

        [Required(ErrorMessage = "A URL é obrigatória")]
        [MaxLength(255)]
        [Url]
        public string URL { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<VeiculosModel> Veiculos { get; set; } = new List<VeiculosModel>();
        public FabricantesModel() { }
        public FabricantesModel(int fabricanteID, string nomeFabricante, string paisOrigem, int anoFundacao, string url, bool isDeleted = false)
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
