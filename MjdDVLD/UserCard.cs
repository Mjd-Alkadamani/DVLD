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
    public partial class UserCard : Form
    {
        private readonly Person _person;
       
        public UserCard(int PersonID)
        {
            InitializeComponent();
            _person = DVLDApp.MangePeople.Find(PersonID);
        }

        public UserCard(Person person)
        {
            InitializeComponent();
            _person = person;
        }

        private void UserCard_Load(object sender, EventArgs e)
        {
            ppcMain.LoadInfo(_person);
        }
    }
}
