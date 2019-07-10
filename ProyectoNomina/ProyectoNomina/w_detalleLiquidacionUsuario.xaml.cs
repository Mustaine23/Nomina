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
    /// Lógica de interacción para w_detalleLiquidacionUsuario.xaml
    /// </summary>
    public partial class w_detalleLiquidacionUsuario : Window
    {
        ConexionDB datos;
        public w_detalleLiquidacionUsuario()
        {
            InitializeComponent();
            datos = new ConexionDB();
        }

        private void btnVerDetalle_Click(object sender, RoutedEventArgs e)
        {
            if ((cboEmpleado.SelectedItem != null) &&  (cboLiquidacion.SelectedItem != null))
            {
                Empleado ee = (Empleado)cboEmpleado.SelectedItem;
                Liquidacion_Mensual ll = (Liquidacion_Mensual)cboLiquidacion.SelectedItem;

                var liquidacionDetalle = datos.Liquidacion_Mensual_Detalle.ToList().FindAll(x => x.Liquidacion_Id == ll.Id_Liquidacion && x.Empleado_Id == ee.Id_Empleado);
                dgDetalle.ItemsSource = liquidacionDetalle;

                var resumenLiquidacion = datos.ResumenLiquidacion.ToList().FindAll(x => x.Id_Liquidacion == ll.Id_Liquidacion && x.Id_Empleado == ee.Id_Empleado);
                if (resumenLiquidacion.Count == 1)
                {
                    foreach (ResumenLiquidacion rr in resumenLiquidacion)
                    {
                        lblEgresos.Content = rr.MontoEgreso.ToString();
                        lblIngresos.Content = rr.MontoIngreso.ToString();
                        lblTotal.Content = (rr.MontoIngreso - rr.MontoEgreso).ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Error, favor comuniquese con informatica.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar el Empleado y la Liquidacion para visualizar el detalle.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cargarCombos()
        {
            var lstUsuarios = datos.Empleado.ToList();
            cboEmpleado.ItemsSource = lstUsuarios;
            cboEmpleado.DisplayMemberPath = "Nombres";
            cboEmpleado.SelectedValuePath = "Id_Empleado";

            var lstConceptos = datos.Liquidacion_Mensual.ToList();
            cboLiquidacion.ItemsSource = lstConceptos;
            cboLiquidacion.DisplayMemberPath = "Descripcion";
            cboLiquidacion.SelectedValuePath = "Id_Concepto";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cargarCombos();
        }
    }
}
