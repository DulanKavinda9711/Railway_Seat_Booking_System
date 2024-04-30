namespace TrainSchedule.DTO
{
    public class BookingCreateDTO
    {

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
