using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.Net.Http;
using System.Net;

namespace TrainAdmin
{
    public partial class Form2 : Form
    {
        int id;
        public Form2()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            String url = "https://localhost:7132/api/Train"; 
            HttpClient client = new HttpClient();
            TrainSchedule trainSchedule = new TrainSchedule();
            trainSchedule.Name = textName.Text;
            trainSchedule.Time = textTime.Value.ToString("HH:mm");
            trainSchedule.Date = textDate.Value.ToString();
            trainSchedule.StartLocation = textSLocation.Text;
            trainSchedule.EndLocation = textELocation.Text;
            trainSchedule.Box =textBoxNum.Text;
            string data = (new JavaScriptSerializer()).Serialize(trainSchedule);
            var content = new StringContent(data, UnicodeEncoding.UTF8, "application/json");
            var res = client.PostAsync(url, content).Result;
            if (res.IsSuccessStatusCode)
            {
                MessageBox.Show("Train Added");
                LoadTrain();

            }
            else
            {
                MessageBox.Show("Fail Train Added");
            }
        }

        public void LoadTrain()
        {
            string url = "https://localhost:7132/api/Train";
            WebClient client = new WebClient();
            client.Headers["content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.DownloadString(url);
            dgv.DataSource = null;
            dgv.DataSource = (new JavaScriptSerializer()).Deserialize<List<TrainSchedule>>(json);
            dgv.Columns["Id"].Visible = false;

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            LoadTrain();
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                textName.Text = dgv.Rows[e.RowIndex].Cells[3].Value.ToString();
                textTime.Text = dgv.Rows[e.RowIndex].Cells[4].Value.ToString();
                textDate.Text = dgv.Rows[e.RowIndex].Cells[5].Value.ToString();
                textSLocation.Text = dgv.Rows[e.RowIndex].Cells[6].Value.ToString();
                textELocation.Text = dgv.Rows[e.RowIndex].Cells[7].Value.ToString();
                textBoxNum.Text = dgv.Rows[e.RowIndex].Cells[8].Value.ToString();
                id = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[2].Value.ToString());
            }
            else if (e.ColumnIndex == 1)
            {
                id = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[2].Value.ToString());
                string url = "https://localhost:7132/api/Train/" + id;
                HttpClient client = new HttpClient();
                DialogResult result = MessageBox.Show("Are you sure you want to Delete?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    var res = client.DeleteAsync(url).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        LoadTrain();
                    }
                }
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            String url = "https://localhost:7132/api/Train/" + id;
            HttpClient client = new HttpClient();
            TrainSchedule trainSchedule = new TrainSchedule();
            trainSchedule.Id = id;
            trainSchedule.Name = textName.Text;
            trainSchedule.Time = textTime.Value.ToString("HH:mm");
            trainSchedule.Date = textDate.Value.ToString();
            trainSchedule.StartLocation = textSLocation.Text;
            trainSchedule.EndLocation = textELocation.Text;
            trainSchedule.Box = textBoxNum.Text;
            string data = (new JavaScriptSerializer()).Serialize(trainSchedule);
            var content = new StringContent(data, UnicodeEncoding.UTF8, "application/json");
            var res = client.PutAsync(url, content).Result;
            if (res.IsSuccessStatusCode)
            {
                MessageBox.Show("Train Update");
                LoadTrain();

            }
            else
            {
                MessageBox.Show("Fail Train Update");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Hide();
        }

        private void textTime_TextChanged(object sender, EventArgs e)
        {

        }
    }

    public class TrainSchedule
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Time { get; set; }

        public string Date { get; set; }

        public string StartLocation { get; set; }

        public string EndLocation { get; set; }

        public string Box { get; set; }

       
    }
}
