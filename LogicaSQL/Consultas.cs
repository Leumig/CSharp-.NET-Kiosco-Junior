﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaSQL
{
    public class Consultas : Conexion
    {
        public Consultas(string connectionString) : base(connectionString) { }
        public Consultas() : base("Server=.;Database=KIOSCO_JUNIOR;Trusted_Connection=True;") { }

        /// <summary>
        /// Abre la conexión SQL y crea un comando al cual le pasa la conexión y una consulta.
        /// En un DataReader guarda lo que lea el comando. Y lo que lea, lo guarda en un DataTable.
        /// Después, cierra al DataReader y a la conexión SQL.
        /// </summary>
        /// <param name="consulta"></param>
        /// <returns>Retorna un DataTable creado en base a lo leído</returns>
        protected DataTable EjecutarConsulta(string consulta)
        {
            Abrir();

            SqlCommand command = new SqlCommand(consulta, Connection);
            SqlDataReader reader = command.ExecuteReader();

            DataTable dataTable = new DataTable();
            dataTable.Load(reader);

            reader.Close();
            Cerrar();

            return dataTable;
        }

        /// <summary>
        /// Abre la conexión, ejecuta el comando NonQuery y cierra la conexión.
        /// </summary>
        /// <param name="command"></param>
        protected void EjecutarConsultaNonQuery(SqlCommand command)
        {
            Abrir();

            command.ExecuteNonQuery();

            Cerrar();
        }

        /// <summary>
        /// Intenta abrir la conexión, y hace una consulta simple para 
        /// comprobar que funciona la conexión.
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void ProbarConexion()
        {
            try
            {
                Abrir();

                SqlCommand command = new SqlCommand("SELECT 1", Connection);
                command.ExecuteNonQuery();

                Cerrar();
            }
            catch (Exception ex)
            {
                Cerrar();
                throw new Exception("No se pudo establecer la conexión con la base de datos", ex);
            }
        }

    }
}
