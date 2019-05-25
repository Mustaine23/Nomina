using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProyectoNomina
{
    /// <summary>
    /// Lógica de interacción para w_Empleados.xaml
    /// </summary>
    public partial class w_Empleados : Window
    {
        ConexionDB datos;
        public w_Empleados()
        {
            InitializeComponent();
            datos = new ConexionDB();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Empleado em = new Empleado();
                em.Nombres = txtNombres.Text.Trim();
                em.Apellidos = txtApellidos.Text.Trim();
                em.Nro_Documento = txtNroDoc.Text.Trim();
                em.Direccion = txtDireccion.Text.Trim();
                em.Nro_Telefono = txtNroTelefono.Text.Trim();
                em.Fecha_Nacimiento = DateTime.Parse(dtpFechaNacimiento.Text);
                em.Fecha_Incorporacion = DateTime.Parse(dtpFechaIncorporacion.Text);
                em.Imagen_Perfil = imgPerfil.Source.ToString();

                datos.Empleadoes.Add(em);
                datos.SaveChanges();

                MessageBox.Show("Empleado insertado correctamente.", "PROCESO FINALIZADO", MessageBoxButton.OK, MessageBoxImage.Information);
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió el siguiente error: " + ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        void LimpiarCampos()
        {
            txtNombres.Text = string.Empty;
            txtApellidos.Text = string.Empty;
            txtNroDoc.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtNroTelefono.Text = string.Empty;
            dtpFechaNacimiento.Text = string.Empty;
            dtpFechaIncorporacion.Text = string.Empty;
            txtSalario.Text = string.Empty;
            imgPerfil.Source = null;
            actualizarGrilla();
            txtNombres.Focus();
        }

        void actualizarGrilla()
        {
            dgEmpleados.ItemsSource = null;
            var dtEmpleados = datos.Empleadoes.ToList();
            dgEmpleados.ItemsSource = dtEmpleados;
        }

        private void btnImagen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Elegir una imagen para el perfil";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";

            if (op.ShowDialog() == true)
            {
                imgPerfil.Source = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("En desarrollo :D", ":O", MessageBoxButton.OK, MessageBoxImage.Hand);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("En desarrollo :D", ":O", MessageBoxButton.OK, MessageBoxImage.Hand);
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }
    }
}

