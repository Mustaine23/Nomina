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
    /// Lógica de interacción para w_permisos.xaml
    /// </summary>
    public partial class w_permisos : Window
    {
        ConexionDB Datos;
        public w_permisos()
        {
            InitializeComponent();
            Datos = new ConexionDB();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnCargarPermisos_Click(object sender, RoutedEventArgs e)
        {
            permisos();
        }

        public void permisos()
        {
            var permisos = Datos.Permisos.ToList();
            dgPermisos.ItemsSource = permisos;
        }
    }
}

