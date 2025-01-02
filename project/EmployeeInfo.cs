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
using System.IO;
using System.Collections.ObjectModel;

namespace project
{
    public partial class EmployeeInfo : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=PC15\SQLEXPRESS;Initial Catalog=employej;Integrated Security=True;");
        int showAllClickCount = 0;

        public EmployeeInfo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string password = txtshowall.Text; // Assuming txtshowall is the TextBox for entering the password

            if (password == "manager")
            {
                // Grant access
                //isAccessGranted = true;
                IsAccessible = true;
                // Your existing code to fetch data and display in DataGridView
                SqlCommand cmd = new SqlCommand("SELECT * FROM emptab", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                da.Fill(table);
                dataGridView1.DataSource = table;
            }
            else
            {
                // Increment the attempt count
                showAllClickCount++;

                // Check if the maximum attempts are reached
                if (showAllClickCount >= 3)
                {
                    // Show login form
                    MessageBox.Show("Sorry, you can't use it anymore. Please try again.");
                    Login lgn = new Login();
                    lgn.Show();
                    this.Hide();
                }
                else
                {
                    // Show message indicating incorrect password
                    MessageBox.Show("Incorrect password. Please try again.");
                }
            }
        }

        private void btnview_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT userid, fname, lname, age, phone, gender, department, photo,position,salary FROM emptab WHERE userid = @userid", con);
                cmd.Parameters.AddWithValue("@userid", int.Parse(textBox1.Text));

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    textBox1.Text = reader["userid"].ToString();
                    textBox2.Text = reader["fname"].ToString();
                    textBox3.Text = reader["lname"].ToString();
                    textBox4.Text = reader["age"].ToString();
                    textBox5.Text = reader["phone"].ToString();
                    textBox6.Text = reader["department"].ToString();
                    

                    string gender = reader["gender"].ToString();
                    if (gender == "male")
                    {
                        radioButton1.Checked = true;
                    }
                    else if (gender == "female")
                    {
                        radioButton2.Checked = true;
                    }

                    byte[] imageBytes = reader["photo"] as byte[];
                    if (imageBytes != null)
                    {
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            picshow.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        picshow.Image = null;
                    }
                    textBox8.Text = reader["position"].ToString();
                    textBox7.Text = reader["salary"].ToString();

                    reader.Close();
                }
                else
                {
                    MessageBox.Show("User ID not found");
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            dashboard dash = new dashboard();
            this.Hide();
            dash.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
