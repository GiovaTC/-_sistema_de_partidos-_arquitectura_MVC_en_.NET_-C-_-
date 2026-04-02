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
    }
}
