﻿using System;
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
    /// Lógica de interacción para Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void menuEmpleados_Click(object sender, RoutedEventArgs e)
        {
            w_Empleados venEmpleados = new w_Empleados();
            venEmpleados.ShowDialog();
        }

        private void menuAdSalVer_Click(object sender, RoutedEventArgs e)
        {
            w_adelantosPedidos venAdelantos = new w_adelantosPedidos();
            venAdelantos.ShowDialog();
        }

        private void menuVacacionesVer_Click(object sender, RoutedEventArgs e)
        {
            w_vacacionesPedidos venVacaciones = new w_vacacionesPedidos();
            venVacaciones.ShowDialog();
        }

        private void menuPermisosVer_Click(object sender, RoutedEventArgs e)
        {
            w_permisos pp = new w_permisos();
            pp.ShowDialog();
        }

        private void menuConceptos_Click(object sender, RoutedEventArgs e)
        {
            w_concepto cc = new w_concepto();
            cc.ShowDialog();
        }

        private void menuSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void menuSalarios_Click(object sender, RoutedEventArgs e)
        {
            w_modificacionSalario venModiSal = new w_modificacionSalario();
            venModiSal.ShowDialog();
        }

        private void menuTurnos_Click(object sender, RoutedEventArgs e)
        {
            W_turno venTurno = new W_turno();
            venTurno.ShowDialog();
        }

        private void menuAsignaTurnos_Click(object sender, RoutedEventArgs e)
        {
            w_AsignacionTurnos ss = new w_AsignacionTurnos();
            ss.ShowDialog();
        }

        private void menuLiquidacion_Click(object sender, RoutedEventArgs e)
        {
            w_Liquidacion asd = new w_Liquidacion();
            asd.ShowDialog();
        }

        private void menuCargarConceptos_Click(object sender, RoutedEventArgs e)
        {
            w_LiquidacionConcepto qq = new w_LiquidacionConcepto();
            qq.ShowDialog();
        }

        private void menuDetalle_Click(object sender, RoutedEventArgs e)
        {
            w_detalleLiquidacionUsuario qwerty = new w_detalleLiquidacionUsuario();
            qwerty.ShowDialog();
        }

        private void menuProceso_Click(object sender, RoutedEventArgs e)
        {
            w_ProcesoLiquidacion dd = new w_ProcesoLiquidacion();
            dd.ShowDialog();
        }

        private void menuAdSalCargar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
