using System;
using System.Windows.Forms;

namespace FireSales
{
    public partial class SetUOM : Form
    {
        dbUtil db = new dbUtil();
        

        public SetUOM()
        {
            InitializeComponent(); 
        }
        
        private void DataBind()
        {
            try
            {
                dataGridView1.DataSource = db.getAllUom();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw e;
            }
        }

        private void SetUOM_Load(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            }
        }

        private void btn_Done_Click(object sender, EventArgs e)
        {
            try
            {
                var id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                MessageBox.Show(id.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DataBind();
            }
        }



        private void button4_Click(object sender, EventArgs e)
        {
            int d = dataGridView1.RowCount;
            int stat = 0;
            if (d > 0)
            {
                try
                {
                    var id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                    stat = db.deleteUom(id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                MessageBox.Show(stat.ToString() + " Record deleted");
                db.getAllUom();
            }


        }

        private void SetUOM_Shown(object sender, EventArgs e)
        {
            this.Refresh();
            System.Threading.Thread.Sleep(100);
            DataBind();
        }
    }
}
