using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login.Formularios
{
    public partial class frmUsuarios : Form
    {
        public string pageAction;

        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'dsLogin.Usuarios' Puede moverla o quitarla según sea necesario.
            this.usuariosTableAdapter.Fill(this.dsLogin.Usuarios);

        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EstableceEstadoEditable(bool edit)
        {
            btnAgregar.Enabled = !edit;
            btnModificar.Enabled = !edit;
            btnBorrar.Enabled = !edit;
            BtnSalir.Enabled = !edit;

            dgUsuarios.Enabled = !edit;

            btnCancelar.Enabled = edit;
            btnGuardar.Enabled = edit;

            txtNombre.ReadOnly = !edit;
            txtAPaterno.ReadOnly = !edit;
            txtAMaterno.ReadOnly = !edit;
            txtUsuario.ReadOnly = !edit;
            txtContraseña.ReadOnly = !edit;

        }

        private void LimpiarCampos()
        {
            txtNombre.Text = string.Empty;
            txtAPaterno.Text = string.Empty;
            txtAMaterno.Text = string.Empty;
            txtUsuario.Text = string.Empty;
            txtContraseña.Text = string.Empty;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            pageAction = "ADD";
            LimpiarCampos();
            EstableceEstadoEditable(true);
            txtNombre.Focus();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            pageAction = "EDIT";
            LimpiarCampos();
            EstableceEstadoEditable(true);
            txtNombre.Focus();
        }
        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (dgUsuarios.CurrentCell == null)
            {
                return;
            }

            double Id = double.Parse(dgUsuarios[0, dgUsuarios.CurrentRow.Index].Value.ToString());


            Login.Negocios.Datos.Usuarios DatosUsuario = Login.Negocios.AdministradorDatos.AdministradorUsuario.ObtenerDatosUsuario(Id);

            if (DatosUsuario == null)
            {
                return;
            }

            if (Id == 1)
            {
                MessageBox.Show("No puedo Borrar al primer usuario", "Atencion", MessageBoxButtons.OK);
                return;
            }
            else
            {
                if (MessageBox.Show("¿Estas seguro que quieres borrar el registro?", "Atencion", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    Login.Negocios.AdministradorDatos.AdministradorUsuario.BajaUsuario(DatosUsuario);
                }
                else
                {
                    return;
                }
                this.usuariosTableAdapter.GetData();
                this.usuariosTableAdapter.Fill(this.dsLogin.Usuarios);
                EstableceEstadoEditable(false);
                LimpiarCampos();
                CargaUsuarioActual();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            EstableceEstadoEditable(false);
        }

        private void CargaUsuarioActual()
        {
            if (dgUsuarios.CurrentCell == null)
            { 
                btnModificar.Enabled = false;
                btnBorrar.Enabled = false;
                return;

            }

            double Id = double.Parse(dgUsuarios[0, dgUsuarios.CurrentRow.Index].Value.ToString());

            Login.Negocios.Datos.Usuarios DatosUsuario = Login.Negocios.AdministradorDatos.AdministradorUsuario.ObtenerDatosUsuario(Id);

            if (DatosUsuario == null)
            {
                MessageBox.Show("No puedo obtener los datos","Atencion",MessageBoxButtons.OK);
                return;
            }

            txtNombre.Text = DatosUsuario.Nombre;
            txtAPaterno.Text= DatosUsuario.APaterno;
            txtAMaterno.Text = DatosUsuario.AMaterno;
            txtUsuario.Text = DatosUsuario.Usuario;
            txtContraseña.Text = DatosUsuario.Contraseña;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Nombre vacio", "Guardado", MessageBoxButtons.OK);
                txtNombre.Focus();
                return;
            }

            if (txtUsuario.Text == String.Empty)
            {
                MessageBox.Show("Usuario vacio", "Guardado", MessageBoxButtons.OK);
                txtUsuario.Focus();
                return;
            }


            if (txtContraseña.Text == String.Empty)
            {
                MessageBox.Show("Contraseña vacia", "Guardado", MessageBoxButtons.OK);
                txtContraseña.Focus();
                return;
            }

            Login.Negocios.Datos.Usuarios DatosUsuario;

            if (pageAction == "ADD")
            {
                DatosUsuario = new Negocios.Datos.Usuarios();
                int id = Login.Persistencia.AdministradorDatos.SiguienteID("Usuarios", "IDUsuario");
                DatosUsuario.IDUsuario = id;
            }
            else
            {
                if (pageAction != "EDIT")
                {
                    MessageBox.Show("No hay usuarios", "Atencion", MessageBoxButtons.OK);
                    EstableceEstadoEditable(false);
                    return;
                }
                else
                {
                    DatosUsuario = new Negocios.Datos.Usuarios();
                }
            }

            DatosUsuario.Nombre = txtNombre.Text.Trim();
            DatosUsuario.APaterno = txtAPaterno.Text.Trim();
            DatosUsuario.AMaterno = txtAMaterno.Text.Trim();
            DatosUsuario.Usuario = txtUsuario.Text;
            DatosUsuario.Contraseña = txtContraseña.Text;

            if (pageAction == "ADD")
            {
                int id = Login.Persistencia.AdministradorDatos.SiguienteID("Usuarios", "IDUsuario");
                DatosUsuario.IDUsuario = id;
                Login.Negocios.AdministradorDatos.AdministradorUsuario.AltaUsuario(DatosUsuario);
            }
            else
            {
                Login.Negocios.AdministradorDatos.AdministradorUsuario.ModificarUsuario(DatosUsuario);
            }
            this.usuariosTableAdapter.GetData();
            this.usuariosTableAdapter.Fill(this.dsLogin.Usuarios);
            EstableceEstadoEditable(false);
        }
    }
}
