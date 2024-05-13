using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace System
{
    public partial class Rooms_Admin_UC : UserControl
    {
        private const string connection = "server=localhost;port=3306;database=managementsystem;user=root;password=";
        List<int> matchedIndexes = new List<int>();

        double roomPrice = 0, paymentAmount = 0, paymentChange = 0,totalAmount = 0;

        private Guna2Panel[] rooms;


        public Rooms_Admin_UC()
        {
            InitializeComponent();
            configureRooms();

            grpBoxFirst.Hide();
            grpBoxSecond.Hide();
            grpBoxThird.Hide();
            grpBoxFourth.Hide();
            grpBoxFifth.Hide();
            grpBoxSixth.Hide();
            grpBoxSeventh.Hide();
            grpBoxEigth.Hide();
            grpBoxNinth.Hide();
        }

        private void pnlAvailUnit_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Rooms_Admin_UC_Load(object sender, EventArgs e)
        {
            panelCheckOut.Hide();

            lblTime.Text = DateTime.Now.ToLongTimeString();
            dtCheckIn.Text = DateTime.Now.ToShortDateString();
            dtCheckOut.Text = DateTime.Now.ToShortDateString();

            kitchenAmenitiesGrid.Rows.Add("Coffe Machine");
            kitchenAmenitiesGrid.Rows.Add("Toaster");
            kitchenAmenitiesGrid.Rows.Add("Blender");
            kitchenAmenitiesGrid.Rows.Add("Plates");
            kitchenAmenitiesGrid.Rows.Add("Kitchen Utensils");
            kitchenAmenitiesGrid.Rows.Add("Pans");

            bedroomAmenitiesGrid.Rows.Add("Extra Pillows");
            bedroomAmenitiesGrid.Rows.Add("Hangers");
            bedroomAmenitiesGrid.Rows.Add("Bed Sheets");
            bedroomAmenitiesGrid.Rows.Add("Drawer");
            bedroomAmenitiesGrid.Rows.Add("Wardrobe");

            bathroomAmenitiesGrid.Rows.Add("Shampoo");
            bathroomAmenitiesGrid.Rows.Add("Body Soap");
            bathroomAmenitiesGrid.Rows.Add("Bidet");
            bathroomAmenitiesGrid.Rows.Add("Shower");
            bathroomAmenitiesGrid.Rows.Add("Bath Towel");
            bathroomAmenitiesGrid.Rows.Add("Hair Dryer");

            roomsAmenitiesGrid.Rows.Add("Wifi");
            roomsAmenitiesGrid.Rows.Add("Air Condition");
            roomsAmenitiesGrid.Rows.Add("Room Service");
            roomsAmenitiesGrid.Rows.Add("Free Parking");
            roomsAmenitiesGrid.Rows.Add("Washing Machine");
            roomsAmenitiesGrid.Rows.Add("Luggage Storage");

            identifyOccupiedRooms();

        }

        private void identifyOccupiedRooms(){

            string selectQuery = "SELECT idRooms, room_status FROM rooms";
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, con))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int roomId = reader.GetInt32("idRooms");
                            string roomStatus = reader.GetString("room_status");

                            var room = rooms.FirstOrDefault(r => r.Tag.Equals(roomId));
                            if (room != null)
                            {

                                if (roomStatus.Equals("Occupied"))
                                {
                                    room.BorderColor = Color.Blue;
                                }
                                else
                                {
                                    room.BorderColor = Color.Black;
                                }
                            }
                        }
                    }
                }

            }
        }

        private void configureRooms()
        {
            rooms = new Guna2Panel[] {
                room1, room2, room3 , room4 ,room5, room6, room7, room8, room9, room10, room11,
                room12, room13 , room14 ,room15, room16, room17, room18, room19, room20, room21,
                room22, room23 , room24 ,room25, room26, room27, room28, room29, room30, room31,
                room32, room33 , room34 ,room35, room36, room37, room38, room39, room40, room41,
                room42, room43 , room44 , room45, room46, room47, room48, room49, room50,
                room51, room52, room53 , room54 ,room55, room56, room57, room58, room59, room60,  room61,
                room62, room63 , room64 ,room65, room66, room67, room68, room69, room70, room71,
                room72, room73 , room74 ,room75, room76, room77, room78, room79, room80, room81,
                room82, room83 , room84 ,room85, room86, room87, room88, room89, room90, room91,
                room92, room93 , room94 , room95, room96, room97, room98, room99, room100};


            for (int i = 0; i < rooms.Length; i++)
            {
                rooms[i].Tag = i + 1;
                rooms[i].Click += Room_Click;
                rooms[i].MouseLeave += Room_MouseLeave;
                rooms[i].BorderColor = Color.Black;
            }

        }

        private void Room_Click(object sender, EventArgs e)
        {
            string selectQuery = "SELECT name, check_out, status FROM bookings WHERE room_number = @roomnumber";
            Guna2Panel clickedPanel = (Guna2Panel)sender;
            int roomIndex = Array.IndexOf(rooms, clickedPanel) + 1;
            txtRoomNumber.Text = roomIndex.ToString();

            lblRoomNumber.Text = roomIndex.ToString();

            if (clickedPanel.BorderColor == Color.Blue)
            {
                clickedPanel.BorderColor = Color.Blue;
                panelCheckOut.Show();
                guna2Panel685.Hide();


                using (MySqlConnection sqlcon = new MySqlConnection(connection))
                {
                    sqlcon.Open();
                    using (MySqlCommand cmd = new MySqlCommand(selectQuery, sqlcon))
                    {
                        cmd.Parameters.AddWithValue("@roomnumber", roomIndex);
                        // Execute the query
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string status = reader["status"].ToString();
                                if (status != "Checked Out")
                                {
                                    occupantName.Text = reader["name"].ToString();
                                    checkOutDate.Value = (DateTime)reader["check_out"];
                                }
                            }
                        }
                    }
                }


            }
            else
            {
                clickedPanel.BorderColor = Color.Red;
                panelCheckOut.Hide();
                guna2Panel685.Show();
            }

            if (string.IsNullOrEmpty(txtRoomNumber.Text))
            {
                MessageBox.Show("Please enter a room number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string selectRooms = "SELECT idRooms, room_capacity, rooms_included, room_status, room_price FROM rooms WHERE idRooms = @roomIndex";

            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();

                using (MySqlCommand cmd = new MySqlCommand(selectRooms, con))
                {
                    cmd.Parameters.AddWithValue("@roomIndex", txtRoomNumber.Text); // Fixed parameter name

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtRoomNumber.Text = reader["idRooms"].ToString();
                            roomCapacity.Text = reader["room_capacity"].ToString();
                            roomInclusion.Text = reader["rooms_included"].ToString();
                            roomStatus.Text = reader["room_status"].ToString();
                            txtPrice.Text = reader["room_price"].ToString();

                            roomPrice = double.Parse(txtPrice.Text);

                        }
                        else
                        {
                            MessageBox.Show("Room not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //label11.Text = label12.Text = label3.Text = label15.Text = label13.Text = "";
                        }
                    }
                }
            }
            showKitchenAmenities();
            showBedroomAmenities();
            showBathroomAmenities();
            showRoomsAmenities();
        }

        private void Room_MouseLeave(object sender, EventArgs e)
        {
            Guna2Panel clickedPanel = (Guna2Panel)sender;


            if (clickedPanel.BorderColor == Color.Blue)
            {
                clickedPanel.BorderColor = Color.Blue;
            }
            else
            {
                clickedPanel.BorderColor = Color.Black;
            }


        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
           /* lblCurrentFloor.Text = "GROUND FLOOR";*/

            grpBoxGround.Show();
            grpBoxFirst.Hide();
            grpBoxSecond.Hide();
            grpBoxThird.Hide();
            grpBoxFourth.Hide();
            grpBoxFifth.Hide();
            grpBoxSixth.Hide();
            grpBoxSeventh.Hide();
            grpBoxEigth.Hide();
            grpBoxNinth.Hide();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {

            grpBoxGround.Hide();
            grpBoxFirst.Show();
            grpBoxSecond.Hide();
            grpBoxThird.Hide();
            grpBoxFourth.Hide();
            grpBoxFifth.Hide();
            grpBoxSixth.Hide();
            grpBoxSeventh.Hide();
            grpBoxEigth.Hide();
            grpBoxNinth.Hide();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            grpBoxGround.Hide();
            grpBoxFirst.Hide();
            grpBoxSecond.Show();
            grpBoxThird.Hide();
            grpBoxFourth.Hide();
            grpBoxFifth.Hide();
            grpBoxSixth.Hide();
            grpBoxSeventh.Hide();
            grpBoxEigth.Hide();
            grpBoxNinth.Hide();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            grpBoxGround.Hide();
            grpBoxFirst.Hide();
            grpBoxSecond.Hide();
            grpBoxThird.Show();
            grpBoxFourth.Hide();
            grpBoxFifth.Hide();
            grpBoxSixth.Hide();
            grpBoxSeventh.Hide();
            grpBoxEigth.Hide();
            grpBoxNinth.Hide();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {

            grpBoxGround.Hide();
            grpBoxFirst.Hide();
            grpBoxSecond.Hide();
            grpBoxFourth.Show();
            grpBoxFifth.Hide();
            grpBoxThird.Hide();
            grpBoxSixth.Hide();
            grpBoxSeventh.Hide();
            grpBoxEigth.Hide();
            grpBoxNinth.Hide();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            grpBoxGround.Hide();
            grpBoxFirst.Hide();
            grpBoxSecond.Hide();
            grpBoxFifth.Show();
            grpBoxFourth.Hide();
            grpBoxThird.Hide();
            grpBoxSixth.Hide();
            grpBoxSeventh.Hide();
            grpBoxEigth.Hide();
            grpBoxNinth.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

            grpBoxGround.Hide();
            grpBoxFirst.Hide();
            grpBoxSecond.Hide();
            grpBoxThird.Hide();
            grpBoxFourth.Hide();
            grpBoxFifth.Hide();
            grpBoxSixth.Show();
            grpBoxSeventh.Hide();
            grpBoxEigth.Hide();
            grpBoxNinth.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            grpBoxGround.Hide();
            grpBoxFirst.Hide();
            grpBoxSecond.Hide();
            grpBoxThird.Hide();
            grpBoxFourth.Hide();
            grpBoxFifth.Hide();
            grpBoxSixth.Hide();
            grpBoxSeventh.Show();
            grpBoxEigth.Hide();
            grpBoxNinth.Hide();
        }

        private void btnEightFloor_Click(object sender, EventArgs e)
        {
            grpBoxGround.Hide();
            grpBoxFirst.Hide();
            grpBoxSecond.Hide();
            grpBoxThird.Hide();
            grpBoxFourth.Hide();
            grpBoxFifth.Hide();
            grpBoxSixth.Hide();
            grpBoxSeventh.Hide();
            grpBoxEigth.Show();
            grpBoxNinth.Hide();
        }

        private void btnNinthFloor_Click(object sender, EventArgs e)
        {
            grpBoxGround.Hide();
            grpBoxFirst.Hide();
            grpBoxSecond.Hide();
            grpBoxThird.Hide();
            grpBoxFourth.Hide();
            grpBoxFifth.Hide();
            grpBoxSixth.Hide();
            grpBoxSeventh.Hide();
            grpBoxEigth.Hide();
            grpBoxNinth.Show();
        }

        private void timerSetTime_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToLongTimeString();
            timerSetTime.Start();
        }

        private void btnBookRoom_Click(object sender, EventArgs e)
        {
        
            if (!string.IsNullOrEmpty(txtPayment.Text))
            {
                if (!double.TryParse(lblTotalAmount.Text, out roomPrice))
                {
                    MessageBox.Show("Invalid room price.");
                    return;
                }

                if (!double.TryParse(txtPayment.Text, out paymentAmount))
                {
                    MessageBox.Show("Invalid payment amount.");
                    return;
                }

                paymentChange = paymentAmount - roomPrice;

                if (paymentAmount < 0)
                {
                    MessageBox.Show("Insufficient amount.");
                }

                txtChange.Text = paymentChange.ToString();
            }
            else
            {
                MessageBox.Show("Input payment amount.");
            }

            if (string.IsNullOrEmpty(txtClientName.Text) || string.IsNullOrEmpty(txtPayment.Text) || dtCheckIn.Value == null || dtCheckOut.Value == null)
            {
                MessageBox.Show("Please fill up the blank space", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string insertQuery = "INSERT INTO bookings (name, check_in, check_out, payment_amount, payment_change, room_number) VALUES (@Name, @CheckIn, @CheckOut, @PaymentAmount,@PaymentChange,@room_number)";

                using (MySqlConnection con = new MySqlConnection(connection))
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@Name", txtClientName.Text);
                        cmd.Parameters.AddWithValue("@CheckIn", dtCheckIn.Value);
                        cmd.Parameters.AddWithValue("@CheckOut", dtCheckOut.Value);
                        cmd.Parameters.AddWithValue("@PaymentAmount", decimal.Parse(txtPayment.Text));
                        cmd.Parameters.AddWithValue("@PaymentChange", txtChange.Text);
                        cmd.Parameters.AddWithValue("@room_number", txtRoomNumber.Text);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            roomOccupants();
                            MessageBox.Show("Booked done!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            identifyOccupiedRooms();
                            timerOverdueCheck.Stop();
                        }
                        else
                        {
                            MessageBox.Show("Booking Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        public void roomOccupants()
        {
            string updateQuery = "UPDATE rooms SET room_status = @room_status WHERE idRooms = @roomIndex";

            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();

                using (MySqlCommand cmd = new MySqlCommand(updateQuery, con))
                {

                    cmd.Parameters.AddWithValue("@room_status", "Occupied");
                    cmd.Parameters.AddWithValue("@roomIndex", txtRoomNumber.Text);

                    int result = cmd.ExecuteNonQuery();

                }
            }
        }

        public void showKitchenAmenities()
        {
            String roomId = txtRoomNumber.Text;

            string sqlQuery = "SELECT coffee_machine, toaster, blender, plates, kitchen_utensils, pans FROM kitchen_amenities WHERE idRooms = @roomId";
            string connectionString = "server=localhost;port=3306;database=managementsystem;user=root;password=";

            MySqlConnection connectionSql = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand(sqlQuery, connectionSql);
            cmd.Parameters.AddWithValue("@roomId", roomId);

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            try
            {
                connectionSql.Open();
                dataAdapter.Fill(dt);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("MySQL Error: " + ex.Message);
            }
            finally
            {
                connectionSql.Close();
            }

            int rowCount = Math.Min(kitchenAmenitiesGrid.Rows.Count, dt.Columns.Count);

            for (int i = 0; i < rowCount; i++)
            {
                kitchenAmenitiesGrid.Rows[i].Cells[1].Value = dt.Rows[0][i];
            }
        }

        public void showBathroomAmenities()
        {
            String roomId = txtRoomNumber.Text;

            string sqlQuery = "SELECT shampoo, body_soap, bidet,shower, bath_towel, hair_dryer FROM bathroom_amenities WHERE idRooms = @roomId";
            string connectionString = "server=localhost;port=3306;database=managementsystem;user=root;password=";

            MySqlConnection connectionSql = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand(sqlQuery, connectionSql);
            cmd.Parameters.AddWithValue("@roomId", roomId);

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            try
            {
                connectionSql.Open();
                dataAdapter.Fill(dt);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("MySQL Error: " + ex.Message);
            }
            finally
            {
                connectionSql.Close();
            }

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    bathroomAmenitiesGrid.Rows[i].Cells[1].Value = dt.Rows[0][i];
                }
            }
        }

        public void showBedroomAmenities()
        {
            String roomId = txtRoomNumber.Text;

            string sqlQuery = "SELECT extra_pillow, hangers, bed_sheets,drawers, wardrobe FROM bedroom_amenities WHERE idRooms = @roomId";
            string connectionString = "server=localhost;port=3306;database=managementsystem;user=root;password=";

            MySqlConnection connectionSql = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand(sqlQuery, connectionSql);
            cmd.Parameters.AddWithValue("@roomId", roomId);

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            try
            {
                connectionSql.Open();
                dataAdapter.Fill(dt);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("MySQL Error: " + ex.Message);
            }
            finally
            {
                connectionSql.Close();
            }

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    bedroomAmenitiesGrid.Rows[i].Cells[1].Value = dt.Rows[0][i];
                }
            }
        }

        public void showRoomsAmenities()
        {
            String roomId = txtRoomNumber.Text;

            string sqlQuery = "SELECT wifi, air_conditioned, room_service, free_parking, washing_machine, luggage_storage FROM room_amenities WHERE idRooms = @roomId";
            string connectionString = "server=localhost;port=3306;database=managementsystem;user=root;password=";

            MySqlConnection connectionSql = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand(sqlQuery, connectionSql);
            cmd.Parameters.AddWithValue("@roomId", roomId);

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            try
            {
                connectionSql.Open();
                dataAdapter.Fill(dt);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("MySQL Error: " + ex.Message);
            }
            finally
            {
                connectionSql.Close();
            }

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    roomsAmenitiesGrid.Rows[i].Cells[1].Value = dt.Rows[0][i];
                }
            }
        }

        private void roomsAmenitiesGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void txtChange_TextChanged(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void txtPayment_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtCheckOut_ValueChanged(object sender, EventArgs e)
        {
            string checkinText = dtCheckIn.Text;
            string checkoutText = dtCheckOut.Text;

            DateTime checkin;
            DateTime checkout;
            if (DateTime.TryParse(checkinText, out checkin) && DateTime.TryParse(checkoutText, out checkout))
            {

                TimeSpan difference = checkout - checkin;
                int differenceInDays = difference.Days;

            
                totalAmount = roomPrice * differenceInDays;

                string formattedTotalAmount = totalAmount.ToString("0.00");

                lblTotalAmount.Text = formattedTotalAmount;
            }

        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            string updateQuery = "UPDATE rooms SET room_status = @roomstatus WHERE idRooms = @roomIndex";

            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();

                using (MySqlCommand cmd = new MySqlCommand(updateQuery, con))
                {
                    cmd.Parameters.AddWithValue("@roomstatus", "Available");
                    cmd.Parameters.AddWithValue("@roomIndex", txtRoomNumber.Text);

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Booking status updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Failed to update booking status", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void room96_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
