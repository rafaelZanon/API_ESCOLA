namespace ProjetoLoja_API.Models
{
    public class Cliente
    {
        public int id {get; set;}

        public string? userName {get; set;}

        public string? senha {get; set;}

        public string? email {get; set;}

        public string? role { get; set; }
    }
}