using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetMSTuto
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            CountDogs();
            CountBirds();
            CountCats();
            CountFood();
        }


        SqlConnection Con = new SqlConnection(@"Data Source=LAPTOP-R7OB3OFM\MSSQLSERVER01;Initial Catalog=thucung;Persist Security Info=True;User ID=sa;Password=123");

        private void CountDogs()
        {
            string Cat = "Dog";
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from ProductTbl where PrCat ='" + Cat + " ' ", Con);
            DataTable dt = new DataTable(); 
            sda.Fill(dt);
            DogLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountBirds()
        {
            string Cat = "Birds";
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from ProductTbl where PrCat ='" + Cat + " ' ", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            BirdLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }

        private void CountCats()
        {
            string Cat = "Cat";
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from ProductTbl where PrCat ='" + Cat + " ' ", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CatLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountFood()
        {
            string Cat = "Food";
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter ("Select Count(*) from ProductTbl where PrCat ='" + Cat + " ' ", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            FoodLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void label5_Click(object sender, EventArgs e)
        {
            
            Billing Obj = new Billing();
            Obj.Show();
            this.Hide();
            
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Home Obj = new Home();
            Obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Products Obj = new Products();
            Obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Employees Obj = new Employees();
            Obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Customers Obj = new Customers();
            Obj.Show();
            this.Hide();
        }

        private void nhanvienlbl_Click(object sender, EventArgs e)
        {
            nhanvienlbl.Text = Login.UserName;
        }

        private void Home_Load(object sender, EventArgs e)
        {
            nhanvienlbl.Text = Login.UserName;
        }
    }
}
