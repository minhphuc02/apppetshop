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
using System.Windows.Input;

namespace PetMSTuto
{
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
            DisPlayProduct();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=LAPTOP-R7OB3OFM\MSSQLSERVER01;Initial Catalog=thucung;Persist Security Info=True;User ID=sa;Password=123");
        private void DisPlayProduct()
        {
            Con.Open();
            string Query = "Select * from ProductTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductsDGV.DataSource = ds.Tables[0];
            Con.Close();

        }
        private void Clear()
        {
            PrNameTb.Text = "";
            QtyTb.Text = "";
            PriceTb.Text = "";
            CatCb.SelectedIndex = 0;
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (PrNameTb.Text == "" || CatCb.Text == "" || QtyTb.Text == "" || PriceTb.Text == "")
            {
                MessageBox.Show("Chưa điền thông tin", "Thông Báo!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ProductTbl (PrName, PrCat, PrQty, PrPrice) values(@PN, @PC, @PQ, @PP)", Con);
                    cmd.Parameters.AddWithValue("@PN", PrNameTb.Text);
                    cmd.Parameters.AddWithValue("@PC", CatCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PQ", QtyTb.Text);
                    cmd.Parameters.AddWithValue("@PP", PriceTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm thành công!!", "Thông Báo!!");
                    Con.Close();
                    DisPlayProduct();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int Key = 0;
        private void ProductsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PrNameTb.Text = ProductsDGV.SelectedRows[0].Cells[1].Value.ToString();
            CatCb.Text = ProductsDGV.SelectedRows[0].Cells[2].Value.ToString();
            QtyTb.Text = ProductsDGV.SelectedRows[0].Cells[3].Value.ToString();
            PriceTb.Text = ProductsDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (PrNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ProductsDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Chưa chọn sản pẩm", "Thông Báo!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from ProductTbl where PrId = @PKey ", Con);
                    cmd.Parameters.AddWithValue("@PKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Xóa Thành Công!!", "Thông Báo!!");
                    Con.Close();
                    DisPlayProduct();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (PrNameTb.Text == "" || CatCb.Text == "" || QtyTb.Text == "" || PriceTb.Text == "")
            {
                MessageBox.Show("Chưa điền thông tin", "Thông Báo!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update ProductTbl set PrName = @PN, PrCat = @PC, PrQty = @PQ, PrPrice = @PP where PrId = @PKey", Con);
                    cmd.Parameters.AddWithValue("@PN", PrNameTb.Text);
                    cmd.Parameters.AddWithValue("@PC", CatCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PQ", QtyTb.Text);
                    cmd.Parameters.AddWithValue("@PP", PriceTb.Text);
                    cmd.Parameters.AddWithValue("@PKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm thành công!!", "Thông Báo!!");
                    Con.Close();
                    DisPlayProduct();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Home Obj = new Home();
            Obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Customers Obj = new Customers();
            Obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Employees Obj = new Employees();
            Obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Billing Obj = new Billing();
            Obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void nhanvienlbl_Click(object sender, EventArgs e)
        {
            nhanvienlbl.Text = Login.UserName;
        }

        private void Products_Load(object sender, EventArgs e)
        {
            nhanvienlbl.Text = Login.UserName;
        }

        private void QtyTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
