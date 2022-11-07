using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Login.Negocios.AdministradorDatos
{
    class AdministradorUsuario
    {
        public static Double VerificaUsuario(System.String Usuario, System.String Contraseña)
        {
            int Bandera;
            OleDbDataReader Encontrado;
            String connectionString = String.Format(Login.Properties.Settings.Default.ConexionDB);
            string query = "SELECT * from Usuarios WHERE Usuario = '" + Usuario + "'And Contraseña = '" + Contraseña + "'";

            OleDbConnection myConnection = new OleDbConnection(connectionString);
            try
            {
                myConnection.Open();
                OleDbCommand myCommand = new OleDbCommand(query, myConnection);
                Encontrado = myCommand.ExecuteReader();
                if (Encontrado.Read() == false)
                {
                    Bandera = -1;
                }
                else
                {
                    Bandera = 1;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                MessageBox.Show(message);
                throw ex;
            }
            finally
            { 
              if (myConnection.State == ConnectionState.Open)
                    myConnection.Close();
            }
            return Bandera;
        }

        public static Login.Negocios.Datos.Usuarios ObtenerDatosUsuario(System.Double IDUsuario)
        {
            String connectionString = String.Format(Login.Properties.Settings.Default.ConexionDB);
            String query = "Select * from Usuarios where IDUsuario = " + IDUsuario;

            DataTable Dt = Login.Persistencia.AdministradorDatos.Executequery(connectionString, query, "Usuarios");
            if (Dt.Rows.Count == 0)
            {
                return null;
            }
            return new Login.Negocios.Datos.Usuarios(Dt.Rows[0]);
        }


        public static void BajaUsuario(Login.Negocios.Datos.Usuarios DatosUsuario)
        {
            String connectionString = String.Format(Login.Properties.Settings.Default.ConexionDB);
            String query = "Delete from Usuarios Where IDUsuario =" + DatosUsuario.IDUsuario ;

            Login.Persistencia.AdministradorDatos.ExecuteNonQuery(connectionString, query);
        }

        public static void AltaUsuario(Login.Negocios.Datos.Usuarios DatosUsuario)
        {
            String connectionString = String.Format(Login.Properties.Settings.Default.ConexionDB);
            String query = "Insert Into Usuarios(IDUsuario, Nombre, APaterno, AMaterno, Usuario, Contraseña) values (" + DatosUsuario.IDUsuario + "," + "'" + DatosUsuario.Nombre+ "," + "'" + DatosUsuario.APaterno + "," + "'" + DatosUsuario.AMaterno + "," + "'" + DatosUsuario.Usuario + "," + "'" + DatosUsuario.Contraseña +"," + "')";

            Login.Persistencia.AdministradorDatos.ExecuteNonQuery(connectionString, query);
        }

        public static void ModificarUsuario(Login.Negocios.Datos.Usuarios DatosUsuario)
        {
            String connectionString = String.Format(Login.Properties.Settings.Default.ConexionDB);
            String query = "update Usuarios set IDUsuario = " + DatosUsuario.IDUsuario + "', Nombre = '" + DatosUsuario.Nombre + "', Apaterno = '" + DatosUsuario.APaterno + "', AMaterno = '" + DatosUsuario.AMaterno + "' , Usuario = '" + DatosUsuario.Usuario + "' , Contraseña = '" + DatosUsuario.Contraseña + "' where IDUsuario = " + DatosUsuario.IDUsuario;

            Login.Persistencia.AdministradorDatos.ExecuteNonQuery(connectionString, query);
        }

        public static Login.Negocios.Datos.Usuarios ObtenerDatos(System.String Nombre,System.String APAterno, System.String AMaterno)
        {
            String connectionString = String.Format(Login.Properties.Settings.Default.ConexionDB);
            String query = "Select * from Usuarios where Nombre = " + Nombre + "' and APaterno = '" + APAterno +"' and AMaterno  = '"+ AMaterno + "'";

            DataTable Dt = Login.Persistencia.AdministradorDatos.Executequery(connectionString, query, "Usuarios");
            if (Dt.Rows.Count == 0)
            {
                return null;
            }
            return new Login.Negocios.Datos.Usuarios(Dt.Rows[0]);
        }
    }
}
