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
    /// Lógica de interacción para w_modificacionSalario.xaml
    /// </summary>
    public partial class w_modificacionSalario : Window
    {
        ConexionDB datos;
        Empleado empleadoAfortunado;
        public w_modificacionSalario()
        {
            InitializeComponent();
            datos = new ConexionDB();
        }

        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            string valida = validarDatos();
            if (valida == "OK")
            {
                Empleado_Salario_Historico ss = new Empleado_Salario_Historico();
                ss.Empleado = empleadoAfortunado;
                ss.Salario_Basico_Anterior = empleadoAfortunado.Salario_Basico;
                ss.Salario_Basico_Nuevo = int.Parse(txtSalarioNuevo.Text);

                datos.Empleado_Salario_Historico.Add(ss);
                datos.SaveChanges();

                empleadoAfortunado.Salario_Basico = int.Parse(txtSalarioNuevo.Text);

                datos.Entry(empleadoAfortunado).State = System.Data.Entity.EntityState.Modified;
                datos.SaveChanges();

                MessageBox.Show("Se actualizó correctamente el salario del Empleado", "PROCESO COMPLETADO", MessageBoxButton.OK, MessageBoxImage.Information);
                cargarEmpleados();
                limpiar();
            }
            else
            {
                MessageBox.Show(valida.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cargarEmpleados();
        }

        private void cargarEmpleados()
        {
            var lstEmpleados = datos.Empleado.ToList();
            dgEmpleados.ItemsSource = lstEmpleados;
        }

        private string validarDatos()
        {
            string resultado = "OK";
            if (txtDocumento.Text.Trim() == "")
            {
                resultado = "Debe seleccionar algún empleado para modificar el salario";
                return resultado;
            }

            if (txtSalarioNuevo.Text.Trim() == "")
            {
                resultado = "Debe cargar el nuevo salario del empleado";
                return resultado;
            }

            return resultado;
        }

        private void dgEmpleados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            empleadoAfortunado = (Empleado)dgEmpleados.SelectedItem;
            txtDocumento.Text = empleadoAfortunado.Nro_Documento;
            txtNombre.Text = empleadoAfortunado.Nombres;
            txtSalarioActual.Text = empleadoAfortunado.Salario_Basico.ToString();
        }

        private void limpiar()
        {
            txtDocumento.Text = txtNombre.Text = txtSalarioActual.Text = txtSalarioNuevo.Text = string.Empty;
        }
    }
}
