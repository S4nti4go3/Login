using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Login.Persistencia
{
    class AdministradorDatos
    {
        public static DataTable Executequery(string ConnexionString, string query, string TableName)
        {
            try
            {
                OleDbConnection myConnection = new OleDbConnection(ConnexionString);
                OleDbDataAdapter myAdapter = new OleDbDataAdapter(query, myConnection);
                DataSet ds = new DataSet();
                myAdapter.Fill(ds, TableName);
                ds.Tables[0].TableName = TableName;
                return ds.Tables[0];

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                MessageBox.Show(message);
                throw ex;
            }
        }

        public static void ExecuteNonQuery(string ConnectionString, string query)
        {
            OleDbConnection myConnection = new OleDbConnection(ConnectionString);
            try
            {
                myConnection.Open();
                OleDbCommand myCommand = new OleDbCommand(query, myConnection);
                myCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                MessageBox.Show(message);
                //throw ex;
            }
            finally
            {
                if (myConnection.State == ConnectionState.Open)
                {
                    myConnection.Close();
                }
            }
        }

        public static int SiguienteID(string tableName, string idField)
        {
            int val;
            string ConnectionString = string.Format(Login.Properties.Settings.Default.ConexionDB);
            OleDbConnection myConnection = new OleDbConnection(ConnectionString);

            string Query = "select max (" + idField + ") from " + tableName;
            myConnection.Open();
            OleDbCommand myCommand  = new OleDbCommand(Query, myConnection);   

            object maxValue  = myCommand.ExecuteScalar();
            myConnection.Close();
            if (maxValue == DBNull.Value) return 1;
            else
            {
                val = int.Parse((maxValue).ToString());
                return val+1;
            }
        }
    }
}
