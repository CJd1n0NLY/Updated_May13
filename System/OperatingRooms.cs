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
    public partial class OperatingRooms : UserControl
    {
        private const string connection = "server=localhost;port=3306;database=managementsystem;user=root;password=";
        public OperatingRooms()
        {
            InitializeComponent();
        }

        private void OperatingRooms_Load(object sender, EventArgs e)
        {
            using (MySqlConnection sqlcon = new MySqlConnection(connection))
            {
                sqlcon.Open();
                MySqlDataAdapter sqlDa = new MySqlDataAdapter("SELECT idRooms, room_status FROM rooms WHERE room_status = 'Occupied'", sqlcon);
                DataTable tbl = new DataTable();
                sqlDa.Fill(tbl);

                gridActiveRooms.Columns["Column1"].DataPropertyName = "idRooms";
                gridActiveRooms.Columns["Column2"].DataPropertyName = "room_status";


                gridActiveRooms.DataSource = tbl;


            }
        }
    }
}
