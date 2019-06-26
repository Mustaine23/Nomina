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
    /// Lógica de interacción para w_AsignacionTurnos.xaml
    /// </summary>
    public partial class w_AsignacionTurnos : Window
    {
        ConexionDB DATOS;
        Empleado emple;
        public w_AsignacionTurnos()
        {
            InitializeComponent();
            DATOS = new ConexionDB();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cargarEmpleados();
        }
        
        private void cargarEmpleados()
        {
            var empleados = DATOS.Empleado.ToList();
            dgEmpleados.ItemsSource = empleados;
        }

        private void cargarTurnos()
        {
            var turnos = DATOS.Turno.ToList();
            cboTurnos.ItemsSource = turnos;
            cboTurnos.DisplayMemberPath = "Observaciones";
            cboTurnos.SelectedValuePath = "Id_Turno";
        }


        private void dgEmpleados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgEmpleados.SelectedItem != null)
            {
                emple = (Empleado)dgEmpleados.SelectedItem;
                txtDocumento.Text = emple.Nro_Documento;
                txtNombre.Text = emple.Nombres;
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                emple.Turno = (Turno)cboTurnos.SelectedItem;
                DATOS.Entry(emple).State = System.Data.Entity.EntityState.Modified;
                DATOS.SaveChanges();

                MessageBox.Show("Se actualizo el turno del empleado correctamente", "PROCESO COMPLETADO", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ocurrio el siguiente error: " + ex.ToString());
            }
    }
    }
}
