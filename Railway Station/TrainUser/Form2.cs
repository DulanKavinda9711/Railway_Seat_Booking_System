using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace TrainUser
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public void SetBookingInformation(Booking booking)
        {

            labName.Text = booking.UserName;
            labNIC.Text = booking.NIC;
            labTrainName.Text = booking.TrainName;
            labDate.Text = booking.Date;
            labTime.Text = booking.Time;
            labSL.Text = booking.StartLocation;
            labEL.Text = booking.EndLocation;
            labBoxNum.Text = booking.Box;
            labSeatNum.Text = booking.SeatNumber;
        }



        private void labName_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = "https://localhost:7132/api/Booking";
            HttpClient client = new HttpClient();
            Booking booking = new Booking();
            booking.UserName = labName.Text;
            booking.NIC = labNIC.Text;
            booking.TrainName = labTrainName.Text;
            booking.Date = labDate.Text;
            booking.Time = labTime.Text;
            booking.StartLocation = labSL.Text;
            booking.EndLocation = labEL.Text;
            booking.Box = labBoxNum.Text;
            booking.SeatNumber = labSeatNum.Text;

            string data = (new JavaScriptSerializer()).Serialize(booking);
            var content = new StringContent(data, UnicodeEncoding.UTF8, "application/json");
            var res = client.PostAsync(url, content).Result;
            if (res.IsSuccessStatusCode)
            {
                
                MessageBox.Show("Your Seat is Booked");
                this.FormClosing += (s, args) =>
                {
                    new Form1().Show();
                };
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to Book Seat. Try Again");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Hide();
        }
    }
}
