using sistema_de_partidos.Controllers;
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

        private int LeerEntero(string v)
        {
            throw new NotImplementedException();
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

        private void EliminarPartido()
        {
            throw new NotImplementedException();
        }

        private void ActualizarPartido()
        {
            throw new NotImplementedException();
        }

        private void ListarPartidos()
        {
            throw new NotImplementedException();
        }

        private void CrearPartido()
        {
            throw new NotImplementedException();
        }
    }
}
