using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace Login.Negocios.Datos
{
    internal class Usuarios
    {
        public Int32 IDUsuario;
        public string Nombre;
        public string APaterno;
        public string AMaterno;
        public string Usuario;
        public string Contraseña;

        public Usuarios()
        { 
        
        }

        public Usuarios(DataRow dr) 
        {
            if (dr["IDUsuario"] != DBNull.Value)
            {
                this.IDUsuario = Int32.Parse(dr["IDUsuario"].ToString());
            }

            if (dr["Nombre"] != DBNull.Value)
            {
                this.Nombre = dr["Nombre"].ToString();
            }

            if (dr["APaterno"] != DBNull.Value)
            {
                this.APaterno = dr["APaterno"].ToString();
            }

            if (dr["AMaterno"] != DBNull.Value)
            {
                this.AMaterno = dr["AMaterno"].ToString();
            }

            if (dr["Usuario"] != DBNull.Value)
            {
                this.Usuario = dr["Usuario"].ToString();
            }

            if (dr["Contraseña"] != DBNull.Value)
            {
                this.Contraseña = dr["Contraseña"].ToString();
            }

        }
    }
}
