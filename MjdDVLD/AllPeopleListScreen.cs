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
    public partial class AllPeopleListScreen : Form
    {

        public delegate void OkButtenEventHandler(object sender, Person PersonID);
        public event OkButtenEventHandler OnOkButtonClick;

        public delegate void CancelButtenEventHandler(object sender);
        public event CancelButtenEventHandler OnCancelButtonClick; // Parameters : (object sender)

        public AllPeopleListScreen()
        {
            InitializeComponent();
        }

        public void Add()
        {

        }

        private void listAllPeopleControl1_OnCancelButtonClick(object sender)
        {
            OnCancelButtonClick?.Invoke(this);
            this.Close();
        }

        private void listAllPeopleControl1_OnOkButtonClick(object sender, Person PersonID)
        {
            OnOkButtonClick?.Invoke(this, PersonID);
        }
    }
}
