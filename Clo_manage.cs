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
    public partial class Clo_manage : Form
    {
        string con = @"Data Source= (local); Initial Catalog= ProjectB; Integrated Security= True;";
        public Clo_manage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(con);
            sqlCon.Open();

            SqlCommand com = new SqlCommand("INSERT INTO Clo (Name, DateCreated, DateUpdated) VALUES ('" + textBox1.Text + "','" + dateTimePicker1.Value.ToShortTimeString() + "','" + dateTimePicker2.Value.ToShortTimeString() + "')", sqlCon);
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
                SqlDataAdapter d = new SqlDataAdapter("SELECT * From dbo.Clo", sqlCon);
                DataTable d1 = new DataTable();
                d.Fill(d1);
                dataGridView1.DataSource = d1;
                sqlCon.Close();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(con);
            sqlCon.Open();

            string query = "Delete From Clo WHERE Id= '"+ dataGridView1.CurrentCell.Value+ "' ";
            SqlCommand com = new SqlCommand(query, sqlCon);
            SqlDataReader r;

            //  com.Connection.Open();
            try
            {
                r = com.ExecuteReader();
                MessageBox.Show("deleted");
                while (r.Read())
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
            DataGridViewCellCancelEventArgs c = new DataGridViewCellCancelEventArgs(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentRow.Index);
            DataGridViewRow newDataRow = this.dataGridView1.Rows[dataGridView1.CurrentRow.Index];
            newDataRow.Cells[1].Value = textBox1.Text;
            newDataRow.Cells[2].Value = dateTimePicker1.Value.ToShortTimeString();
            newDataRow.Cells[3].Value = dateTimePicker2.Value.ToShortTimeString();


            SqlConnection sqlCon = new SqlConnection(con);
            sqlCon.Open();

            string query = "Update Clo SET Name = @FirstName , DateCreated= @LastName , DateUpdated= @Contact  WHERE Id= '" + dataGridView1.CurrentCell.Value + "'  ";
            SqlCommand com = new SqlCommand(query, sqlCon);
            com.Parameters.AddWithValue("@FirstName", textBox1.Text);
            com.Parameters.AddWithValue("@LastName", dateTimePicker1.Value.ToShortTimeString());
            com.Parameters.AddWithValue("@Contact", dateTimePicker2.Value.ToShortTimeString());

            //  com.Connection.Open();
            try
            {
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex.Message);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //gets a collection that contains all the rows
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                //populate the textbox from specific value of the coordinates of column and row.
                textBox1.Text = row.Cells[1].Value.ToString();
                dateTimePicker1.Text = row.Cells[2].Value.ToString();
                dateTimePicker1.Text = row.Cells[2].Value.ToString();
            }
        }
    }
}
