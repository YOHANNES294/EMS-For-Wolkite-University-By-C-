using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class Form3 : Form
    {
        SqlCommand cmd; 

        public Form3()
        {
            InitializeComponent();
            cmd = new SqlCommand("SELECT userid, fname, lname FROM emptab WHERE userid = @userid", con); 
        }

        SqlConnection con = new SqlConnection(@"Data Source=PC15\SQLEXPRESS;Initial Catalog=employej;Integrated Security=True;");

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
               
                con.Open();

                if (!int.TryParse(textBox9.Text, out int daysWorked) || !int.TryParse(textBox1.Text, out int userId))
                {
                    MessageBox.Show("Please enter valid numbers in both fields.");
                    return;
                }

             
                if (daysWorked < 1 || daysWorked > 30)
                {
                    MessageBox.Show("Please enter a number of days between 1 and 30.");
                    return;
                }

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@userid", userId);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    label5.Text = reader["fname"].ToString();
                    label6.Text = reader["lname"].ToString();

                    
                    int daysTillFullMonth = daysWorked * 1000;
                    textBox10.Text = daysTillFullMonth.ToString();

                    
                    if (daysWorked <= 20)
                    {
                        label1.Text = "You haven't done much.Do better in the future.";
                    }
                    else if (daysWorked >= 21 && daysWorked <= 29)
                    {
                        label1.Text = "Good work! Keep it up and strive for more.";
                    }
                    else if (daysWorked == 30)
                    {
                        label1.Text = "You are a excellent worker! Keep up the good work";
                    }
                }
                else
                {
                    MessageBox.Show("User ID not found.");
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
            
                con.Close();
            }






        }

        private void label10_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dashboard hm = new dashboard();
            this.Hide();
            hm.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            dashboard hm = new dashboard();
            this.Hide();
            hm.Show();
        }
    }
}
