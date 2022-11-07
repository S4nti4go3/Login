using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }
     
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Double Bandera;
            Bandera = Login.Negocios.AdministradorDatos.AdministradorUsuario.VerificaUsuario(txtUsuario.Text, txtContraseña.Text);
            if (Bandera == 1)
            {
                Login.Formularios.frmPrincipal formulario = new Formularios.frmPrincipal();
                formulario.ShowDialog();
                txtUsuario.Clear();
                txtContraseña.Clear();
                txtUsuario.Focus();
            }
            else
            {
                MessageBox.Show("Usuario no encontrado");
                txtUsuario.Clear();
                txtContraseña.Clear();
                txtUsuario.Focus();

            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
