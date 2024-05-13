using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace System
{
    public partial class Dashboard : Form
    {

        public Dashboard()
        {
            InitializeComponent();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            rooms_Admin_UC1.BringToFront();
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void maintenanceTab_Click(object sender, EventArgs e)
        {
            dashboardMaintenance1.BringToFront();
        }

        private void administrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form1 showForm1 = new Form1();
            showForm1.Show();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
    
        }
    }
}
  













