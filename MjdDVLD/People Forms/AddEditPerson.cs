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
using General;

namespace MjdDVLD.People_Forms
{
    public partial class AddEditPerson : Form
    {
        public AddEditPerson(int PersonIDToEdit = -1, bool OnlyShow = false)
        {
            InitializeComponent();

            DataTable AllCountries = DVLDApp.MangeCountries.ListAllCountries();

            _CountriesIDes = new List<int>();

            foreach (DataRow r in AllCountries.Rows)
            {
                _CountriesIDes.Add(Convert.ToInt32(r.ItemArray[0]));
                cbCountries.Items.Add(r.ItemArray[1].ToString());
            }

            if (AllCountries.Rows.Count > 0)
                cbCountries.SelectedIndex = 0;

            btnResetImage.Visible = false;


            pbPhoto.ImageLocation = SettingsClass.MaleErrorImagePath;

            if (PersonIDToEdit != -1)
            {
                Person PersonToEdit = DVLDApp.MangePeople.Find(PersonIDToEdit);

                if (PersonToEdit != null)
                {
                    this.Text = "Edit Person";
                    _LoadPersonInfo(PersonToEdit);

                    if (OnlyShow)
                    {
                        mtbFirstName.Enabled = false;
                        mtbScoundName.Enabled = false;
                        mtbThiredName.Enabled = false;
                        mtbLastName.Enabled = false;
                        rbtnMale.Enabled = false;
                        rbtnFemale.Enabled = false;
                        mtbNationalNumber.Enabled = false;
                        cbCountries.Enabled = false;
                        mtbPhoneNumber.Enabled = false;
                        mtbAddress.Enabled = false;
                        mtbEmail.Enabled = false;
                        mtbDateOfBirth.Enabled = false;
                        btnSave.Visible = false;
                        btnCancel.Text = "Ok";
                    }
                    else
                    {
                        btnResetImage.Visible = true;
                    }

                }
                else
                {
                    MessageBox.Show("Sory, this License ID does not exist>", "Error");
                    this.Close();
                }
            }
        }
        
        private List<int> _CountriesIDes;

        private bool _SetError(MaskedTextBox Box)
        {
            if (string.IsNullOrEmpty(Box.Text)) 
            ErrorProvider.SetError(Box,"This filed can not be empty.");

            return string.IsNullOrEmpty(Box.Text);
        }

        private Person _ToPerson()
        {
            Person PersonToEdit;
            if (lblPersonID.Text == "??")
            {
                return new Person(mtbNationalNumber.Text, mtbFirstName.Text, mtbScoundName.Text,
                    string.IsNullOrEmpty(mtbThiredName.Text) ? null : mtbThiredName.Text, mtbLastName.Text, Convert.ToDateTime(mtbDateOfBirth),
                    rbtnMale.Checked ? Gendor.Mail : Gendor.Femail, mtbAddress.Text, mtbPhoneNumber.Text, mtbEmail.Text,
                    _CountriesIDes[cbCountries.SelectedIndex], pbPhoto.ImageLocation); 
            }

            PersonToEdit = DVLDApp.MangePeople.Find(Convert.ToInt32(lblPersonID.Text));

            PersonToEdit.NationalNo = mtbNationalNumber.Text;
            PersonToEdit.FirstName = mtbFirstName.Text;
            PersonToEdit.SecondName = mtbScoundName.Text;
            PersonToEdit.ThirdName = string.IsNullOrEmpty(mtbThiredName.Text) ? null : mtbThiredName.Text;
            PersonToEdit.LastName = mtbLastName.Text;
            PersonToEdit.DateOfBirth = Convert.ToDateTime(mtbDateOfBirth.Text);
            PersonToEdit.Gendor = rbtnMale.Checked ? Gendor.Mail : Gendor.Femail;
            PersonToEdit.Address = mtbAddress.Text;
            PersonToEdit.Phone = mtbPhoneNumber.Text;
            PersonToEdit.Email = mtbEmail.Text;
            PersonToEdit.NationalityCountryID = _CountriesIDes[cbCountries.SelectedIndex];
            PersonToEdit.ImagePath = pbPhoto.ImageLocation;

            return PersonToEdit;
        }

        private void _LoadPersonInfo(Person person)
        {
            lblPersonID.Text = person.PersonID.ToString();
            mtbFirstName.Text = person.FirstName;
            mtbScoundName.Text = person.SecondName;
            mtbThiredName.Text = person.ThirdName;
            mtbLastName.Text = person.LastName;

            if (person.Gendor == Gendor.Mail)
                rbtnMale.Checked = true;
            else
                rbtnFemale.Checked = true;
            
            mtbNationalNumber.Text = person.NationalNo;
            pbPhoto.ImageLocation = person.ImagePath;

            mtbPhoneNumber.Text = person.Phone;
            mtbAddress.Text = person.Address;
            mtbEmail.Text = person.Email;
            mtbDateOfBirth.Text = person.DateOfBirth.ToShortDateString();

            cbCountries.SelectedIndex = _CountriesIDes.Find(item => item == person.NationalityCountryID)-1;

        }

        private void pbPhoto_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty((string)pbPhoto.ImageLocation) || (string)pbPhoto.ImageLocation == SettingsClass.MaleErrorImagePath || (string)pbPhoto.ImageLocation == SettingsClass.FemaleErrorImagePath)
            {
                openFileDialog.InitialDirectory = @"D\";
                openFileDialog.ShowDialog();
                pbPhoto.ImageLocation = openFileDialog.FileName;

                btnResetImage.Visible = true;
            }
        }

        private void rbtnFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty((string)pbPhoto.ImageLocation)|| (string)pbPhoto.ImageLocation == SettingsClass.MaleErrorImagePath || (string)pbPhoto.ImageLocation == SettingsClass.FemaleErrorImagePath)
            {
                if (rbtnMale.Checked)
                    pbPhoto.ImageLocation = SettingsClass.MaleErrorImagePath;
                else
                    pbPhoto.ImageLocation = SettingsClass.FemaleErrorImagePath;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool successed = false;

            ErrorProvider.Clear();

            _SetError(mtbFirstName);
            _SetError(mtbScoundName);
            _SetError(mtbLastName);
            _SetError(mtbNationalNumber);
            _SetError(mtbEmail);
            _SetError(mtbAddress);


            if (mtbDateOfBirth.Text =="   /   /")
            { ErrorProvider.SetError(mtbDateOfBirth, "This filed can not be empty."); }

            if (_SetError(mtbFirstName) || _SetError(mtbScoundName) || _SetError(mtbLastName) || _SetError(mtbNationalNumber) || _SetError(mtbEmail) || _SetError(mtbAddress) || mtbDateOfBirth.Text == "   /   /")
                return;

            if (lblPersonID.Text == "??")
            {
                successed = DVLDApp.MangePeople.Add(_ToPerson());
            }

            else if (DVLDApp.MangePeople.IsExist(Convert.ToInt32(lblPersonID.Text)))
            {
                successed = DVLDApp.MangePeople.Update(_ToPerson());
            }

            else if (MessageBox.Show("Error.", "This Person ID Does Not Exits, Do you want to add the Person as new one.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                lblPersonID.Text = "??"; // so _ToPerson() could jet in (add new mode) not (edit mode) //  
                successed = DVLDApp.MangePeople.Add(_ToPerson());
            }

            if (successed)
            {
                MessageBox.Show("Added successfully.", "success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
                MessageBox.Show("Adding Failed.", "Failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return;
        }

        private void btnResetImage_Click(object sender, EventArgs e)
        {
            pbPhoto.ImageLocation = null;
            rbtnFemale_CheckedChanged(this,EventArgs.Empty);
            btnResetImage.Visible = false;
        }
    
    }

}
