using System;
using System.Windows.Forms;


namespace FireSales
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public static bool isSalesWinOpen = false;
        public static bool isProductWinOpen = false;
        public static string ProgressPercentage;
       

        private void button1_Click(object sender, EventArgs e)
        {
            Sale sale = new Sale();
            if (isSalesWinOpen == false)
            {
                sale.MdiParent = this;
                isSalesWinOpen = true;
                sale.Show();
            }
            else
            {
                MessageBox.Show("POS is already open!");
            }
            
        }

        
        private void button3_Click(object sender, EventArgs e)
        {
            if (isProductWinOpen == false)
            {
                Products prod = new Products();
                prod.MdiParent = this;
                isProductWinOpen = true;
                prod.Show();
            }
            else
            {
                MessageBox.Show("Products is already open!");
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            SetUOM suom = new SetUOM();
            suom.MdiParent = this;
            //isProductWinOpen = true;
            suom.Show();
        }

        public void setProgress()
        {
            toolStripProgressBar1.Value = Convert.ToInt32(ProgressPercentage);
        }

        public void setProgresstext()
        {
            toolStripStatusLabel3.Text = ProgressPercentage;
        }

        private void toolStripStatusLabel3_Click(object sender, EventArgs e)
        {

        }
    }
}
