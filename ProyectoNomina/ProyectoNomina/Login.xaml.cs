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
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        ConexionDB datos;
        string vUser = string.Empty;
        string vPass = string.Empty;
        public Login()
        {
            InitializeComponent();
            datos = new ConexionDB();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string valida = validar();
            if (valida == "OK")
            {
                #region Tratamos de hacer el LOGIN
                string iniciaSesion = login();
                if (iniciaSesion == "OK")
                {
                    Menu mm = new Menu();
                    mm.ShowDialog();
                }
                else
                {
                    MessageBox.Show(iniciaSesion, "ERROR", MessageBoxButton.OK);
                    txtUser.Focus();
                }
                #endregion
            }
            else
            {
                MessageBox.Show(valida, "ERROR", MessageBoxButton.OK);
            }
        }

        public string validar()
        {
            string resultado = "OK";
            vUser = txtUser.Text.Trim().ToUpper();
            vPass = passwordBox.Password.ToString();

            if (vUser == "")
            {
                resultado = "Ingrese el usuario.";
                return resultado;
            }

            if (vPass == "")
            {
                resultado = "Ingrese la contraseña.";
                return resultado;
            }

            return resultado;
        }

        public string login()
        {
            string resultado = "OK";
            bool existeUser = false;

            var lstUsuarios = datos.Usuario.ToList();

            foreach (Usuario user in lstUsuarios)
            {
                if (user.Usuario1.ToString() == vUser)
                {
                    existeUser = true;
                    if (user.Password.ToString() != vPass)
                    {
                        resultado = "Contraseña Incorrecta";
                        return resultado;
                    }
                }
            }

            if (!existeUser)
                resultado = "Usuario ingresado no existe.";
            

            return resultado;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUser.Focus();

        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtUser_LostFocus(object sender, RoutedEventArgs e)
        {
            passwordBox.Focus();
        }
    }
}
