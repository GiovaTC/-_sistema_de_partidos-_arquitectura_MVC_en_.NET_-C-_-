using Oracle.ManagedDataAccess.Client;
using sistema_de_partidos.Models;
using System;
using System.Collections.Generic;

namespace sistema_de_partidos.Data
{
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
                                SET GOLES_LOCAL = :gl,
                                    GOLES_VISITANTE = :gv,
                                    RESULTADO = :res
                                WHERE ID_PARTIDO = :id";

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

                string sql = "DELETE FROM PARTIDOS WHERE ID_PARTIDO = :id";

                using (var cmd = new OracleCommand(sql, conn))
                {
                    cmd.Parameters.Add(":id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
