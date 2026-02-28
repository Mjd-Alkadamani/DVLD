using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessTier;

namespace MjdDVLD
{
    public partial class LogInScreen : Form
    {
        public LogInScreen()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            epLoginFailed1.Clear();
            epLoginFailed2.Clear();

            if (string.IsNullOrEmpty(mtbUserNameOrID.Text)|| string.IsNullOrEmpty(mtbPassword.Text))
            {
                if(string.IsNullOrEmpty(mtbUserNameOrID.Text))
                epLoginFailed1.SetError(mtbUserNameOrID, "This field is required");
                if(string.IsNullOrEmpty(mtbPassword.Text))
                epLoginFailed2.SetError(mtbPassword, "This field is required");
            }
            else if (DVLDApp.LogInWith(mtbUserNameOrID.Text, mtbPassword.Text))
            {
                this.Visible = false;

                MainScreen MainMenue = new MainScreen();

                MainMenue.ShowDialog();

                //mtbPassword.Text = "";

                DVLDApp.LogOut();

                this.Visible = true;

            }
            else
            {
                epLoginFailed1.SetError(mtbPassword, "Wrong User Name Or Password");
                epLoginFailed2.SetError(mtbUserNameOrID, "Wrong User Name Or Password");

                Timer.Enabled = true;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            epLoginFailed1.Clear();
            epLoginFailed2.Clear();
            Timer.Enabled = false;
        }
    }
}
