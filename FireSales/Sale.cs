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
                DataTable d = new DataTable();
                d = db.getProdDetails(textBox2.Text.ToString()).ToTable();
                Debug.WriteLine("Enter Pressed");
                Debug.WriteLine(d.Rows[0].ToString());
                textBox5.Text = d.Rows[0].ToString();

        }
    }
}
