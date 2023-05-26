using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetMSTuto
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=LAPTOP-R7OB3OFM\MSSQLSERVER01;Initial Catalog=thucung;Persist Security Info=True;User ID=sa;Password=123");

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public static String UserName = "";

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from EmployeeTbl where EmpName='" + txttk.Text + "' and EmpPass='" + txtmk.Text + "'", Con);
            DataTable ds = new DataTable();
            sda.Fill(ds);
            if (ds.Rows[0][0].ToString() == "1")
            {
                UserName = txttk.Text;
                Home obj = new Home();
                obj.Show();
                this.Hide();
                Con.Close();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu !!!");
            }

            Con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Admin Obj = new Admin();
            Obj.Show();
            this.Hide();
        }
    }
}
