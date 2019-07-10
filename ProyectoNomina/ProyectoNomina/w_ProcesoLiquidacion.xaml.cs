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
    /// Lógica de interacción para w_ProcesoLiquidacion.xaml
    /// </summary>
    public partial class w_ProcesoLiquidacion : Window
    {
        ConexionDB datos;
        public w_ProcesoLiquidacion()
        {
            InitializeComponent();
            datos = new ConexionDB();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cargarGrilla();
        }
        
        private void cargarGrilla()
        {
            var lstLiquidaciones = datos.Liquidacion_Mensual.ToList();
            dgLiquidaciones.ItemsSource = lstLiquidaciones;
        }

        private void btnProcesar_Click(object sender, RoutedEventArgs e)
        {
            if (dgLiquidaciones.ItemsSource != null)
            {
                Liquidacion_Mensual dd = (Liquidacion_Mensual)dgLiquidaciones.SelectedItem;

                if (dd.Estado.ToString() != "A")
                {

                }
                else
                {
                    MessageBox.Show("Solo puede procesar una liquidacion con estado 'A'", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro para procesar la liquidación.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
