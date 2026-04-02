using sistema_de_partidos.Controllers;
using sistema_de_partidos.Views;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "User Id=system;Password=Tapiero123;Data Source=localhost:1521/orcl";

        var controller = new PartidoController(connectionString);
        var vista = new VistaConsola(controller);

        vista.Iniciar();
    }
}   