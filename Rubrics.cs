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
    public partial class Rubrics : Form
    {
        private static int visitCounter = 0;
        string con = @"Data Source= (local); Initial Catalog= ProjectB; Integrated Security= True;";
        public Rubrics()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            SqlConnection sqlCon = new SqlConnection(con);
            sqlCon.Open();

            SqlCommand com = new SqlCommand("INSERT INTO Rubric (Id, Details ) VALUES ('"+textBox2.Text+"', '" + richTextBox1.Text + "')", sqlCon);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet d = new DataSet();
            da.Fill(d);
            MessageBox.Show("inserted");

        }

        private void Rubrics_Load(object sender, EventArgs e)
        {
            visitCounter++; // Increase each time a form is loaded
            textBox2.Text = visitCounter.ToString("0");
        }
    }
}
