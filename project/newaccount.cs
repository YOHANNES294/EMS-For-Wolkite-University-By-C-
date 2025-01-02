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

namespace project
{
    public partial class newaccount : Form
    {
        public newaccount()
        {
            InitializeComponent();
        }

        private void sign_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPass.Text != txtconfirm.Text)
                {
                    MessageBox.Show("Password and Confirm Password do not match.");
                    return; 
                }

                SqlConnection con = new SqlConnection("Data Source=PC15\\SQLEXPRESS;Initial Catalog=employej;Integrated Security=True;");
                con.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO logtbl VALUES (@userid, @password)", con);
                cmd.Parameters.AddWithValue("@userid", int.Parse(txtId.Text));
                cmd.Parameters.AddWithValue("@password", txtPass.Text);

                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Sign up successful");
                    Login log = new Login();
                    this.Hide();
                    log.Show();
                }
                else
                {
                    MessageBox.Show("Sign up failed. Please try again.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }

        private void backlogin_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            this.Hide();
            log.Show();
        }

        private void lblPass_Click(object sender, EventArgs e)
        {

        }
    }
}
