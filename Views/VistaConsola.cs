using sistema_de_partidos.Controllers;
using sistema_de_partidos.Models;
using System;
using System.Collections.Generic;

namespace sistema_de_partidos.Views
{
    public class VistaConsola
    {
        private readonly PartidoController controller;

        public VistaConsola(PartidoController controller)
        {
            this.controller = controller;
        }

        public void Iniciar()
        {
            int opcion;

            do
            {
                MostrarMenu();
                opcion = LeerEntero("SELECCIONE UNA OPCION: ");
                EjecutarOpcion(opcion);
                
            } while (opcion != 0);
        }

        private void MostrarMenu()
        {
            Console.WriteLine("\n==============================");
            Console.WriteLine("   SISTEMA DE PARTIDOS MVC");
            Console.WriteLine("==============================");
            Console.WriteLine("1. Crear Partido");
            Console.WriteLine("2. Listar Partidos");
            Console.WriteLine("3. Actualizar Partido");
            Console.WriteLine("4. Eliminar Partido");
            Console.WriteLine("0. Salir");
            Console.WriteLine("==============================");
        }

        private void EjecutarOpcion(int opcion)
        {
            try
            {
                switch (opcion)
                {
                    case 1: CrearPartido(); break;
                    case 2: ListarPartidos(); break;
                    case 3: ActualizarPartido(); break;
                    case 4: EliminarPartido(); break;
                    case 0: Console.WriteLine("Saliendo..."); break;
                    default: Console.WriteLine("Opción inválida"); break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void CrearPartido()
        {
            var p = new Partido
            {
                EquipoLocal = LeerTexto("Equipo Local: "),
                EquipoVisitante = LeerTexto("Equipo Visitante: "),
                GolesLocal = LeerEntero("Goles Local: "),
                GolesVisitante = LeerEntero("Goles Visitante: ")
            };

            controller.Crear(p);
            Console.WriteLine("✔ Registrado correctamente");
        }

        private void ListarPartidos()
        {
            var lista = controller.Listar();

            Console.WriteLine("\nID | LOCAL vs VISITANTE | GL-GV | RESULTADO");
            Console.WriteLine("------------------------------------------------");

            foreach (var p in lista)
            {
                Console.WriteLine($"{p.Id} | {p.EquipoLocal} vs {p.EquipoVisitante} | {p.GolesLocal}-{p.GolesVisitante} | {p.Resultado}");
            }
        }

        private void ActualizarPartido()
        {
            var p = new Partido
            {
                Id = LeerEntero("ID: "),
                GolesLocal = LeerEntero("Nuevo Goles Local: "),
                GolesVisitante = LeerEntero("Nuevo Goles Visitante: ")
            };

            controller.Actualizar(p);
            Console.WriteLine("✔ Actualizado correctamente");
        }

        private void EliminarPartido()
        {
            int id = LeerEntero("ID a eliminar: ");
            controller.Eliminar(id);
            Console.WriteLine("✔ Eliminado correctamente");
        }

        private string LeerTexto(string msg)
        {
            Console.Write(msg);
            return Console.ReadLine();
        }

        private int LeerEntero(string msg)
        {
            int valor;
            while (true)
            {
                Console.Write(msg);
                if (int.TryParse(Console.ReadLine(), out valor))
                    return valor;

                Console.WriteLine("⚠ Número inválido");
            }
        }
    }
}
