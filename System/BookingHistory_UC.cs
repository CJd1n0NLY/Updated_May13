using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace System
{
    public partial class BookingHistory_UC : UserControl
    {
        private const string connection = "server=localhost;port=3306;database=managementsystem;user=root;password=";
        public BookingHistory_UC()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BookingHistory_UC_Load(object sender, EventArgs e)
        {
            dtDate.Text = DateTime.Now.ToShortDateString();

            using (MySqlConnection sqlcon = new MySqlConnection(connection))
            {
                sqlcon.Open();
                MySqlDataAdapter sqlDa = new MySqlDataAdapter("SELECT name, check_in, check_out, room_number FROM bookings", sqlcon);
                DataTable tbl = new DataTable();
                sqlDa.Fill(tbl);

                gridBookHistory.Columns["Column1"].DataPropertyName = "name";
                gridBookHistory.Columns["Column2"].DataPropertyName = "check_in";
                gridBookHistory.Columns["Column3"].DataPropertyName = "check_out";
                gridBookHistory.Columns["Column4"].DataPropertyName = "room_number";

                gridBookHistory.DataSource = tbl;


            }
        }

   
    }
}
