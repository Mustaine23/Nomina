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
    /// Lógica de interacción para w_vacacionesPedidos.xaml
    /// </summary>
    public partial class w_vacacionesPedidos : Window
    {
        ConexionDB Datos;
        public w_vacacionesPedidos()
        {
            InitializeComponent();
            Datos = new ConexionDB();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnCargar_Click(object sender, RoutedEventArgs e)
        {
            pedidos();
        }

        private void pedidos()
        {
            var pedidosVacaciones = Datos.Vacaciones.ToList();
            dgPedidos.ItemsSource = pedidosVacaciones;
        }

        private void btnAutorizar_Click(object sender, RoutedEventArgs e)
        {
            if (dgPedidos.SelectedItem != null)
            {
                Vacaciones vac = (Vacaciones)dgPedidos.SelectedItem;
                if (vac.Estado == "pendiente")
                {
                    vac.Estado = "autorizado";

                    Datos.Entry(vac).State = System.Data.Entity.EntityState.Modified;
                    Datos.SaveChanges();

                    MessageBox.Show("Se autorizó correctamente el pedido de vacaciones", "PROCESO COMPLETADO", MessageBoxButton.OK, MessageBoxImage.Information);
                    pedidos();
                }
                else
                {
                    MessageBox.Show("El estado del pedido ya no es pendiente. Operacion no valida.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar algún pedido para poder rechazarlo.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRechazar_Click(object sender, RoutedEventArgs e)
        {
            if (dgPedidos.SelectedItem != null)
            {
                Vacaciones vac = (Vacaciones)dgPedidos.SelectedItem;
                if (vac.Estado == "pendiente")
                {
                    vac.Estado = "rechazado";

                    Datos.Entry(vac).State = System.Data.Entity.EntityState.Modified;
                    Datos.SaveChanges();

                    MessageBox.Show("Se rechazó correctamente el pedido de vacaciones", "PROCESO COMPLETADO", MessageBoxButton.OK, MessageBoxImage.Information);
                    pedidos();
                }
                else
                {
                    MessageBox.Show("El estado del pedido ya no es pendiente. Operacion no valida.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar algún pedido para poder rechazarlo.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
