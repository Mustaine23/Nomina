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
    /// Lógica de interacción para w_Liquidacion.xaml
    /// </summary>
    public partial class w_Liquidacion : Window
    {
        ConexionDB datos;
        string año;
        string mes;
        public w_Liquidacion()
        {
            InitializeComponent();
            datos = new ConexionDB();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cargarForm();
            cargarGrilla();
        }

        private void cargarForm()
        {
            año = DateTime.Now.Year.ToString().PadLeft(2, '0');
            mes = DateTime.Now.Month.ToString().PadLeft(2, '0');
            txtAño.Text = año;
            txtMes.Text = mes;
            getDescripcionMes(mes);
        }

        private void getDescripcionMes(string mes)
        {
            switch (mes)
            {
                case "01":
                    txtDescripcionMes.Text = "ENERO";
                    break;
                case "02":
                    txtDescripcionMes.Text = "FEBRERO";
                    break;
                case "03":
                    txtDescripcionMes.Text = "MARZO";
                    break;
                case "04":
                    txtDescripcionMes.Text = "ABRIL";
                    break;
                case "05":
                    txtDescripcionMes.Text = "MAYO";
                    break;
                case "06":
                    txtDescripcionMes.Text = "JUNIO";
                    break;
                case "07":
                    txtDescripcionMes.Text = "JULIO";
                    break;
                case "08":
                    txtDescripcionMes.Text = "AGOSTO";
                    break;
                case "09":
                    txtDescripcionMes.Text = "SEPTIEMBRE";
                    break;
                case "10":
                    txtDescripcionMes.Text = "OCTUBRE";
                    break;
                case "11":
                    txtDescripcionMes.Text = "NOVIEMBRE";
                    break;
                case "12":
                    txtDescripcionMes.Text = "DICIEMBRE1";
                    break;
            }
        }

        private void cargarGrilla()
        {
            var lstLiquidaciones = datos.Liquidacion_Mensual.ToList();
            dgLiquidaciones.ItemsSource = lstLiquidaciones;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Liquidacion_Mensual aa = new Liquidacion_Mensual();
                aa.Anho = short.Parse(txtAño.Text);
                aa.Fecha_Generacion = DateTime.Now;
                aa.Estado = "A";
                aa.Mes = short.Parse(txtMes.Text);
                aa.Usuario_Id = ProyectoNomina.Properties.Settings.Default.usuarioLogin;

                datos.Liquidacion_Mensual.Add(aa);
                datos.SaveChanges();

                MessageBox.Show("Planilla de Liquidación insertada correctamente.", "PROCESO FINALIZADO", MessageBoxButton.OK, MessageBoxImage.Information);
                cargarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió el siguiente error: " + ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
