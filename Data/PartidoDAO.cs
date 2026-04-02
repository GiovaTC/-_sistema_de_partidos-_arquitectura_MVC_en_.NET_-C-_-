using Oracle.ManagedDataAccess.Client;
using sistema_de_partidos.Models;
using System;
using System.Collections.Generic;
using System.Data;

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
            try
            {
                using (var conn = new OracleConnection(connectionString))
                {
                    conn.Open();

                    string sql = @"INSERT INTO PARTIDOS
                                   (EQUIPO_LOCAL, EQUIPO_VISITANTE, GOLES_LOCAL, GOLES_VISITANTE, RESULTADO)
                                   VALUES (:local, :visitante, :gl, :gv, :res)";

                    using (var cmd = new OracleCommand(sql, conn))
                    {
                        cmd.BindByName = true;

                        cmd.Parameters.Add(":local", OracleDbType.Varchar2).Value = p.EquipoLocal;
                        cmd.Parameters.Add(":visitante", OracleDbType.Varchar2).Value = p.EquipoVisitante;
                        cmd.Parameters.Add(":gl", OracleDbType.Int32).Value = p.GolesLocal;
                        cmd.Parameters.Add(":gv", OracleDbType.Int32).Value = p.GolesVisitante;
                        cmd.Parameters.Add(":res", OracleDbType.Varchar2).Value = p.Resultado;

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Insertar: " + ex.ToString());
                throw;
            }
        }

        public List<Partido> Listar()
        {
            var lista = new List<Partido>();

            try
            {
                using (var conn = new OracleConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT * FROM PARTIDOS";

                    using (var cmd = new OracleCommand(sql, conn))
                    {
                        cmd.BindByName = true;

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
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Listar: " + ex.ToString());
                throw;
            }

            return lista;
        }

        public void Actualizar(Partido p)
        {
            try
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
                        cmd.BindByName = true;

                        cmd.Parameters.Add(":gl", OracleDbType.Int32).Value = p.GolesLocal;
                        cmd.Parameters.Add(":gv", OracleDbType.Int32).Value = p.GolesVisitante;
                        cmd.Parameters.Add(":res", OracleDbType.Varchar2).Value = p.Resultado;
                        cmd.Parameters.Add(":id", OracleDbType.Int32).Value = p.Id;

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Actualizar: " + ex.ToString());
                throw;
            }
        }

        public void Eliminar(int id)
        {
            try
            {
                using (var conn = new OracleConnection(connectionString))
                {
                    conn.Open();

                    string sql = "DELETE FROM PARTIDOS WHERE ID_PARTIDO = :id";

                    using (var cmd = new OracleCommand(sql, conn))
                    {
                        cmd.BindByName = true;

                        cmd.Parameters.Add(":id", OracleDbType.Int32).Value = id;

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Eliminar: " + ex.ToString());
                throw;
            }
        }
    }
}