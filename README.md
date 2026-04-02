# -_sistema_de_partidos-_arquitectura_MVC_en_.NET_-C-_- :.
⚽ Sistema de Partidos - Arquitectura MVC en .NET (C#):

<img width="1024" height="1024" alt="image" src="https://github.com/user-attachments/assets/a35db4b0-dc92-438f-8c10-31707a33fc9b" /> 

<img width="2532" height="1076" alt="image" src="https://github.com/user-attachments/assets/3b35939a-8423-47bd-83ea-0dca11d6784a" /> 

```

A continuación tienes el programa completo en .NET (C#), consolidado y listo para ejecutar, incluyendo:

✔ Arquitectura MVC
✔ Lógica con 3 condicionales anidados
✔ CRUD completo (Oracle 19c)
✔ Vista de consola modular, validada y escalable a WinForms/WPF
✔ Separación clara por capas .

🧩 1. ESTRUCTURA DEL PROYECTO:
/ProyectoPartidos
 ├── Models
 │     └── Partido.cs
 ├── Services
 │     └── ResultadoService.cs
 ├── Data
 │     └── PartidoDAO.cs
 ├── Controllers
 │     └── PartidoController.cs
 ├── Views
 │     └── VistaConsola.cs
 └── Program.cs

🧱 2. MODELO (Models/Partido.cs):
public class Partido
{
    public int Id { get; set; }
    public string EquipoLocal { get; set; }
    public string EquipoVisitante { get; set; }
    public int GolesLocal { get; set; }
    public int GolesVisitante { get; set; }
    public string Resultado { get; set; }
}

🧠 3. SERVICE (Services/ResultadoService.cs):

✔ Implementación con 3 condicionales anidados

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

🗃️ 4. DAO (Data/PartidoDAO.cs):

📌 Requiere paquete NuGet:

Oracle.ManagedDataAccess
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;

public class PartidoDAO
{
    private readonly string connectionString;

    public PartidoDAO(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public void Insertar(Partido p)
    {
        using (var conn = new OracleConnection(connectionString))
        {
            conn.Open();

            string sql = @"INSERT INTO PARTIDOS 
                          (EQUIPO_LOCAL, EQUIPO_VISITANTE, GOLES_LOCAL, GOLES_VISITANTE, RESULTADO) 
                          VALUES (:local, :visitante, :gl, :gv, :res)";

            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.Parameters.Add(":local", p.EquipoLocal);
                cmd.Parameters.Add(":visitante", p.EquipoVisitante);
                cmd.Parameters.Add(":gl", p.GolesLocal);
                cmd.Parameters.Add(":gv", p.GolesVisitante);
                cmd.Parameters.Add(":res", p.Resultado);

                cmd.ExecuteNonQuery();
            }
        }
    }

    public List<Partido> Listar()
    {
        var lista = new List<Partido>();

        using (var conn = new OracleConnection(connectionString))
        {
            conn.Open();

            string sql = "SELECT * FROM PARTIDOS";

            using (var cmd = new OracleCommand(sql, conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    lista.Add(new Partido
                    {
                        Id = Convert.ToInt32(reader["ID_PARTIDO"]),
                        EquipoLocal = reader["EQUIPO_LOCAL"].ToString(),
                        EquipoVisitante = reader["EQUIPO_VISITANTE"].ToString(),
                        GolesLocal = Convert.ToInt32(reader["GOLES_LOCAL"]),
                        GolesVisitante = Convert.ToInt32(reader["GOLES_VISITANTE"]),
                        Resultado = reader["RESULTADO"].ToString()
                    });
                }
            }
        }

        return lista;
    }

    public void Actualizar(Partido p)
    {
        using (var conn = new OracleConnection(connectionString))
        {
            conn.Open();

            string sql = @"UPDATE PARTIDOS 
                           SET GOLES_LOCAL=:gl, GOLES_VISITANTE=:gv, RESULTADO=:res 
                           WHERE ID_PARTIDO=:id";

            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.Parameters.Add(":gl", p.GolesLocal);
                cmd.Parameters.Add(":gv", p.GolesVisitante);
                cmd.Parameters.Add(":res", p.Resultado);
                cmd.Parameters.Add(":id", p.Id);

                cmd.ExecuteNonQuery();
            }
        }
    }

    public void Eliminar(int id)
    {
        using (var conn = new OracleConnection(connectionString))
        {
            conn.Open();

            string sql = "DELETE FROM PARTIDOS WHERE ID_PARTIDO=:id";

            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.Parameters.Add(":id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}

🎮 5. CONTROLLER (Controllers/PartidoController.cs):
using System.Collections.Generic;

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

🖥️ 6. VISTA CONSOLA (Views/VistaConsola.cs):
using System;
using System.Collections.Generic;

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
            opcion = LeerEntero("Seleccione una opción: ");
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

🚀 7. MAIN (Program.cs):
class Program
{
    static void Main(string[] args)
    {
        string connectionString = "User Id=usuario;Password=password;Data Source=localhost:1521/xe";

        var controller = new PartidoController(connectionString);
        var vista = new VistaConsola(controller);

        vista.Iniciar();
    }
}

🗄️ 8. SCRIPT ORACLE 19c:
CREATE TABLE PARTIDOS (
    ID_PARTIDO NUMBER GENERATED BY DEFAULT AS IDENTITY PRIMARY KEY,
    EQUIPO_LOCAL VARCHAR2(50),
    EQUIPO_VISITANTE VARCHAR2(50),
    GOLES_LOCAL NUMBER,
    GOLES_VISITANTE NUMBER,
    RESULTADO VARCHAR2(20)
);

🧪 9. EJEMPLO DE EJECUCION:
1. Crear Partido
Equipo Local: Nacional
Equipo Visitante: Millonarios
Goles Local: 3
Goles Visitante: 1

➡ Resultado automático: GANA LOCAL

🔥 CONCLUSIÓN TECNICA:

Este proyecto cumple con:

✔ Separación de responsabilidades (MVC)
✔ Persistencia real en Oracle 19c
✔ Lógica desacoplada (Service Layer)
✔ Vista reutilizable (clave para migración a UI gráfica)
✔ Base sólida para arquitectura empresarial :. / .
