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
    public partial class AuditLogs_UC : UserControl
    {
        private const string connection = "server=localhost;port=3306;database=managementsystem;user=root;password=";
        public AuditLogs_UC()
        {
            InitializeComponent();
        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void AuditLogs_UC_Load(object sender, EventArgs e)
        {

            using (MySqlConnection sqlcon = new MySqlConnection(connection))
            {
                sqlcon.Open();
                MySqlDataAdapter sqlDa = new MySqlDataAdapter("SELECT firstname, lastname, role FROM accounts", sqlcon);
                DataTable tbl = new DataTable();
                sqlDa.Fill(tbl);

                gridUserInfo.Columns["Column1"].DataPropertyName = "firstname";
                gridUserInfo.Columns["Column2"].DataPropertyName = "lastname";
                gridUserInfo.Columns["Column3"].DataPropertyName = "role";

                gridUserInfo.DataSource = tbl;


            }
        }

        private void gridUserInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < gridUserInfo.Rows.Count)
            {
                DataGridViewRow row = gridUserInfo.Rows[e.RowIndex];
                string firstnameFilter = row.Cells["Column1"].Value.ToString();
                string lastnameFilter = row.Cells["Column2"].Value.ToString();

                using (MySqlConnection sqlcon = new MySqlConnection(connection))
                {
                    sqlcon.Open();

                    string query = "SELECT log_in_date, log_in_time FROM audit_logs WHERE first_name = @firstname AND last_name = @lastname";
                    MySqlCommand cmd = new MySqlCommand(query, sqlcon);
                    cmd.Parameters.AddWithValue("@firstname", firstnameFilter);
                    cmd.Parameters.AddWithValue("@lastname", lastnameFilter);

                    MySqlDataAdapter sqlDa = new MySqlDataAdapter(cmd);
                    DataTable tbl = new DataTable();
                    sqlDa.Fill(tbl);

                    gridReport.Columns["Column4"].DataPropertyName = "log_in_date";
                    gridReport.Columns["Column5"].DataPropertyName = "log_in_time";
                    gridReport.DataSource = tbl;
                }
            }
        }



    }
}
