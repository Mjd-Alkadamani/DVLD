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

namespace MjdDVLD
{
    public partial class PersonCardControle : UserControl
    {
        public PersonCardControle()
        {
            InitializeComponent();
        }

        private void _LoadInfo(string FirstName, string SecondName, string ThirdName, string LastName, Gendor Gendor, int PersonID,
            int NationalityCountryID, string NationalNo, DateTime DateOfBirth, string Email, string Address, string Phone, string ImagePath)
        {
            if(Gendor == Gendor.Mail)
            { pbPhoto.ErrorImage = new Bitmap(SettingsClass.MaleErrorImagePath) ; }
            else
            { pbPhoto.ErrorImage = new Bitmap(SettingsClass.FemaleErrorImagePath); }

            if (!string.IsNullOrEmpty(ImagePath))
                pbPhoto.ImageLocation = SettingsClass.ImageLocation + ImagePath;

            lblFirstName.Text = FirstName;
            lblScoundName.Text = SecondName;
            lblTiredName.Text = ThirdName;
            lblLastName.Text = LastName;
            lblGendore.Text = Gendor == Gendor.Mail ? "Mail" : "Femail";
            lblPersonID.Text = PersonID.ToString();
            lblCountry.Text = DVLDApp.MangeCountries.Find(NationalityCountryID).CountryName;
            lblNationalNumber.Text = NationalNo;
            lblDateOfBirth.Text = DateOfBirth.ToShortDateString();
            lblEmail.Text = Email;
            lblAddress.Text = Address;
            lblPhoneNumber.Text = Phone;
        }

        public void LoadInfo(Person Person)
        {
            _LoadInfo(Person.FirstName, Person.SecondName, Person.ThirdName, Person.LastName, Person.Gendor, Person.PersonID,
             Person.NationalityCountryID, Person.NationalNo, Person.DateOfBirth, Person.Email, Person.Address, Person.Phone, Person.ImagePath);
        }

        public void LoadInfo(int PersonID)
        {
            Person Person = DVLDApp.MangePeople.Find(PersonID);

            _LoadInfo(Person.FirstName, Person.SecondName, Person.ThirdName, Person.LastName, Person.Gendor, Person.PersonID,
            Person.NationalityCountryID, Person.NationalNo, Person.DateOfBirth, Person.Email, Person.Address, Person.Phone, Person.ImagePath); 

        }


    }
}
