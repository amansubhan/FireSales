using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using Finisar.SQLite;

namespace FireSales
{
    class DBUtil
    {
        static SQLiteConnection sqlite_conn;
        static SQLiteCommand sqlite_cmd;
        static SQLiteDataReader sqlite_datareader;

        public DBUtil()
        {

        }

        ~DBUtil()
        {
            sqlite_conn.Close();
        }

        private void setConnection()
        {
            sqlite_conn = new SQLiteConnection("Data Source=1database.db;Version=3");
            sqlite_conn.Open();
        }

        public void executeDML()
        {
            setConnection();
            // create a new SQL command:
            sqlite_cmd = sqlite_conn.CreateCommand();

            sqlite_cmd.CommandText = "INSERT INTO test (id, text) VALUES (3, 'Test Text 3');";

            // And execute this again ;D
            sqlite_cmd.ExecuteNonQuery();

            // We are ready, now lets cleanup and close our connection:
            sqlite_conn.Close();
        }

        public void executeDQL()
        {
            setConnection();
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM test";

            // Now the SQLiteCommand object can give us a DataReader-Object:
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            // The SQLiteDataReader allows us to run through the result lines:
            while (sqlite_datareader.Read()) // Read() returns true if there is still a result line to read
            {
                // Print out the content of the text field:
                //System.Console.WriteLine(sqlite_datareader["text"]);
                String output = sqlite_datareader.GetString(0);
                MessageBox.Show("Reply from database: " + output);
            }

            // We are ready, now lets cleanup and close our connection:
            sqlite_conn.Close();
        }

        public DataTable exectueUOMList()
        {
            setConnection();
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT id, type FROM uom";

            sqlite_datareader = sqlite_cmd.ExecuteReader();

            DataSet ds = new DataSet();
            DataTable t = ds.Tables.Add("UOM");
            t.Columns.Add("id", typeof(int));
            t.Columns.Add("type", typeof(string));
            DataRow row = null;

            while (sqlite_datareader.Read())
                {
                int id = sqlite_datareader.GetInt32(0);
                string type = sqlite_datareader.GetString(1);
                row = t.NewRow();
                row["id"] = id;
                row["type"] = type;
                t.Rows.Add(row);
                }
            return t;
        }

        /*public List<ProductsClass> getAllProducts()
        {
            setConnection();

            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM products";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            List<ProductsClass> prod = new List<ProductsClass>();

            while (sqlite_datareader.Read())
            {
                prod.Add(new ProductsClass(sqlite_datareader.GetInt32(0), 
                    sqlite_datareader.GetString(1), 
                    sqlite_datareader.GetString(2), 
                    sqlite_datareader.GetInt32(3), 
                    sqlite_datareader.GetInt32(4), 
                    sqlite_datareader.GetBoolean(5)));
            }

            return prod;
        }*/
    }

}
