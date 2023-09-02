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

namespace StudentEnrollmentSystemNew
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=SHARIKASINI;Initial Catalog=Logintable;Integrated Security=True");

        private void Login_Click(object sender, EventArgs e)
        {
            string Username = textBoxUN.Text;
            string Password = textBoxPS.Text;

            SqlDataAdapter sda = new SqlDataAdapter("select count (*)from [AdminTable] where UserName = '" + Username + "' and Password = '" + Password + "' ", Con);
            DataTable DT = new DataTable();
            sda.Fill(DT);

            if (DT.Rows[0][0].ToString() == "1")
            {
                MessageBox.Show("Authentication Success!", "Valid Username and Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                HomePage F2 = new HomePage();
                F2.Show();
            }

            else
            {
                MessageBox.Show("Credentials Provided are incorrect", "invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBoxUN.Clear();
            textBoxPS.Clear();
            textBoxUN.Focus();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }

            else
            {

            }
        }
    }
}
