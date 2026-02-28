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
    public partial class MainScreen : Form
    {
        public MainScreen()
        {
            InitializeComponent();
        }



        //   //   //   //   //   //   //   //   //   //   //   //   //   //   //   //   //   //   //   // 
        /* I was working on Add Edit Eye Test To Add it to Add Eidt Application sub Local application  */
        //   //   //   //   //   //   //   //   //   //   //   //   //   //   //   //   //   //   //   // 



        ///////////// Minu strip /////////////

        ///////////// Licenses /////////////

        private void allLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable table = DVLDApp.MangeLicenses.ListAllLicenses();

            if (table.Rows.Count == 0)
            {
                MessageBox.Show("There is no Licenses found.", "Nan.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ListAllForm Licenses = new ListAllForm(table, "Licenses", new ListAllForm.Permissions(
                ListAllForm.Permissions.Permission.Add,ListAllForm.Permissions.Permission.Edit,ListAllForm.Permissions.Permission.ShowCard,ListAllForm.Permissions.Permission.Refresh), false);

            Licenses.OnAddButtonClick += AddLicense;
            Licenses.OnEditButtonClick += EditLicense;
            Licenses.OnShowCardButtonClick += ShowLicenseCard;
            Licenses.OnRefrashButtonClick += RefrashLicenses;
            Licenses.OnDeletelButtonClick += DeleteLicenses;

            Licenses.ShowDialog();
        }

        private void AddLicense(object sender)
        {
            Licenses_Forms.AddEditLicenses AddScreen = new Licenses_Forms.AddEditLicenses();

            AddScreen.ShowDialog();
        }

        private void EditLicense(object sender, int ID)
        {
            Licenses_Forms.AddEditLicenses AddScreen = new Licenses_Forms.AddEditLicenses(ID);

            AddScreen.ShowDialog();

        }

        private void ShowLicenseCard(object sender, int ID)
        {
            Licenses_Forms.AddEditLicenses ShowCardScreen = new Licenses_Forms.AddEditLicenses(ID,true);

            ShowCardScreen.ShowDialog();
        }

        private void RefrashLicenses(object sender, ref DataTable Table)
        {
            Table = DVLDApp.MangeLicenses.ListAllLicenses();
        }
        
        private bool DeleteLicenses(object sender, int ID)
        {
            return DVLDApp.MangeLicenses.DeleteLicense(ID);
        }


        ///////////// Ditened Licenses /////////////

        private void ditenedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable table = DVLDApp.MangeDetainedLicenses.ListAllDetainedLicenses();

            if (table.Rows.Count == 0)
            {
                MessageBox.Show("There is no Ditened Licenses Curnutly.", "Nan.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
                table.Select($"ReleaseDate = \'{DBNull.Value}\' Or ReleasedByUserID = \'{DBNull.Value}\' or ReleaseApplicationID = \'{DBNull.Value}\'");
            // ListAllForm DitenedLicenses = new ListAllForm(table, "Curnutly Ditened Licenses");
            // DitenedLicenses.ShowDialog();
            
   

        }

        private void diteningRecordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable table = DVLDApp.MangeDetainedLicenses.ListAllDetainedLicenses();

            if (table.Rows.Count == 0)
            {
                MessageBox.Show("There is no Ditened Licenses Curnutly.", "Nan.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // ListAllForm DitenedLicenses = new ListAllForm(table, "Curnutly Ditened Licenses");
            // DitenedLicenses.ShowDialog();



        }

    ///////////// Aplications /////////////

        private void showAllApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable table = DVLDApp.MangeApplications.ListAllApplications();

            if (table.Rows.Count == 0)
            {
                MessageBox.Show("There is no Applications found.", "Nan.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ListAllForm Applications = new ListAllForm(table, "Applicatins", new ListAllForm.Permissions(
                    ListAllForm.Permissions.Permission.Add, ListAllForm.Permissions.Permission.Edit, ListAllForm.Permissions.Permission.ShowCard, ListAllForm.Permissions.Permission.Refresh), false);

            Applications.OnAddButtonClick += AddApplication;
            Applications.OnEditButtonClick += EditApplication;
            Applications.OnShowCardButtonClick += ShowApplicationCard;
            Applications.OnRefrashButtonClick += RefrashApplication;


            Applications.ShowDialog();
        }

        private void AddApplication(object sender)
        {
            Application_Forms.AddEidtApplication AddScreen = new Application_Forms.AddEidtApplication();

            AddScreen.ShowDialog();
        }

        private void EditApplication(object sender, int ID)
        {
            Application_Forms.AddEidtApplication AddScreen = new Application_Forms.AddEidtApplication(ID);

            AddScreen.ShowDialog();
        }

        private void ShowApplicationCard(object sender, int ID)
        {
            Application_Forms.AddEidtApplication ShowCardScreen = new Application_Forms.AddEidtApplication(ID, true);

            ShowCardScreen.ShowDialog();
        }

        private void RefrashApplication(object sender, ref DataTable Table)
        {
            Table = DVLDApp.MangeApplications.ListAllApplications();
        }

        private bool DeleteApplication(object sender, int ID)
        {
            // You can not Delete an application
            return false;
        }


        ///////////// Users /////////////

        private void allUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DataTable table = DVLDApp.MangeUsers.ListAllUsers();

            if (table.Rows.Count == 0)
            {
                MessageBox.Show("There is no Users found.", "Nan.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            ListAllForm DriversList = new ListAllForm(table, "Users", new ListAllForm.Permissions(
                    ListAllForm.Permissions.Permission.Add, ListAllForm.Permissions.Permission.Edit, ListAllForm.Permissions.Permission.Delete, ListAllForm.Permissions.Permission.ShowCard, ListAllForm.Permissions.Permission.Refresh), false);

            DriversList.OnAddButtonClick += AddUser;
            DriversList.OnEditButtonClick += EditUser;
            DriversList.OnShowCardButtonClick += ShowUserCard;
            DriversList.OnRefrashButtonClick += RefrashUser;
            DriversList.OnDeletelButtonClick += DeleteUser;


            DriversList.ShowDialog();

        }

        private void AddUser(object sender)
        {
            Users_Forms.AddEditUser AddScreen = new Users_Forms.AddEditUser();

            AddScreen.ShowDialog();
        }

        private void EditUser(object sender, int ID)
        {
            Users_Forms.AddEditUser AddScreen = new Users_Forms.AddEditUser(ID);

            AddScreen.ShowDialog();
        }

        private void ShowUserCard(object sender, int ID)
        {
            Users_Forms.AddEditUser ShowCardScreen = new Users_Forms.AddEditUser(ID, true);

            ShowCardScreen.ShowDialog();
        }

        private void RefrashUser(object sender, ref DataTable Table)
        {
            Table = DVLDApp.MangeUsers.ListAllUsers();
        }

        private bool DeleteUser(object sender, int ID)
        {
            return DVLDApp.MangeUsers.DeleteUser(ID);
        }


        ///////////// Drivers /////////////

        private void allDriversToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DataTable table = DVLDApp.MangeDrivers.ListAllDrivers();

            if (table.Rows.Count == 0)
            {
                MessageBox.Show("There is no Drivers found.", "Nan.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ListAllForm DriversList = new ListAllForm(table, "Drivers", new ListAllForm.Permissions(
                ListAllForm.Permissions.Permission.Add,ListAllForm.Permissions.Permission.Delete, ListAllForm.Permissions.Permission.ShowCard, ListAllForm.Permissions.Permission.Refresh), false);

            DriversList.OnAddButtonClick += AddDriver;
            DriversList.OnEditButtonClick += EditDriver;
            DriversList.OnShowCardButtonClick += ShowDriverCard;
            DriversList.OnRefrashButtonClick += RefrashDrivers;
            DriversList.OnDeletelButtonClick += DeleteDriver;

            DriversList.ShowDialog();

        }

        private void AddDriver(object sender)
        {
            Driver_Forms.AddEidtDriver AddScreen = new Driver_Forms.AddEidtDriver();

            AddScreen.ShowDialog();
        }

        private void EditDriver(object sender, int ID)
        {
            Driver_Forms.AddEidtDriver AddScreen = new Driver_Forms.AddEidtDriver(ID);

            AddScreen.ShowDialog();

        }

        private void ShowDriverCard(object sender, int ID)
        {
            Driver_Forms.AddEidtDriver ShowCardScreen = new Driver_Forms.AddEidtDriver(ID, true);

            ShowCardScreen.ShowDialog();
        }

        private void RefrashDrivers(object sender, ref DataTable Table)
        {
            Table = DVLDApp.MangeDrivers.ListAllDrivers();
        }

        private bool DeleteDriver(object sender, int ID)
        {
            return DVLDApp.MangeDrivers.DeleteDriver(ID);
        }

        ///////////// People /////////////

        private void allPeopleListToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DataTable table = DVLDApp.MangePeople.ListAllPeople();

            if (table.Rows.Count == 0)
            {
                MessageBox.Show("There is no People found.", "Nan.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ListAllForm People = new ListAllForm(table, "People", new ListAllForm.Permissions(
                ListAllForm.Permissions.Permission.Add, ListAllForm.Permissions.Permission.Edit, ListAllForm.Permissions.Permission.ShowCard,ListAllForm.Permissions.Permission.Refresh),false);

            People.OnAddButtonClick += AddPerson;
            People.OnEditButtonClick += EditPerson;
            People.OnShowCardButtonClick += ShowPerson;
            People.OnRefrashButtonClick += RefrashPeople;


            People.ShowDialog();
            
        }

        private void AddPerson(object sender)
        {
            MjdDVLD.People_Forms.AddEditPerson AddScreen = new People_Forms.AddEditPerson();
            AddScreen.ShowDialog();
        }
    
        private void EditPerson(object sender, int iD)
        {

            MjdDVLD.People_Forms.AddEditPerson AddScreen = new People_Forms.AddEditPerson(iD);
            AddScreen.ShowDialog();
        }

        private void ShowPerson(object sender, int iD)
        {
            MjdDVLD.People_Forms.AddEditPerson AddScreen = new People_Forms.AddEditPerson(iD,true);
            AddScreen.ShowDialog();
        }

        private void RefrashPeople(object sender, ref DataTable Table)
        {
            Table = DVLDApp.MangePeople.ListAllPeople();
        }

    }

}
