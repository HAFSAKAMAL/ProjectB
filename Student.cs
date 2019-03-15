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



namespace Mini_Project
{

    

    public partial class Student : Form
    {
        private static int visitCounter = 0;
        string con = @"Data Source= (local); Initial Catalog= ProjectB; Integrated Security= True;";
        public Student()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            visitCounter++; // Increase each time a form is loaded
            textBox4.Text = visitCounter.ToString("0");
            

        }

        public static bool IsAllLetters(string s)
        {
            foreach (char c in s)
            {
                if (!Char.IsLetter(c))
                    return false;
            }
            return true;
        }

        public static bool IsAllDigits(string s)
        {
            foreach (char c in s)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }

        public static bool isBool( int a)
        {
            if(a == 1)
            {
                return true;
            }
            else if (a == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            if ( textBox1.Text == "" || !(IsAllLetters(textBox1.Text)))
            {
                MessageBox.Show("invalid name");
            }
            else if(!(IsAllLetters(textBox2.Text)))
            {
                MessageBox.Show("invalid name");
            }
            else if(!(textBox3.Text.Length == 11 )|| !(IsAllDigits(textBox3.Text)))
            {
                MessageBox.Show("Invalid contact number");
            }
            else if(!(textBox5.Text.Contains ('@')) || textBox5.Text == "" )
            {
                MessageBox.Show("Invalid email");
            }

          /*  else if (!(isBool(Convert.ToInt32((textBox7.Text)))))
            {
                MessageBox.Show("Invalid entry");
            }
            */
            else if (textBox6.Text== "")
            {
                MessageBox.Show("invalid registration number");
            }
            else
            {
                SqlConnection sqlCon = new SqlConnection(con);
                sqlCon.Open();

                SqlCommand com = new SqlCommand("INSERT INTO Student (FirstName, LastName, Contact,Email, RegistrationNumber,Status) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "')", sqlCon);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataSet d = new DataSet();
                da.Fill(d);
                MessageBox.Show("inserted");

            }

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
           
           
       
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(con))
            {
                sqlCon.Open();
                // SqlCommand com = new SqlCommand();
                SqlDataAdapter d = new SqlDataAdapter("SELECT * FROM Student", sqlCon);
                DataTable d1 = new DataTable();
                d.Fill(d1);
                dataGridView1.DataSource = d1;
                sqlCon.Close();
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                //gets a collection that contains all the rows
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                //populate the textbox from specific value of the coordinates of column and row.
                textBox4.Text = row.Cells[0].Value.ToString();
                textBox1.Text = row.Cells[1].Value.ToString();
                textBox2.Text = row.Cells[2].Value.ToString();
                textBox3.Text = row.Cells[3].Value.ToString();
                textBox5.Text = row.Cells[4].Value.ToString();
                textBox6.Text = row.Cells[5].Value.ToString();
                textBox7.Text = row.Cells[6].Value.ToString();
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            DataGridViewCellCancelEventArgs c = new DataGridViewCellCancelEventArgs(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentRow.Index );
            DataGridViewRow newDataRow = this.dataGridView1.Rows[dataGridView1.CurrentRow.Index];
            newDataRow.Cells[0].Value = textBox4.Text;
            newDataRow.Cells[1].Value = textBox1.Text;
            newDataRow.Cells[2].Value = textBox2.Text;
            newDataRow.Cells[3].Value = textBox3.Text;
            newDataRow.Cells[4].Value = textBox5.Text;
            newDataRow.Cells[5].Value = textBox6.Text;
            newDataRow.Cells[6].Value = textBox7.Text;

            SqlConnection sqlCon = new SqlConnection(con);
            sqlCon.Open();

            string query = "Update Student SET FirstName = @FirstName , LastName= @LastName , Contact= @Contact ,Email= @Email , RegistrationNumber= @RegistrationNumber,Status = @Status WHERE ID= @ID ";
            SqlCommand com = new SqlCommand(query, sqlCon);
            com.Parameters.AddWithValue("@FirstName", textBox1.Text);
            com.Parameters.AddWithValue("@LastName", textBox2.Text);
            com.Parameters.AddWithValue("@Contact", textBox3.Text);
            com.Parameters.AddWithValue("@Email", textBox5.Text);
            com.Parameters.AddWithValue("@RegistrationNumber", textBox6.Text);
            com.Parameters.AddWithValue("@Status", textBox7.Text);
            com.Parameters.AddWithValue("@ID", textBox4.Text);
          //  com.Connection.Open();
            try
            {
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex.Message);
            }


            /*  SqlCommand com = new SqlCommand();
              SqlDataAdapter da = new SqlDataAdapter(, sqlCon);

              DataSet d = new DataSet();
              da.Fill(d);
              MessageBox.Show("inserted");
              sqlCon.Close();

      */

        }

        private void button3_Click(object sender, EventArgs e)
        {

            SqlConnection sqlCon = new SqlConnection(con);
            sqlCon.Open();

            string query = "Delete From Student WHERE ID= '"+ textBox4.Text+ "'; ";
            SqlCommand com = new SqlCommand(query, sqlCon);
            SqlDataReader r;
       
            //  com.Connection.Open();
            try
            {
                r = com.ExecuteReader();
                MessageBox.Show("deleted");
                while(r.Read())
                {

                }

                
               
                dataGridView1.Rows.RemoveAt(0);
                

            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex.Message);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Student f = new Student();
            Attandance a = new Attandance();
            f.Hide();
            a.Show();
        }
    }
}
