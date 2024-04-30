using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.Net.Http;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Net.Mime.MediaTypeNames;

namespace TrainUser
{
    public partial class Form1 : Form
    {
        int id;

        public Form1()
        {
            InitializeComponent();
        }

        public void LoadUser()
        {
            string url = "https://localhost:7132/api/Train";
            WebClient client = new WebClient();
            client.Headers["content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.DownloadString(url);

            List<Train> trains = (new JavaScriptSerializer()).Deserialize<List<Train>>(json);


            string startLocationFilter = textSL.Text.Trim();
            string endLocationFilter = textEL.Text.Trim();
            DateTime dateFilter = dateTimePicker1.Value.Date;

            List<Train> filteredTrains = trains.Where(train =>
                (string.IsNullOrEmpty(startLocationFilter) || train.StartLocation == startLocationFilter) &&
                (string.IsNullOrEmpty(endLocationFilter) || train.EndLocation == endLocationFilter) &&
                (train.Date == dateFilter.ToShortDateString())
            ).ToList();


            dgv1.DataSource = null;
            dgv1.DataSource = filteredTrains;
            dgv1.Columns["Id"].Visible = false;

        }

        public void LoadBooking()
        {
            
            List<Label> labels = new List<Label> { lb1, lb2, lb3, lb4, lb5, lb6, lb7, lb8, lb9, lb10,
                                           lb11, lb12, lb13, lb14, lb15, lb16, lb17, lb18, lb19, lb20 };

            string textBoxNumText = textBoxNum.Text.Trim();

            if (string.IsNullOrEmpty(textBoxNumText))
            {
                foreach (var label in labels)
                {
                    label.BackColor = Color.Green; 
                }
                return;
            }

            string url = "https://localhost:7132/api/Booking";
            WebClient client = new WebClient();
            client.Headers["content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.DownloadString(url);

            List<Booking> bookings = (new JavaScriptSerializer()).Deserialize<List<Booking>>(json);

            string startLocationFilter = textSL.Text.Trim();
            string endLocationFilter = textEL.Text.Trim();
            string trainNameFilter = textTrainName.Text.Trim();
            string boxFilter = textBoxNumText; // Use the trimmed value

            var filteredBookings = bookings.Where(booking =>
                (string.IsNullOrEmpty(startLocationFilter) || booking.StartLocation == startLocationFilter) &&
                (string.IsNullOrEmpty(endLocationFilter) || booking.EndLocation == endLocationFilter) &&
                (string.IsNullOrEmpty(trainNameFilter) || booking.TrainName == trainNameFilter) &&
                (string.IsNullOrEmpty(boxFilter) || booking.Box == boxFilter)
            );

            StringBuilder seatDetails = new StringBuilder();
            foreach (var booking in filteredBookings)
            {
                seatDetails.AppendLine(booking.SeatNumber);
            }

            textBox1.Text = seatDetails.ToString();

            
            string[] bookedSeats = textBox1.Text.Split(',');

            foreach (string seatSegment in bookedSeats)
            {
                string trimmedSegment = seatSegment.Trim(); 
                int seatNumber;
                if (int.TryParse(trimmedSegment, out seatNumber) && seatNumber >= 1 && seatNumber <= 20)
                {
                    labels[seatNumber - 1].BackColor = Color.Red; 
                }
                else
                {
                    
                    string[] subSeats = trimmedSegment.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string subSeat in subSeats)
                    {
                        if (int.TryParse(subSeat, out seatNumber) && seatNumber >= 1 && seatNumber <= 20)
                        {
                            labels[seatNumber - 1].BackColor = Color.Red; 
                        }
                    }
                }
            }
        }



        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadUser();

        }

        private void textSeatNum_TextChanged(object sender, EventArgs e)
        {
            
        }


        private void dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                textTrainName.Text = dgv1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textTime.Text = dgv1.Rows[e.RowIndex].Cells[3].Value.ToString();
            }
        }

        private void btnBooking_Click(object sender, EventArgs e)
        {


            Booking booking = new Booking();
            booking.TrainName = textTrainName.Text;
            booking.StartLocation = textSL.Text;
            booking.EndLocation = textEL.Text;
            booking.Date = dateTimePicker1.Value.ToString();
            booking.Time = dateTimePicker1.Value.ToString("HH:mm");
            booking.UserName = textYourName.Text;
            booking.NIC = textNIC.Text;
            booking.SeatNumber = textSeatNum.Text;
            booking.Box = textBoxNum.Text;

            string[] seats = booking.SeatNumber.Split(',');
            if (seats.Length > 5)
            {
                MessageBox.Show("One person can book only five seats.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!AreSeatsAvailable(booking.TrainName, seats))
            {
                MessageBox.Show("One or more seats are already booked.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Form2 form2 = new Form2();
            form2.SetBookingInformation(booking);
            form2.Show();
            this.Hide();

        }
        private bool AreSeatsAvailable(string trainName, string[] seats)
        {

            return true;
        }



        private void textBoxNum_TextChanged(object sender, EventArgs e)
        {
            LoadBooking();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }

    public class Train
    {
        public int Id { get; set; }

        public string Name { get; set; } = "Not given";

        public string Time { get; set; }

        public string Date { get; set; }


        public string StartLocation { get; set; }


        public string EndLocation { get; set; }


        public string Box { get; set; }
    }

    public class Booking
    {
        public int Id { get; set; }

        public string TrainName { get; set; } = "Not given";

        public string StartLocation { get; set; }

        public string EndLocation { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public string Box { get; set; }

        public string SeatNumber { get; set; }

        public string NIC { get; set; }

        public string UserName { get; set; }
    }
}