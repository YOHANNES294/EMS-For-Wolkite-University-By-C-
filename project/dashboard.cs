using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class dashboard : Form
    {
        public dashboard()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            home hm = new home();
            this.Hide();
            hm.Show();

        }

        private void label4_Click(object sender, EventArgs e)
        {
            EmployeeInfo view = new EmployeeInfo();
            this.Hide();
            view.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            EmployeeInfo view = new EmployeeInfo();
            this.Hide();
            view.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            home hm = new home();
            this.Hide();
            hm.Show();

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Form3 frm = new Form3();
            this.Hide();
            frm.Show();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            form6 frm = new form6();
            this.Hide();
            frm.Show();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Form4 frm = new Form4();
            this.Hide();
            frm.Show();
        }
    }
}
