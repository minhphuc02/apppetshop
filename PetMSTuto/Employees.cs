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
using System.Security.Cryptography;

namespace PetMSTuto
{
    public partial class Employees : Form
    {
        public Employees()
        {
            InitializeComponent();
            DisplayEmployees();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=LAPTOP-R7OB3OFM\MSSQLSERVER01;Initial Catalog=thucung;Persist Security Info=True;User ID=sa;Password=123");
        private void DisplayEmployees()
        {
            Con.Open();
            string Query = "Select * from EmployeeTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            EmployeesDGV.DataSource = ds.Tables[0];
            Con.Close();
           
        }
        private void Clear()
        {
            EmpNameTb.Text = "";
            EmpAddTb.Text = "";
            EmpPhoneTb.Text = "";
            PasswordTb.Text = "";
        }
        private void EmployeesDGV1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            }
            int Key = 0;

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "" || EmpAddTb.Text == "" || EmpPhoneTb.Text == "" || PasswordTb.Text == "") 
              {
                  MessageBox.Show("Chưa điền thông tin", "Thông Báo!!");
              }
              else
              {
                  try
                  {
                      Con.Open();
                      SqlCommand cmd = new SqlCommand("insert into EmployeeTbl (EmpName, EmpAdd, EmpDOB, EmpPhone, EmpPass) values(@EN, @EA, @ED, @EP, @EPa)", Con);
                      cmd.Parameters.AddWithValue("@EN",EmpNameTb.Text);
                      cmd.Parameters.AddWithValue("@EA", EmpAddTb.Text);
                      cmd.Parameters.AddWithValue("@ED", EmpDOB.Value.Date);
                      cmd.Parameters.AddWithValue("@EP", EmpPhoneTb.Text);
                      cmd.Parameters.AddWithValue("@EPa", PasswordTb.Text);
                      cmd.ExecuteNonQuery();
                      MessageBox.Show("Đã thêm nhân viên", "Thông Báo!!");
                      Con.Close();
                      DisplayEmployees();
                      Clear();
                  }
                  catch(Exception Ex)
                  {
                      MessageBox.Show(Ex.Message);
                  }
              }  
           
        }     
        private void EmployeesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EmpNameTb.Text = EmployeesDGV.SelectedRows[0].Cells[1].Value.ToString();
            EmpAddTb.Text = EmployeesDGV.SelectedRows[0].Cells[2].Value.ToString();
            EmpDOB.Text = EmployeesDGV.SelectedRows[0].Cells[3].Value.ToString();
            EmpPhoneTb.Text = EmployeesDGV.SelectedRows[0].Cells[4].Value.ToString();
            PasswordTb.Text = EmployeesDGV.SelectedRows[0].Cells[5].Value.ToString();
            if (EmpNameTb.Text == "")
            {
                Key = 0;
            } else
            {
                Key = Convert.ToInt32(EmployeesDGV.SelectedRows[0].Cells[0].Value.ToString());
            }    
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "" || EmpAddTb.Text == "" || EmpPhoneTb.Text == "" || PasswordTb.Text == "")
            {
                MessageBox.Show("Chưa điền thông tin", "Thông Báo!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update EmployeeTbl set  EmpName =@EN, EmpAdd = @EA, EmpDOB = @ED, EmpPhone = @EP, EmpPass = @EPa Where EmpNum = @EKey", Con);
                    cmd.Parameters.AddWithValue("@EN", EmpNameTb.Text);
                    cmd.Parameters.AddWithValue("@EA", EmpAddTb.Text);
                    cmd.Parameters.AddWithValue("@ED", EmpDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@EP", EmpPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@EPa", PasswordTb.Text);
                    cmd.Parameters.AddWithValue("@EKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thành công", "Thông Báo!!");
                    Con.Close();
                    DisplayEmployees();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Chưa chọn nhân viên", "Thông Báo!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from EmployeeTbl where EmpNum = @EmpKey ", Con);
                    cmd.Parameters.AddWithValue("@EmpKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã xóa nhân viên", "Thông Báo!!");
                    Con.Close();
                    DisplayEmployees();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Customers Obj = new Customers();
            Obj.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Products Obj = new Products();
            Obj.Show();
            this.Hide();
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

        private void nhanvienlbl_Click(object sender, EventArgs e)
        {
            nhanvienlbl.Text = Login.UserName;
        }

        private void Employees_Load(object sender, EventArgs e)
        {
            nhanvienlbl.Text = Login.UserName;
        }
    }
}
