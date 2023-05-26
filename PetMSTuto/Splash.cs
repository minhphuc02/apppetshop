using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetMSTuto
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
            timer1.Start();
        }
        int Startp = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            Startp += 10;
            Myprogress.Value = Startp;
            PercentageLbl.Text = Startp + "%";
            if(Myprogress.Value ==100)
            {
                Myprogress.Value = 0;
                Login Obj = new Login();
                Obj.Show();
                this.Hide();
                timer1.Stop();
            }    
        }
    }
}
