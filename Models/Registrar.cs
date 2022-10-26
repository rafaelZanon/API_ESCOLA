//Criando classe com os atributos relacionado a
// tabela registrar
namespace LoginRegistrarApp.Models
{
    public class Registrar
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public int IsActive { get; set; }
    }
}