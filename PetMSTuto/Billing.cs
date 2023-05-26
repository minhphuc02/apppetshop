using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetMSTuto
{
    public partial class Billing : Form
    {
        public Billing()
        {
            InitializeComponent();
            GetCustomers();
            DisplayProduct();
            DisplayTransactions();

        }
        SqlConnection Con = new SqlConnection(@"Data Source=LAPTOP-R7OB3OFM\MSSQLSERVER01;Initial Catalog=thucung;Persist Security Info=True;User ID=sa;Password=123");

        private void GetCustomers()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select CustId from CustomerTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustId", typeof(int));
            dt.Load(Rdr);
            CustIdCb.ValueMember = "CustId";
            CustIdCb.DataSource = dt;
            Con.Close();
        }
        private void DisplayProduct()
        {
            Con.Open();
            string Query = "Select * from ProductTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void DisplayTransactions()
        {
            Con.Open();
            string Query = "Select * from BillTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            TransactionDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void GetCustName()
        {
            Con.Open();
            string Query = "Select * from CustomerTbl wehre CustId ='" + CustIdCb.SelectedValue.ToString();
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable(); 
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                CustNameTb.Text = dr["CustName"].ToString();
            }  
            Con.Close();
        }
       
        private void UpdateStock()
        {
            try
            {
                int NewQty = Stock - Convert.ToInt32(QtyTb.Text);
                Con.Open();
                SqlCommand cmd = new SqlCommand("Update ProductTbl set PrQty = @PQ where PrId=@PKey", Con);
                cmd.Parameters.AddWithValue("@PQ", NewQty);
                cmd.Parameters.AddWithValue("@PKey", Key);
                cmd.ExecuteNonQuery();
                Con.Close();
                DisplayProduct();
            }
            catch(Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void CustIdCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCustName();
        }     
        int Key = 0, Stock = 0;
        private void Reset()
        {
            PrNameTb.Text = "";
            QtyTb.Text = "";
            Stock = 0;
            Key = 0;
        }

        
        private void InsertBill()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into BillTbl (BDate, CustId, CustName, PrName, Amt) values (@BD, @CI, @CN, @PN, @Am)", Con);
                cmd.Parameters.AddWithValue("@BD", DateTime.Today.Date);
                cmd.Parameters.AddWithValue("@CI", CustIdCb.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@CN", CustNameTb.Text);
                cmd.Parameters.AddWithValue("@PN", PrNameTb.Text); 
                cmd.Parameters.AddWithValue("@Am", GrdTotal);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Hóa đơn đã được lưu", "Thông Báo!!");
                Con.Close();
                DisplayTransactions();                
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        string prodname;
        private void PrintBtn_Click(object sender, EventArgs e)
        {
            InsertBill();
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 400, 600);
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
           
        }
        int prodid, prodqty, prodprice, tottal, pos = 60;


        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("PETSHOP", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new Point(135));
            e.Graphics.DrawString(" STT    SẢN PHẨM        SỐ LƯỢNG       GIÁ       TỔNG", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Black, new Point(1, 40));

            foreach (DataGridViewRow row in BillDGV.Rows)
            {
                prodid = Convert.ToInt32(row.Cells[0].Value);
                prodname = "" + row.Cells[1].Value;
                prodprice = Convert.ToInt32(row.Cells[2].Value);
                prodqty = Convert.ToInt32(row.Cells[3].Value);
                tottal = Convert.ToInt32(row.Cells[4].Value);

                e.Graphics.DrawString("" + prodid, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(3, pos));
                e.Graphics.DrawString("" + prodname, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                e.Graphics.DrawString("" + prodprice, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                e.Graphics.DrawString("" + prodqty, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(260, pos));
                e.Graphics.DrawString("" + tottal, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(340, pos));
                pos = pos + 20;
            }
           
            e.Graphics.DrawString("TOTAL BLLL:" + GrdTotal, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Crimson, new Point(135, pos + 50));
            e.Graphics.DrawString("*****PETSHOP****", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Crimson, new Point(125, pos + 85));
            BillDGV.Rows.Clear();
            BillDGV.Refresh();
            pos = 100;
            GrdTotal = 0;
        }

        
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void ProductsDGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            PrNameTb.Text = ProductsDGV.SelectedRows[0].Cells[1].Value.ToString();
            Stock = Convert.ToInt32(ProductsDGV.SelectedRows[0].Cells[3].Value.ToString());
            PrPriceTb.Text = ProductsDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (PrNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ProductsDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
        ///
        /*  private void EmpNameLbl_Click(object sender, EventArgs e)
          {
             // nhanvienlbl.Text = Login.Employee;
             nhanvienlbl.Text = Login.UserName;

          }*/
        ///
        int n = 0, GrdTotal = 0;
 
        private void nhanvienlbl_Click(object sender, EventArgs e)
        {
          
            nhanvienlbl.Text = Login.UserName;
        }

        private void Billing_Load(object sender, EventArgs e)
        {
            nhanvienlbl.Text = Login.UserName;
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (QtyTb.Text == "" || Convert.ToInt32(QtyTb.Text) > Stock)
            {
                MessageBox.Show("Không đủ số lượng", "Thông Báo!!!");
            }
            else
            {
                int total = Convert.ToInt32(QtyTb.Text) * Convert.ToInt32(PrPriceTb.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillDGV);
                newRow.Cells[0].Value = n + 1; 
                newRow.Cells[1].Value = PrNameTb.Text;
                newRow.Cells[2].Value = QtyTb.Text;
                newRow.Cells[3].Value =  PrPriceTb.Text;
                newRow.Cells[4].Value = total;
                
                BillDGV.Rows.Add(newRow);
                n++;
                GrdTotal = GrdTotal + total;
                lbltongbill.Text = "Tổng Bill: " + GrdTotal;

                UpdateStock();
                Reset();
            }
        }     
        private void label1_Click(object sender, EventArgs e)
        {
            Products Obj = new Products();
            Obj.Show();
            this.Hide();
        }
        private void label2_Click(object sender, EventArgs e)
        {
            Home Obj = new Home();
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
        private void label6_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

    }
}
