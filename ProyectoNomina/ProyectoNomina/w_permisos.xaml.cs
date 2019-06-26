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
    /// Lógica de interacción para w_permisos.xaml
    /// </summary>
    public partial class w_permisos : Window
    {
        ConexionDB Datos;
        public w_permisos()
        {
            InitializeComponent();
            Datos = new ConexionDB();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            permisos();
        }

        private void btnCargarPermisos_Click(object sender, RoutedEventArgs e)
        {
            permisos();
        }

        public void permisos()
        {
            var permisos = Datos.Permisos.ToList();
            dgPermisos.ItemsSource = permisos;
        }

        private void btnRechazarPermiso_Click(object sender, RoutedEventArgs e)
        {
            if (dgPermisos.SelectedItem != null)
            {
                Permisos p = (Permisos)dgPermisos.SelectedItem;
                if (p.Estado == "pendiente")
                {
                    p.Estado = "rechazado";

                    Datos.Entry(p).State = System.Data.Entity.EntityState.Modified;
                    Datos.SaveChanges();

                    MessageBox.Show("Se rechazó correctamente el permiso", "PROCESO COMPLETADO", MessageBoxButton.OK, MessageBoxImage.Information);
                    permisos();
                }
                else
                {
                    MessageBox.Show("El estado del permiso ya no es pendiente. Operacion no valida.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar algun permiso para poder rechazarlo.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAutorizarPermiso_Click(object sender, RoutedEventArgs e)
        {
            if (dgPermisos.SelectedItem != null)
            {
                Permisos p = (Permisos)dgPermisos.SelectedItem;
                if (p.Estado == "pendiente")
                {
                    p.Estado = "autorizado";

                    Datos.Entry(p).State = System.Data.Entity.EntityState.Modified;
                    Datos.SaveChanges();

                    MessageBox.Show("Se autorizo correctamente el permiso", "PROCESO COMPLETADO", MessageBoxButton.OK, MessageBoxImage.Information);
                    permisos();
                }
                else
                {
                    MessageBox.Show("El estado del permiso ya no es pendiente. Operacion no valida.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar algun permiso para poder autorizar.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

