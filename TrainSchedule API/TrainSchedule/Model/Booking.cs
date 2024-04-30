using System.ComponentModel.DataAnnotations;

namespace TrainSchedule.Model
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TrainName { get; set; } = "Not given";

        [Required]
        public string StartLocation { get; set; }

        [Required]
        public string EndLocation { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string Time { get; set; }

        [Required]
        public string Box { get; set; }

        [Required]
        public string SeatNumber { get; set; }

        [Required]
        public string NIC { get; set; }

        [Required]
        public string UserName { get; set; }

    }
}
