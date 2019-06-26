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
    /// Lógica de interacción para w_adelantosPedidos.xaml
    /// </summary>
    public partial class w_adelantosPedidos : Window
    {
        ConexionDB datos;
        public w_adelantosPedidos()
        {
            InitializeComponent();
            datos = new ConexionDB();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cargardatos();
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            cargardatos();
        }

        void cargardatos()
        {
            dgSolicitudes.ItemsSource = null;
            var solicitudes = datos.Anticipo.ToList();
            dgSolicitudes.ItemsSource = solicitudes;
        }

        private void btnAutorizar_Click(object sender, RoutedEventArgs e)
        {
            if (dgSolicitudes.SelectedItem != null)
            {
                Anticipo ant = (Anticipo)dgSolicitudes.SelectedItem;
                if (ant.Estado == "pendiente")
                {
                    ant.Estado = "autorizado";

                    datos.Entry(ant).State = System.Data.Entity.EntityState.Modified;
                    datos.SaveChanges();

                    MessageBox.Show("Se autorizó correctamente el aniticipo", "PROCESO COMPLETADO", MessageBoxButton.OK, MessageBoxImage.Information);
                    cargardatos();
                }
                else
                {
                    MessageBox.Show("El estado del aniticipo ya no es pendiente. Operacion no valida.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar algún aniticipo para poder autorizarlo.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRechazar_Click(object sender, RoutedEventArgs e)
        {
            if (dgSolicitudes.SelectedItem != null)
            {
                Anticipo ant = (Anticipo)dgSolicitudes.SelectedItem;
                if (ant.Estado == "pendiente")
                {
                    ant.Estado = "Rechazado";

                    datos.Entry(ant).State = System.Data.Entity.EntityState.Modified;
                    datos.SaveChanges();

                    MessageBox.Show("Se Rechazó correctamente el aniticipo", "PROCESO COMPLETADO", MessageBoxButton.OK, MessageBoxImage.Information);
                    cargardatos();
                }
                else
                {
                    MessageBox.Show("El estado del aniticipo ya no es pendiente. Operacion no valida.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar algún aniticipo para poder rechazarlo.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
