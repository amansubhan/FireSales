using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FireSales
{
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddProduct ap = new AddProduct();
            ap.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Products_Load(object sender, EventArgs e)
        {
           

        }

        private void Products_Shown(object sender, EventArgs e)
        {
            this.Refresh();
            System.Threading.Thread.Sleep(100);
            dbComp db = new dbComp();
            dataGridView1.DataSource = db.GetAllProducts();
        }
    }
}
