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
    /// Lógica de interacción para w_LiquidacionConcepto.xaml
    /// </summary>
    public partial class w_LiquidacionConcepto : Window
    {
        ConexionDB datos;
        Liquidacion_Mensual liquidacionSeleccionada;

        public w_LiquidacionConcepto()
        {
            InitializeComponent();
            datos = new ConexionDB();
        }

        private void btnActualizarLiquidaciones_Click(object sender, RoutedEventArgs e)
        {
            cargarGrillaLiquidaciones();
        }

        private void cargarGrillaLiquidaciones()
        {
            var lstLiquidaciones = datos.Liquidacion_Mensual.ToList();
            dgLiquidaciones.ItemsSource = lstLiquidaciones;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cargarGrillaLiquidaciones();
        }

        private void dgLiquidaciones_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgLiquidaciones.SelectedItem != null)
            {
                liquidacionSeleccionada = (Liquidacion_Mensual)dgLiquidaciones.SelectedItem;
                txtLiquidacionSeleccionada.Text = liquidacionSeleccionada.Anho.ToString() + "/" + liquidacionSeleccionada.Mes.ToString().PadLeft(2, '0');
                getDetalleLiquidacion();
            }
        }

        private void getDetalleLiquidacion()
        {
            var lstDetalleLiquidacion = liquidacionSeleccionada.Liquidacion_Mensual_Detalle;
            dgConceptos.ItemsSource = lstDetalleLiquidacion;
        }
    }
}
