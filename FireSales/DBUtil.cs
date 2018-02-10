using System;
using System.Data.SQLite;
using System.Data;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace FireSales
{
    class dbUtil
    {
        private BackgroundWorker bw;
        const string filename = @"1database.db";
        SQLiteConnection conn = new SQLiteConnection("Data Source=" + filename + ";Version=3;");
        DataSet ds = new DataSet();

        public dbUtil()
        {
            
        }

        
        public DataView Select(string sql)
        {
            try
            {
                var da = new SQLiteDataAdapter(sql, conn);
                da.Fill(ds);
                conn.Close();
                return ds.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            conn.Close();
            return ds.Tables[0].DefaultView; ;
        }

        public int Update(string sql)
        {
            conn.Open();
            int status = 0;
            using (SQLiteCommand qry = new SQLiteCommand(conn))
            {
                qry.CommandText = sql;
                status = qry.ExecuteNonQuery();
            }
            conn.Close();
            return status;
        }

        public int Insert(string sql)
        {
            conn.Open();
            int status = 0;
            using (SQLiteCommand qry = new SQLiteCommand(conn))
            {
                qry.CommandText = sql;
                status = qry.ExecuteNonQuery();
            }
            conn.Close();
            return status;
        }

        public int Delete(string sql)
        {
            conn.Open();
            int status = 0;
            using (SQLiteCommand qry = new SQLiteCommand(conn))
            {
                qry.CommandText = sql;
                status = qry.ExecuteNonQuery();
            }
            conn.Close();
            return status;
        }

        public DataView GetAllProducts()
        {
            const string sql = "select * from products;";
            return Select(sql);
        }

        public int deleteUom(int id)
        {
            string sql = "delete from uom where id = " + id + ";";
            return Delete(sql);
        }

        public DataView getAllUom()
        {
            const string sql = "select * from uom;";
            return Select(sql);
        }

        public AutoCompleteStringCollection getProdNames()
        {
            string qry = "select name,id from products;";
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(qry, conn);
            SQLiteDataReader reader = cmd.ExecuteReader();
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            while (reader.Read())
            {
                MyCollection.Add(reader.GetString(0));
            }
            conn.Close();
            return MyCollection;
        }

        public DataTable getProdDetail(string pname)
        {         
            return Select("select p.id, p.descr, p.price, u.type, p.istaxed " +
                "from products p, uom u" +
                " where p.name = '" + pname + "'" +
                "and p.uom = u.id;").ToTable(); 
        }

        public DataTable getProdByID(int pid)
        {
            return Select("select p.id, p.name, p.descr, p.price, u.type, p.istaxed " +
                "from products p, uom u" +
                " where p.id = '" + pid + "'" +
                "and p.uom = u.id;").ToTable();
        }
    }
}