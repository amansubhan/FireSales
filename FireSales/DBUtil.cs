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
        string sql = null;
        int progress = 0;

        public dbUtil()
        {
            this.bw = new BackgroundWorker();
            this.bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            this.bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            this.bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            this.bw.WorkerReportsProgress = true;
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //this.label1.Text = "The answer is: " + e.Result.ToString();
            //this.btn_Done.Enabled = true;
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //window.setProgress(e.ProgressPercentage);
            //MainWindow.setProgresstext(e.ProgressPercentage.ToString());
            //string status = e.ProgressPercentage.ToString();
            //MainWindow.ProgressPercentage = e.ProgressPercentage.ToString();
            //Debug.WriteLine(e.ProgressPercentage.ToString());
            //this.btn_Done.Text = e.ProgressPercentage.ToString();
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;

            /*for (int i = 0; i < 100; ++i)
            {
                // report your progres
                worker.ReportProgress(i);

                // pretend like this a really complex calculation going on eating up CPU time
                System.Threading.Thread.Sleep(100);
            }
            e.Result = "42";*/
            conn.Close();
            progress = 10;
            conn.Open();
            worker.ReportProgress(10);
            var da = new SQLiteDataAdapter(sql, conn);
            da.Fill(ds);
            conn.Close();
            sql = null;
            worker.ReportProgress(100);
            progress = 100;

        }

        public DataView Select(string sql)
        {
            try
            {
                if (!this.bw.IsBusy)
                {
                    this.bw.RunWorkerAsync();
                    //this.btn_Done.Enabled = false;
                }
                while (progress <= 100)
                {

                    if (progress == 100)
                    {
                        Debug.WriteLine(progress.ToString());
                        return ds.Tables[0].DefaultView;
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            conn.Close();
            return null;
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
            sql = "select * from products;";
            return Select(sql);
        }

        public int deleteUom(int id)
        {
            sql = "delete from uom where id = " + id + ";";
            return Delete(sql);
        }

        public DataView getAllUom()
        {
            sql = "select * from uom;";
            return Select(sql);
        }

        public AutoCompleteStringCollection getProdNames()
        {
            string qry = "select name from products;";
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

        public DataView getProdDetails(string pname)
        {
            DataView pd = new DataView();
            pd = Select("select descr, price from products where name = " + pname + ";");
            return pd;
        }
    }
}