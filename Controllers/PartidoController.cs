using sistema_de_partidos.Data;
using sistema_de_partidos.Models;
using sistema_de_partidos.Services;
using System.Collections.Generic;

namespace sistema_de_partidos.Controllers
{
    public class PartidoController
    {
        private readonly PartidoDAO dao;
        private readonly ResultadoService service;

        public PartidoController(string connectionString)
        {
            dao = new PartidoDAO(connectionString);
            service = new ResultadoService();
        }

        public void Crear(Partido p)
        {
            p.Resultado = service.CalcularResultado(p.GolesLocal, p.GolesVisitante);
            dao.Insertar(p);
        }

        public List<Partido> Listar()
        {
            return dao.Listar();
        }

        public void Actualizar(Partido p)
        {
            p.Resultado = service.CalcularResultado(p.GolesLocal, p.GolesVisitante);
            dao.Actualizar(p);
        }

        public void Eliminar(int id)
        {
            dao.Eliminar(id);
        }
    }   
}
