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

    public partial class Form1 : Form
    {
        string con = @"Data Source= (local); Initial Catalog= ProjectB; Integrated Security= True;";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
              SqlConnection sqlCon = new SqlConnection(con);
              sqlCon.Open();
           
            SqlCommand com = new SqlCommand("INSERT INTO Student (FirstName, LastName, Contact,Email, RegistrationNumber,Status) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" +textBox7.Text+"')", sqlCon);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet d = new DataSet();
            da.Fill(d);
            MessageBox.Show("inserted");
           
       
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
   
        }
    }
}
