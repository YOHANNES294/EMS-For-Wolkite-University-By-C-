using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace project
{
    public partial class Form4 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=PC15\SQLEXPRESS;Initial Catalog=employej;Integrated Security=True;");

        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // Remove this line, no need to refresh the report on load
            // this.reportViewer1.RefreshReport();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("select * from emptab", con);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                con.Close(); 

                reportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource Source = new ReportDataSource("DataSet1", table);
                
                reportViewer1.LocalReport.ReportPath = @"C:\Users\yohan\OneDrive\Desktop\c#project\c#project\project\project\Report1.rdlc";
                reportViewer1.LocalReport.DataSources.Add(Source);
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=PC15\\SQLEXPRESS;Initial Catalog=employej;Integrated Security=True;"))
                {
                    con.Open();

                    if (!int.TryParse(textBox1.Text, out int userId))
                    {
                        MessageBox.Show("Invalid user ID. Please enter a valid integer.");
                        return;
                    }

                    SqlCommand command = new SqlCommand("SELECT * FROM emptab WHERE userId = @userId", con);
                    command.Parameters.AddWithValue("@userId", userId);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            u.Text = reader["userid"].ToString();
                            f.Text = reader["fname"].ToString();
                            l.Text = reader["lname"].ToString();
                            a.Text = reader["age"].ToString();
                           p.Text = reader["phone"].ToString();
                           g.Text = reader["gender"].ToString();
                            d.Text = reader["department"].ToString();
                            s.Text = reader["salary"].ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No data found for the specified user ID.");
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            dashboard dash = new dashboard();
            dash.Show();
            this.Hide();
        }
    }
}
