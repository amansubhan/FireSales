using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FireSales
{
    public partial class Sale : Form
    {
        dbUtil db = new dbUtil();
        string pid, pname, descr, price, uom, istaxed = null;

        public Sale()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Sale_Load(object sender, EventArgs e)
        {
            textBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox2.AutoCompleteCustomSource = db.getProdNames();
            this.ActiveControl = textBox2;
            this.AcceptButton = button_qry;
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = null;
            TextBox t = sender as TextBox;
            if (t != null)
            {
                if (t.Text.Length >= 3)
                {
                    textBox2.AutoCompleteCustomSource = db.getProdNames();

                }
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button20_Click(object sender, EventArgs e)
        {

        }

        private void button_qry_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                DataTable pd = db.getProdDetail(textBox2.Text);
                foreach (DataRow row in pd.Rows)
                {
                    pid = row["id"].ToString();
                    descr = row["descr"].ToString();
                    price = row["price"].ToString();
                    uom = row["type"].ToString();
                    istaxed = row["istaxed"].ToString();
                }

                textBox1.Text = pid;
                textBox3.Text = descr;
                textBox4.Text = uom;
                textBox5.Text = price;
                textBox6.Text = istaxed;
            }
            else
            {
                DataTable pd = db.getProdByID(Convert.ToInt32(textBox1.Text));
                foreach (DataRow row in pd.Rows)
                {
                    pname = row["name"].ToString();
                    descr = row["descr"].ToString();
                    price = row["price"].ToString();
                    uom = row["type"].ToString();
                    istaxed = row["istaxed"].ToString();
                }

                textBox1.Text = null;
                textBox2.Text = pname;
                textBox3.Text = descr;
                textBox4.Text = uom;
                textBox5.Text = price;
                textBox6.Text = istaxed;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
