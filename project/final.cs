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
    public partial class final : Form
    {
        public final()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=PC15\\SQLEXPRESS;Initial Catalog=employej;Integrated Security=True;");
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO emptab VALUES (@userid, @password)", con);
                cmd.Parameters.AddWithValue("@userid", int.Parse(textBox1.Text));
                cmd.Parameters.AddWithValue("@password", textBox2.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record added successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }
            finally
            {
                con.Close();
            }


        }
    }
}
