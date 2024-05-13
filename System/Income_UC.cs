using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace System
{
    public partial class Income_UC : UserControl
    {
        private const string con = "server=localhost;port=3306;database=managementsystem;user=root;password=";

        public Income_UC()
        {
            InitializeComponent();
        }

        private void Income_UC_Load(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(con))
            {
                connection.Open();

                
                string earningsQuery = "SELECT SUM(payment_amount) FROM bookings";
                MySqlCommand command = new MySqlCommand(earningsQuery, connection);
                object earningsResult = command.ExecuteScalar();

                if (earningsResult != null && earningsResult != DBNull.Value)
                {
                    decimal totalEarnings = Convert.ToDecimal(earningsResult);
                    totalEarning.Text = "Total Earnings: ₱ " + totalEarnings.ToString("0.00");
                }
                else
                {
                    totalEarning.Text = "Total Earnings: ₱ 0.00";
                }

                
                string countQuery = "SELECT COUNT(*) FROM bookings";
                MySqlCommand comm = new MySqlCommand(countQuery, connection);
                object countResult = comm.ExecuteScalar();

                if (countResult != null && countResult != DBNull.Value)
                {
                    int bookingCount = Convert.ToInt32(countResult);
                    numBooks.Text = "" + bookingCount.ToString();
                }
                else
                {
                    numBooks.Text = "Total Booking : 0";
                }

                string bookingDetailsQuery = "SELECT idBook, name, check_in, check_out, payment_amount FROM bookings";
                MySqlCommand bookingCommand = new MySqlCommand(bookingDetailsQuery, connection);
                using (MySqlDataReader reader = bookingCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idBook = reader.GetInt32("idBook");
                        string name = reader.GetString("name");
                        DateTime checkIn = reader.GetDateTime("check_in");
                        DateTime checkOut = reader.GetDateTime("check_out");
                        decimal paymentAmount = reader.GetDecimal("payment_amount");
                        dtGridIncome.Rows.Add(idBook, name, checkIn.Date.ToString("yyyy-MM-dd"), checkOut.Date.ToString("yyyy-MM-dd"), paymentAmount);
                    }
                }
            }
        }
    }
}


