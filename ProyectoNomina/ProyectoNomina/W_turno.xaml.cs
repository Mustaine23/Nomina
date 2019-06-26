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
    /// Lógica de interacción para W_turno.xaml
    /// </summary>
    public partial class W_turno : Window
    {
        ConexionDB datos;
        public W_turno()
        {
            InitializeComponent();
            datos = new ConexionDB();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            actualizargrilla();



        }

        void actualizargrilla()
        {
            var turno = datos.Turno.ToList();
            dgvTurnos.ItemsSource = turno;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            Turno pao = new Turno(); //se crea el objeto
            pao.Hora_Entrada = txthoraentrada.Text.ToString();
            pao.Hora_Salida = txthorasalida.Text.ToString();
            pao.Observaciones = txtobservacion.Text.ToString();

            datos.Turno.Add(pao);
            datos.SaveChanges();

            limpiar();
            actualizargrilla();

        }
        void limpiar()
        {
            txtobservacion.Text = "";
            txthoraentrada.Text = "";
            txthorasalida.Text = "";
            txtId.Text = "";
            txthoraentrada.Focus();

        }

        private void btnlimpiar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();

        }

        private void btnmodificar_Click(object sender, RoutedEventArgs e)
        {
            Turno modificar = new Turno();
            modificar.Id_Turno = int.Parse( txtId.Text.ToString());
            modificar.Hora_Entrada = txthoraentrada.Text.ToString();
            modificar.Hora_Salida = txthorasalida.Text.ToString();
            modificar.Observaciones = txtobservacion.Text.ToString();

            //Le ponemos una banderita de que se modicaron datos en la entidad..
            datos.Entry(modificar).State = System.Data.Entity.EntityState.Modified;
            datos.SaveChanges();

            limpiar();
            actualizargrilla();


        }

        private void dgvTurnos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(dgvTurnos.SelectedItem != null)
            {
                Turno pepo = (Turno)dgvTurnos.SelectedItem;

                txtId.Text = pepo.Id_Turno.ToString();
                txthoraentrada.Text = pepo.Hora_Entrada.ToString();
                txthorasalida.Text = pepo.Hora_Salida.ToString();
                txtobservacion.Text = pepo.Observaciones.ToString();
            }
        }
    }
}
