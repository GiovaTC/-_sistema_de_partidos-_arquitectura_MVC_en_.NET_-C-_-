
namespace sistema_de_partidos.Models
{
    public class Partido
    {
        public int Id { get; set; }
        public string EquipoLocal { get; set; }
        public string EquipoVisitante { get; set; }
        public int GolesLocal { get; set; }
        public int GolesVisitante { get; set; }
        public string Resultado { get; set; }
    }   
}
