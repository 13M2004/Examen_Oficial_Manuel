using Examen_Oficial.Data.DataAccess;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Examen_Oficial
{
    public partial class Form1 : Form
    {
        
        private string[] Nombre_Selecciones =
        {
            "Qatar",
            "Ecuador",
            "Senegal",
            "Paises Bajos",
            "Inglaterra",
            "Iran",
            "Estados Unidos",
            "Gales",
            "Argentina",
            "Arabia Saudita",
            "Mexico",
            "Polonia",
            "Francia",
            "Australia",
            "Dinamarca",
            "Tunez",
            "España",
            "Costa Rica",
            "Alemania",
            "Japon",
            "Belgica",
            "Canada",
            "Marruecos",
            "Croacia",
            "Brasil",
            "Serbia",
            "Suiza",
            "Camerun",
            "Portugal",
            "Ghana",
            "Uruguay",
            "Corea del Sur"
        };

        private Mundial seleccion;

        public Form1()
        {
            InitializeComponent();
            seleccion = new Mundial ("localhost", "root", "alex123");
            Form1_Load();
        }


        private void Form1_Load()
        {
            // Llenar el ComboBox con las SELECCIONES
            commboboxselecciones.Items.AddRange(Nombre_Selecciones);
        }

        private void buttonCargar_Click(object sender, EventArgs e)
        {
            dataGridViewMundial.DataSource = seleccion.LeerSelecciones();

        }



        //AGREGAR SELECCION
        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            string id_selecciones = textBoxId_Selecciones.Text;
            string nombre_selecciones = textBoxNombre_Selecciones.Text;
            string jugadores_destacados = textBoxJugadores_Destacados.Text;
            string no_clasificaciones_mundial = textBoxNo_Clasificaciones_Mundial.Text;
            string frases_selecciones = textBoxFrases_Selecciones.Text;
            DateTime fecha_ultimo_mundial_ganado = DateTime.Now;
            int valor_plantilla = (int)numericUpDownValor_Plantilla.Value;

           
            int respuesta = seleccion.CrearSeleccion(id_selecciones, nombre_selecciones, jugadores_destacados, no_clasificaciones_mundial, frases_selecciones, fecha_ultimo_mundial_ganado, valor_plantilla);
            if (respuesta > 0)
            {
                MessageBox.Show("SELECCION creada correctamente");
                dataGridViewMundial.DataSource = seleccion.LeerSelecciones();
            }
            else
            {
                MessageBox.Show("Error al crear la SELECCION");
            }
        }



        //BUCAS SELECCION POR ID
        private void buscarPorId_Selecciones()
        {
            int idSeleccionABuscar = Convert.ToInt32(textBoxId_Selecciones.Text);

            DataTable SeleccionEncontrado = seleccion.buscarSeleccionPorId(idSeleccionABuscar);

            if (SeleccionEncontrado.Rows.Count > 0)
            {
                //La seleccion fue encontrada
                string nombre_selecciones = SeleccionEncontrado.Rows[0]["nombre_selecciones"].ToString();
                string jugadores_destacados = SeleccionEncontrado.Rows[0]["jugadores_destacados"].ToString();
                string no_clasificaciones_mundial = SeleccionEncontrado.Rows[0]["no_clasificaciones_mundial"].ToString();
                string frases_selecciones = SeleccionEncontrado.Rows[0]["no_clasificaciones_mundial"].ToString();
                DateTime fecha_ultimo_mundial_ganado = Convert.ToDateTime(SeleccionEncontrado.Rows[0]["fecha_ultimo_mundial_ganado"].ToString());
                int valor_plantilla = int.Parse(SeleccionEncontrado.Rows[0]["valor_plantilla"].ToString());

                textBoxNombre_Selecciones.Text = nombre_selecciones;
                textBoxJugadores_Destacados.Text = jugadores_destacados;
                textBoxNo_Clasificaciones_Mundial.Text = no_clasificaciones_mundial;
                textBoxFrases_Selecciones.Text = frases_selecciones;
                dateTimePickerFecha_Ultimo_Mundial_Ganado.Value = fecha_ultimo_mundial_ganado; 
                numericUpDownValor_Plantilla.Value = valor_plantilla;
            }
            else
            {
                // El SELECCION no fue encontrada
                Console.WriteLine("No se encontró la SELECCION con ese ID: " + idSeleccionABuscar);
            }
        }
        
        private void buttonBuscar_Click_1(object sender, EventArgs e)
        {
            buscarPorId_Selecciones();
        }


        //ACTUALIZAR SELECCION
        private void buttonActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                int id_selecciones = int.Parse(textBoxId_Selecciones.Text);
                string nombre_selecciones = textBoxNombre_Selecciones.Text;
                string jugadores_destacados = textBoxJugadores_Destacados.Text;
                int no_clasificaciones_mundial = int.Parse(textBoxNo_Clasificaciones_Mundial.Text);
                string frases_selecciones = textBoxFrases_Selecciones.Text;
                DateTime fecha_ultimo_mundial_ganado = dateTimePickerFecha_Ultimo_Mundial_Ganado.Value;
                int valor_plantilla = (int)numericUpDownValor_Plantilla.Value;

                // Pregunta de confirmación
                DialogResult confirmacion = MessageBox.Show("¿Está seguro que desea actualizar la selección?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    int respuesta = seleccion.ActualizarSeleccion(id_selecciones, nombre_selecciones, jugadores_destacados, no_clasificaciones_mundial, frases_selecciones, fecha_ultimo_mundial_ganado, valor_plantilla);
                    if (respuesta > 0)
                    {
                        MessageBox.Show("SELECCION Actualizado correctamente");
                        dataGridViewMundial.DataSource = seleccion.LeerSelecciones();
                    }
                    else
                    {
                        MessageBox.Show("Error al actualizar la SELECCION");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Dato ingresado no válido: \"{ex.Message}\"");
            }
        }



        //ELIMINAR SELECCION
        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            int Id_Selecciones = int.Parse(textBoxId_Selecciones.Text);


            int respuesta = seleccion.EliminarSeleccion(Id_Selecciones);
            if (respuesta > 0)
            {
                MessageBox.Show("SELECCION eliminada correctamente");
                dataGridViewMundial.DataSource = seleccion.LeerSelecciones();
            }
            else
            {
                MessageBox.Show("Error al Eliminar la SELECCION");
            }


        }

        private void buttonSalir_Click(object sender, EventArgs e)
        {
            // Mostrar mensaje de despedida
            MessageBox.Show("¡Hasta luego INGENIERO!", "Despedida", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Salir del programa
            Application.Exit();
        }
    }

}
