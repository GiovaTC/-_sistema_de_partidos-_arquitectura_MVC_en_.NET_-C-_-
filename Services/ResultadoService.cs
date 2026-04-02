
namespace sistema_de_partidos.Services
{
    public class ResultadoService
    {
        public string CalcularResultado(int golesLocal, int golesVisitante)
        {
            if (golesLocal >= 0 && golesVisitante >= 0)
            {
                if (golesLocal > golesVisitante)
                {
                    return "GANA LOCAL";
                }
                else
                {
                    if (golesLocal == golesVisitante)
                    {
                        return "EMPATE";
                    }
                    else
                    {
                        return "GANA VISITANTE";
                    }
                }
            }
            else
            {
                return "DATOS INVALIDOS";
            }
        }
    }
}
