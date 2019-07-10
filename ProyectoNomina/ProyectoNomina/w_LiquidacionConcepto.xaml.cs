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
        Empleado empleadoSeleccionado;

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
            cargarEmpleados();
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

        private void cargarEmpleados()
        {
            var lstUsuarios = datos.Empleado.ToList();
            cboEmpleado.ItemsSource = lstUsuarios;
            cboEmpleado.DisplayMemberPath = "Nombres";
            cboEmpleado.SelectedValuePath = "Id_Empleado";

            var lstConceptos = datos.Concepto.ToList().FindAll(X => X.Descripcion != "IPS");
            cboConcepto.ItemsSource = lstConceptos;
            cboConcepto.DisplayMemberPath = "Descripcion";
            cboConcepto.SelectedValuePath = "Id_Concepto";
        }

        private void limpiar()
        {
            txtLiquidacionSeleccionada.Text = txtMonto.Text = string.Empty;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (validaciones())
            {
                try
                {
                    Liquidacion_Mensual_Detalle lmd = new Liquidacion_Mensual_Detalle();
                    lmd.Concepto = (Concepto)cboConcepto.SelectedItem;
                    lmd.Empleado = (Empleado)cboEmpleado.SelectedItem;
                    lmd.Liquidacion_Mensual = liquidacionSeleccionada;

                    int vMonto = 0;
                    if (radioPositivo.IsChecked == true)
                        vMonto = int.Parse(txtMonto.Text);

                    if (radioNegativo.IsChecked == true)
                        vMonto = int.Parse(txtMonto.Text) * -1;

                    lmd.Monto = vMonto;

                    datos.Liquidacion_Mensual_Detalle.Add(lmd);
                    datos.SaveChanges();

                    MessageBox.Show("Detalle de Liquidacion insertado correctamente: ", "Proceso Completado", MessageBoxButton.OK, MessageBoxImage.Information);
                    limpiar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió el siguiente error: " + ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool validaciones()
        {
            bool retorno = true;

            if (int.Parse(txtMonto.Text) <= 0)
            {
                MessageBox.Show("El monto ingresado debe ser mayor a 0", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                retorno = false;
            }

            if ((radioNegativo.IsChecked == false) && (radioPositivo.IsChecked == false))
            {
                MessageBox.Show("Debe seleccionar si el concepto es positivo o negativo.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                retorno = false;
            }
            if (txtLiquidacionSeleccionada.Text == "")
            {
                MessageBox.Show("Debe seleccionar una liquidacion", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                retorno = false;
            }
            return retorno;
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dgConceptos.SelectedItem != null)
            {
                Liquidacion_Mensual_Detalle detalleEliminar = (Liquidacion_Mensual_Detalle)dgConceptos.SelectedItem;
                datos.Liquidacion_Mensual_Detalle.Remove(detalleEliminar);
                datos.SaveChanges();

                MessageBox.Show("Detalle Eliminado correctamente. ", "PROCESO COMPLETADO", MessageBoxButton.OK, MessageBoxImage.Information);
                limpiar();
            }
        }
    }
}
