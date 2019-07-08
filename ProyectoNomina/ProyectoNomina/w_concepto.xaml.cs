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
    /// Lógica de interacción para w_concepto.xaml
    /// </summary>
    public partial class w_concepto : Window
    {
        ConexionDB datos;
        public w_concepto()
        {
            InitializeComponent();
            datos = new ConexionDB();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Concepto em = new Concepto();
                em.Descripcion = txtDescripcion.Text.Trim();
                em.Tipo = txtTipo.Text.Trim();
                

                datos.Concepto.Add(em);
                datos.SaveChanges();

                MessageBox.Show("Concepto insertado correctamente.", "PROCESO FINALIZADO", MessageBoxButton.OK, MessageBoxImage.Information);
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió el siguiente error: " + ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

         void LimpiarCampos()
        {
            txtDescripcion.Text = string.Empty;
            txtTipo.Text = string.Empty;
          
            actualizarGrilla();
            txtDescripcion.Focus();
        }
        void actualizarGrilla()
        {
            dgConceptos.ItemsSource = null;
            var dtConcepto = datos.Concepto.ToList();
            dgConceptos.ItemsSource = dtConcepto;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
