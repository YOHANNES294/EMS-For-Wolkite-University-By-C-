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
using System.Runtime.InteropServices;

namespace project
{
    public partial class form6 : Form
    {

        SqlConnection con = new SqlConnection("Data Source=PC15\\SQLEXPRESS;Initial Catalog=employej;Integrated Security=True;");

        public form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string sqlquery = "SELECT COUNT(*) FROM emptab";


            con.Open();

            using (SqlCommand cmd = new SqlCommand(sqlquery, con))
            {

                object result = cmd.ExecuteScalar();

                if (result != null)
                {

                    int count = Convert.ToInt32(result);

                   textBox1.Text = "Total records: " + count.ToString();
                }
                else
                {
                    MessageBox.Show("No records found.");
                }
            }


            con.Close();
        }
        SqlConnection cnn = new SqlConnection("Data Source=PC15\\SQLEXPRESS;Initial Catalog=employej;Integrated Security=True;");

        private void button2_Click(object sender, EventArgs e)
        {
            string sqlQuery = "SELECT SUM(salary) FROM emptab";

          
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            decimal sum = Convert.ToDecimal(result);
                          textBox2.Text = "Sum value: " + sum.ToString();
                        }
                        else
                        {
                            MessageBox.Show("No records found or the column contains NULL values.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error executing SQL query: " + ex.Message);
                }
            }
        }
       
        private void button3_Click(object sender, EventArgs e)
        {
            string sqlQuery = "SELECT AVG(salary) FROM emptab";

            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=PC15\\SQLEXPRESS;Initial Catalog=employej;Integrated Security=True;"))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        object result = command.ExecuteScalar();

                        if (result != DBNull.Value)
                        {
                            // It's safe to convert the result to decimal since AVG() function returns a numeric value
                            decimal average = Convert.ToDecimal(result);
                            textBox3.Text = "Average: " + average.ToString("0.00"); // Displaying with two decimal places
                        }
                        else
                        {
                            MessageBox.Show("No records found or the column contains NULL values.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error executing SQL query: " + ex.Message);
            }
            finally
            {
                // Ensure that connection is closed even if an exception occurs
                con.Close();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            dashboard hm = new dashboard();
            this.Hide();
            hm.Show();
        }
    }
}