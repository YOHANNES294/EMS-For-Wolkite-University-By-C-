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
using System.Drawing.Imaging;
using System.IO;

namespace project
{
    public partial class home : Form
    {
        public home()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=PC15\SQLEXPRESS;Initial Catalog=employej;Integrated Security=True;");
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void home_Load(object sender, EventArgs e)
        {
            BindData();
        }
        void BindData()
        {
            SqlCommand cnn = new SqlCommand("select * from emptab", con);
            SqlDataAdapter da = new SqlDataAdapter(cnn);
            var ds = new DataSet();
            DataTable table = new DataTable();
            da.Fill(table);
            dataGridView1.DataSource = table;
            

        }

        private void btnAdd_Click(object sender, EventArgs e)

        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text) ||
    string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox5.Text) || string.IsNullOrWhiteSpace(textBox6.Text) ||
    string.IsNullOrWhiteSpace(textBox7.Text) || comboBox1.SelectedItem == null || picshow.Image == null)
            {
                MessageBox.Show("Missing information");
            }
            else if (!radioButton1.Checked && !radioButton2.Checked)
            {
                MessageBox.Show("Please select a gender");
            }
            else
            {
                
                if (!int.TryParse(textBox1.Text, out int userId) || userId < 0)
                {
                    MessageBox.Show("Please enter a valid non-negative integer for User ID.");
                    return; 
                }

                try
                {
                    con.Open();

                   
                    byte[] imageBytes;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        picshow.Image.Save(ms, picshow.Image.RawFormat);
                        imageBytes = ms.ToArray();
                    }

                   
                    SqlCommand cmd = new SqlCommand("INSERT INTO emptab (userid, fname, lname, age, phone, gender, department, photo, position, salary) " +
                                                    "VALUES (@userid, @fname, @lname, @age, @phone, @gender, @department, @photo, @position, @salary)", con);
                    cmd.Parameters.AddWithValue("@userid", userId); 
                    cmd.Parameters.AddWithValue("@fname", textBox2.Text);
                    cmd.Parameters.AddWithValue("@lname", textBox3.Text);
                    cmd.Parameters.AddWithValue("@age", int.Parse(textBox4.Text));
                    cmd.Parameters.AddWithValue("@phone", textBox5.Text); 
                    cmd.Parameters.AddWithValue("@gender", radioButton1.Checked ? "male" : "female"); 
                    cmd.Parameters.AddWithValue("@department", textBox6.Text);
                    cmd.Parameters.AddWithValue("@photo", imageBytes);
                    cmd.Parameters.AddWithValue("@position", comboBox1.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@salary", float.Parse(textBox7.Text)); 

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Your file has been added successfully!");
                    BindData(); 
                }
                catch (FormatException)
                {
                    MessageBox.Show("Please enter valid numeric values for 'Age', 'Phone', and 'Salary'.");
                }
                catch (OverflowException)
                {
                    MessageBox.Show("The value entered is too large or too small.");
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

        




    }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text="";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            radioButton1.Text = "";
            radioButton2.Text = "";
            picshow.Image = null;
            comboBox1.SelectedItem = null;


        }

        private void button3_Click(object sender, EventArgs e)
        {

           
            int userid = int.Parse(textBox1.Text);
             SqlConnection con = new SqlConnection(@"Data Source=PC15\SQLEXPRESS;Initial Catalog=employej;Integrated Security=True;");
             con.Open();
             SqlCommand cnn = new SqlCommand("DELETE FROM emptab WHERE userid = @userid", con);
             cnn.Parameters.AddWithValue("@userid", userid);
             cnn.ExecuteNonQuery();
             con.Close();
             MessageBox.Show("Are You Sure Data ID " + userid.ToString() + " deleted.");
        }



        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                picshow.Image = new Bitmap(fileDialog.FileName);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || 
                textBox5.Text == "" || textBox6.Text == "" || comboBox1.Items == null || picshow.Image == null || textBox7.Text == "")
            
            {
                MessageBox.Show("Please fill in all fields before updating.");
            }
            else
            {
                
                SqlConnection con = new SqlConnection(@"Data Source=PC15\SQLEXPRESS;Initial Catalog=employej;Integrated Security=True;");
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE emptab SET fname=@fname, lname=@lname, age=@age, phone=@phone, gender=@gender, department=@department,position=@position WHERE userid=@userid", con);
                cmd.Parameters.AddWithValue("@userid", int.Parse(textBox1.Text));
                cmd.Parameters.AddWithValue("@fname", textBox2.Text);
                cmd.Parameters.AddWithValue("@lname", textBox3.Text);
                cmd.Parameters.AddWithValue("@age", int.Parse(textBox4.Text));
                cmd.Parameters.AddWithValue("@phone", int.Parse(textBox5.Text));

                string gender;
                if (radioButton1.Checked)
                {
                    gender = "male";
                }
                else
                {
                    gender = "female";
                }

                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@department", textBox6.Text);
                cmd.Parameters.AddWithValue("@position", comboBox1.Text);
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Your file is updated. Welcome!");
                }
                else
                {
                    MessageBox.Show("No records updated. User ID not found.");
                }
            }


        }

        private void btnview_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                //SqlCommand cmd = new SqlCommand("SELECT userid, fname, lname, age, phone, gender, department, photo FROM emptab WHERE userid = @userid", con);
                SqlCommand cmd = new SqlCommand("SELECT * FROM emptab WHERE userid = @userid", con);
                cmd.Parameters.AddWithValue("@userid", int.Parse(txtsearch.Text));

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

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            dashboard dash = new dashboard();
            this.Hide();
            dash.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";

            // Clear radio buttons
            radioButton1.Checked = false;
            radioButton2.Checked = false;

            // Clear image
            picshow.Image = null;

            // Clear search text box
            txtsearch.Text = "";

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            int userid;
            if (int.TryParse(textBox1.Text, out userid))
            {
                
                using (SqlConnection con = new SqlConnection(@"Data Source=PC15\SQLEXPRESS;Initial Catalog=employej;Integrated Security=True;"))
                {
                    con.Open();

                    
                    using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM emptab WHERE userid = @userid", con))
                    {
                        checkCmd.Parameters.AddWithValue("@userid", userid);
                        int userCount = (int)checkCmd.ExecuteScalar();

                        if (userCount > 0)
                        {
                            
                            using (SqlCommand deleteCmd = new SqlCommand("DELETE FROM emptab WHERE userid = @userid", con))
                            {
                                deleteCmd.Parameters.AddWithValue("@userid", userid);
                                deleteCmd.ExecuteNonQuery();
                                MessageBox.Show("Data with ID " + userid.ToString() + " has been deleted.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Data with ID " + userid.ToString() + " does not exist.");
                        }
                    }
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid user ID.");
            }

        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT userid, fname, lname, age, phone, gender, department, photo FROM emptab WHERE userid = @userid", con);
                cmd.Parameters.AddWithValue("@userid", int.Parse(txtsearch.Text));

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

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" ||
                textBox5.Text == "" || textBox6.Text == "" || comboBox1.Items == null || picshow.Image == null || textBox7.Text == "")
            {
                MessageBox.Show("Please fill in all fields before updating.");
            }
            else
            {
                
                SqlConnection con = new SqlConnection(@"Data Source=PC15\SQLEXPRESS;Initial Catalog=employej;Integrated Security=True;");
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE emptab SET fname=@fname, lname=@lname, age=@age, phone=@phone, gender=@gender, department=@department WHERE userid=@userid", con);
                cmd.Parameters.AddWithValue("@userid", int.Parse(textBox1.Text));
                cmd.Parameters.AddWithValue("@fname", textBox2.Text);
                cmd.Parameters.AddWithValue("@lname", textBox3.Text);
                cmd.Parameters.AddWithValue("@age", int.Parse(textBox4.Text));
                cmd.Parameters.AddWithValue("@phone", int.Parse(textBox5.Text));

                string gender;
                if (radioButton1.Checked)
                {
                    gender = "male";
                }
                else
                {
                    gender = "female";
                }

                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@department", textBox6.Text);
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Your file is updated. Welcome!");
                }
                else
                {
                    MessageBox.Show("No records updated. User ID not found.");
                }
            }
        }

        private void searchToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            try
            {
                con.Open();

                if (int.TryParse(txtsearch.Text, out int userId))
                {
                    SqlCommand cmd = new SqlCommand("SELECT *FROM emptab WHERE userid = @userid", con);
                    cmd.Parameters.AddWithValue("@userid", userId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        textBox1.Text = reader["userid"].ToString();
                        textBox2.Text = reader["fname"].ToString();
                        textBox3.Text = reader["lname"].ToString();
                        textBox4.Text = reader["age"].ToString();
                        textBox5.Text = reader["phone"].ToString();
                        textBox6.Text = reader["department"].ToString();
                        textBox7.Text = reader["salary"].ToString();
                        comboBox1.Text = reader["position"].ToString();

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

                        reader.Close();
                    }
                    else
                    {
                        MessageBox.Show("User ID not found");
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid user ID");
                }
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int userid;
            if (int.TryParse(textBox1.Text, out userid))
            {
               
                using (SqlConnection con = new SqlConnection(@"Data Source=PC15\SQLEXPRESS;Initial Catalog=employej;Integrated Security=True;"))
                {
                    con.Open();

                    
                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM emptab WHERE userid = @userid", con);
                    checkCmd.Parameters.AddWithValue("@userid", userid);
                    int userCount = (int)checkCmd.ExecuteScalar();

                    if (userCount > 0)
                    {
                      
                        SqlCommand deleteCmd = new SqlCommand("DELETE FROM emptab WHERE userid = @userid", con);
                        deleteCmd.Parameters.AddWithValue("@userid", userid);
                        deleteCmd.ExecuteNonQuery();
                        MessageBox.Show("Data with ID " + userid.ToString() + " has been deleted.");
                    }
                    else
                    {
                        MessageBox.Show("Data with ID " + userid.ToString() + " does not exist.");
                    }

                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid user ID.");
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal; 
            }
            else
            {
                this.WindowState = FormWindowState.Maximized; 
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void aboutEmployeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About ab = new About();
            ab.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
           // textBox5.Text = "";
           // textBox6.Text = "";
            textBox7.Clear(); 
            radioButton1.Text = "";
            radioButton2.Text = "";
            picshow.Image = null;
            comboBox1.SelectedIndex = -1; 
            txtsearch.Text = "";


        }
    }
}
