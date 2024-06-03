using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Examen_Oficial.Data.DataAccess
{
    internal class Mundial
    {
        // Información de conexión a la base de datos
        private string connectionString = "Server = localhost; Database = db_universidad; Uid=root;Pwd=alex123";


        //constructor
        public Mundial(string servidor, string usur, string pwd)
        {
            connectionString = "Server=" + servidor + ";Database=db_universidad;Uid=" + usur + ";Pwd=" + pwd + ";";
        }

        // LEER TODAS LAS SELECCIONES
        public DataTable LeerSelecciones()
        {
            DataTable selecciones = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM db_universidad.mundial2018;";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(selecciones);
                    }
                }
            }
            return selecciones;
        }


        // Método para crear un nueva SELECCION
        public int CrearSeleccion(string id_selecciones, string nombre_selecciones, string jugadores_destacados, string no_clasificaciones_mundial, string frases_selecciones, DateTime fecha_ultimo_mundial_ganado, int valor_plantilla)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO mundial2018 (id_selecciones, nombre_selecciones, jugadores_destacados, no_clasificaciones_mundial, frases_selecciones, fecha_ultimo_mundial_ganado, valor_plantilla) VALUES (@id_selecciones, @nombre_selecciones, @jugadores_destacados, @no_clasificaciones_mundial, @frases_selecciones, @fecha_ultimo_mundial_ganado, @valor_plantilla)";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id_selecciones", id_selecciones);
                    command.Parameters.AddWithValue("@nombre_selecciones", nombre_selecciones);
                    command.Parameters.AddWithValue("@jugadores_destacados", jugadores_destacados);
                    command.Parameters.AddWithValue("@no_clasificaciones_mundial", no_clasificaciones_mundial);
                    command.Parameters.AddWithValue("@frases_selecciones", frases_selecciones);
                    command.Parameters.AddWithValue("@fecha_ultimo_mundial_ganado", fecha_ultimo_mundial_ganado);
                    command.Parameters.AddWithValue("@valor_plantilla", valor_plantilla);

                    return command.ExecuteNonQuery();
                }
            }
        }



        //Busca una SELECCION por su ID
        public DataTable buscarSeleccionPorId(int Id_Selecciones)
        {
            DataTable seleccion = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM db_universidad.mundial2018 WHERE Id_Selecciones = @Id_Selecciones";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id_Selecciones", Id_Selecciones);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(seleccion);
                    }
                }
               
            }
            return seleccion;

        }



        //SELECCION ACTUALIZADA
        public int ActualizarSeleccion(int id_selecciones, string nombre_selecciones, string jugadores_destacados, int no_clasificaciones_mundial, string frases_selecciones, DateTime fecha_ultimo_mundial_ganado, int valor_plantilla)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Pregunta de confirmación
                    DialogResult confirmacion = MessageBox.Show("¿Está seguro que desea actualizar la selección?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (confirmacion == DialogResult.Yes)
                    {
                        string sql = "UPDATE db_universidad.mundial2018 SET nombre_selecciones = @nombre_selecciones, jugadores_destacados = @jugadores_destacados, no_clasificaciones_mundial = @no_clasificaciones_mundial, frases_selecciones = @frases_selecciones, fecha_ultimo_mundial_ganado = @fecha_ultimo_mundial_ganado, valor_plantilla = @valor_plantilla WHERE id_selecciones = @id_selecciones";

                        using (MySqlCommand command = new MySqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@id_selecciones", id_selecciones);
                            command.Parameters.AddWithValue("@nombre_selecciones", nombre_selecciones);
                            command.Parameters.AddWithValue("@jugadores_destacados", jugadores_destacados);
                            command.Parameters.AddWithValue("@no_clasificaciones_mundial", no_clasificaciones_mundial);
                            command.Parameters.AddWithValue("@frases_selecciones", frases_selecciones);
                            command.Parameters.AddWithValue("@fecha_ultimo_mundial_ganado", fecha_ultimo_mundial_ganado);
                            command.Parameters.AddWithValue("@valor_plantilla", valor_plantilla);

                            return command.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // No hacer nada si el usuario cancela la operación
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar la selección: " + ex.Message);
                    return 0;
                }
            }
        }



        //ELIMINAR SELECCION
        public int EliminarSeleccion(int Id_Selecciones)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = "DELETE FROM db_universidad.mundial2018 WHERE Id_Selecciones = @Id_Selecciones";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id_Selecciones", Id_Selecciones);
                    return command.ExecuteNonQuery();
                }
            }
        }
    }
}
       
