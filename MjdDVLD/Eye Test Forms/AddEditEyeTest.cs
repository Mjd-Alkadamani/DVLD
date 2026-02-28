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

namespace MjdDVLD.Eye_Test_Forms
{
    public partial class AddEditEyeTest : Form
    {
        public AddEditEyeTest(int CreatedAcordingToApplication, int EyeTestIDToEdit = -1, bool ShowOnly = false)
        {
            InitializeComponent();

            //if(DVLDApp.MangeApplications)

            if (EyeTestIDToEdit == -1)
            {
                lblCreatedByUserID.Text = DVLDApp.LogedInUser.UserID.ToString();
                mtbAppointmentDate.Text = (DateTime.Now + new TimeSpan(24, 0, 0)).ToString();
            }


        }

        private void _LoadEyeTestInfo(EyeTest Test)
        {

        }

        private void btnPersonID_Click(object sender, EventArgs e)
        {
            ListAllForm ApplicationsList = new ListAllForm
             (DVLDApp.MangePeople.ListAllPeople(), "Applications", new ListAllForm.Permissions(), true);

            ApplicationsList.OnOkButtonClick += _ChangePersonID;

            ApplicationsList.ShowDialog();
        }

        private void _ChangePersonID(object sender, int ID)
        {
            btnPersonID.Text = ID.ToString();
        }

        private void txtbPaidFees_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbPaidFees.Text))
            {
                txtbPaidFees.Text = "";
                return;
            }


            string AllowedChars = "0123456789.";

            // join if in 0-9 or the dote (.)
            txtbPaidFees.Text = String.Join("", txtbPaidFees.Text.Where(c => AllowedChars.Contains(c)));

            if (txtbPaidFees.Text.Count(n => n == '.') > 1)
            {
                txtbPaidFees.Text = txtbPaidFees.Text.Remove(txtbPaidFees.Text.LastIndexOf('.'), 1);
            }
        }

        private void txtbNotes_TextChanged(object sender, EventArgs e)
        {

            if (txtbNotes.Text.Length >= 500)
            {
                txtbNotes.Text = txtbNotes.Text.Substring(0, 500);
            }
            
        }
    
    
    
    
    }
}
