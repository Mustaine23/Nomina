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

                if (dd.Estado.ToString() == "A")
                {
                    var lstEmpleado = datos.Empleado.ToList();

                    foreach (Empleado ee in lstEmpleado)
                    {
                        int vIngreso = 0;
                        int vEgresos = 0;
                        int vTotal = 0;

                        var detalleLiquidacion = ee.Liquidacion_Mensual_Detalle.ToList().FindAll(x => x.Liquidacion_Id == dd.Id_Liquidacion);

                        if (detalleLiquidacion != null)
                        {
                            foreach (Liquidacion_Mensual_Detalle det in detalleLiquidacion)
                            {
                                if (det.Monto > 0)
                                {
                                    vIngreso = vIngreso + det.Monto;
                                }
                                else
                                {
                                    vEgresos = vEgresos + det.Monto * -1;
                                }
                            }
                        }

                        var adelantos = ee.Anticipo.ToList().FindAll(x => DateTime.Parse(x.Fecha_Definicion.ToString()).Month == dd.Mes && DateTime.Parse(x.Fecha_Definicion.ToString()).Year == dd.Anho && x.Estado == "A");

                        if (adelantos != null)
                        {
                            foreach (Anticipo an in adelantos)
                            {
                                vEgresos = vEgresos + an.Monto_Aprobado;
                            }
                        }
                    }

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
