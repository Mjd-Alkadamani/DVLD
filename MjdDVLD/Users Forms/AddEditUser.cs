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

namespace MjdDVLD.Users_Forms
{
    public partial class AddEditUser : Form
    {
        public AddEditUser(int UserIDToEdit = -1, bool OnlyShow = false)
        {
            InitializeComponent();

            if (UserIDToEdit != -1)
            {
                User UserToEdit = DVLDApp.MangeUsers.Find(UserIDToEdit);

                if (UserToEdit != null)
                {
                    _LoadUserInfo(UserToEdit);
                    this.Text = "Edite User";

                    if (OnlyShow)
                    {

                        btnPersonID.Enabled = false;
                        txtbUserName.Enabled = false;
                        txtbPassword.Enabled = false;
                        txtbRenterPassword.Enabled = false;
                        cbIsActive.Enabled = false;

                        btnCancel.Text = "Ok";
                        btnSave.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("Sory, this User ID does not exist.", "Error");
                    this.Close();
                }
            }

        }

        private void _LoadUserInfo(User UserToLoad)
        {
            lblUserID.Text = UserToLoad.UserID.ToString();
            btnPersonID.Text = UserToLoad.PersonID.ToString();
            _LoadCurrantPersonImage();

            txtbUserName.Text = UserToLoad.UserName;
            txtbPassword.Text = UserToLoad.Password;
            txtbRenterPassword.Text = UserToLoad.Password;
            cbIsActive.Checked = UserToLoad.IsActive;

        }

        private void _LoadCurrantPersonImage()
        {
            if (btnPersonID.Text != "??")
                pbPhoto.ImageLocation = DVLDApp.MangePeople.GetPersonImagePath(Convert.ToInt32(btnPersonID.Text));
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
            pbPhoto.ImageLocation = DVLDApp.MangePeople.GetPersonImagePath(ID);
        }

        private void txtbRenterPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtbPassword.Text != txtbRenterPassword.Text)
                ErrorProvider.SetError(txtbRenterPassword, "The First and secound Passwords is not the same.");
            else
                ErrorProvider.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private User _LoadUser()
        {
            if(lblUserID.Text == "??")
            {
                return new User(Convert.ToInt32(btnPersonID.Text), txtbUserName.Text, txtbPassword.Text, cbIsActive.Checked);
            }

            User LoadedUser = DVLDApp.MangeUsers.Find(Convert.ToInt32(lblUserID.Text));

            LoadedUser.UserName = txtbUserName.Text;
            LoadedUser.Password = txtbPassword.Text;
            LoadedUser.IsActive = cbIsActive.Checked;

            return LoadedUser;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool successed = false;

            ErrorProvider.Clear();

            if (txtbPassword.Text != txtbRenterPassword.Text)
            {
                ErrorProvider.SetError(txtbRenterPassword, "The First and secound Passwords is not the same.");
                return;
            }

            if (btnPersonID.Text == "??")
            {
                ErrorProvider.SetError(btnPersonID, "You have to Choose a Person.");
                return;
            }

            if (lblUserID.Text == "??")
            {
                if (DVLDApp.MangeUsers.IsExistByPersonID(Convert.ToInt32(btnPersonID.Text)))
                { MessageBox.Show("This Person already have a User Account.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                else
                successed = DVLDApp.MangeUsers.Add(_LoadUser());
            }
            else if (DVLDApp.MangeUsers.IsExist(Convert.ToInt32(lblUserID.Text)))
            {
                successed = DVLDApp.MangeUsers.Update(_LoadUser());
            }

            else if (MessageBox.Show("Error.", "This User ID Does Not Exits, Do you want to add the Person as new one.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                lblUserID.Text = "??"; // so _ToPerson() could jet in (add new mode) not (edit mode) //  
                successed = DVLDApp.MangeUsers.Add(_LoadUser());
            }



            if (successed)
            {
                MessageBox.Show("Finshed successfully.", "success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
                MessageBox.Show("Operation Failed.", "Failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

    }
}
