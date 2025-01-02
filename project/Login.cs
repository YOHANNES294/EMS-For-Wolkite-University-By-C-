using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;


namespace project
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            pictureBox5.Click += pictureBox5_Click;
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=PC15\\SQLEXPRESS;Initial Catalog=employej;Integrated Security=True;");
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from logtbl where userid='" + txtId.Text + "' and password = '" + txtPass.Text + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                dashboard hom = new dashboard();
                this.Hide();
                hom.Show(); 
            }
            else
            {
                MessageBox.Show("please check userid and password");
            }


            
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            txtId.Clear();
            txtPass.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Really, do you not have an account? You wish to create it a look.press OK ");
            newaccount acc = new newaccount();
            this.Hide();
            acc.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtPass.UseSystemPasswordChar = false;
                

            }
            else
            {
               
                txtPass.UseSystemPasswordChar = true;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblId_Click(object sender, EventArgs e)
        {

        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            txtPass.PasswordChar = '*';
        }

        private void lblPass_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

            try
            {
                
                string url = "https://https://www.wku.edu.et/en/index.php/en/academics/colleges/computing-informatics";

               
                Process.Start(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while trying to open the URL: " + ex.Message);
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
    }


