namespace Intelectah.ViewModel
{
    public class ConcessionariasViewModel
    {
        public int ConcessionariaID { get; set; }
        public string Nome { get; set; }
        public EnderecoViewModel Endereco { get; set; } = new EnderecoViewModel();
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int CapacidadeMax { get; set; }
        public bool IsDeleted { get; set; }

        public ConcessionariasViewModel()
        {
        }

        public ConcessionariasViewModel(int concessionariaID, string nome, string telefone, string email, int capacidadeMax, bool isDeleted = false)
        {
            ConcessionariaID = concessionariaID;
            Nome = nome;
            Telefone = telefone;
            Email = email;
            CapacidadeMax = capacidadeMax;
            IsDeleted = isDeleted;
            Endereco = new EnderecoViewModel(); 
        }
    }

    public class EnderecoViewModel
    {
        public string EnderecoCompleto { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }
    }
}
