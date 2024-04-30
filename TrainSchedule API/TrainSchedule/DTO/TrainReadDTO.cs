using System.ComponentModel.DataAnnotations;

namespace TrainSchedule.DTO
{
    public class TrainReadDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = "Not given";

        public string Time { get; set; }

        public string Date { get; set; }

        
        public string StartLocation { get; set; }

        
        public string EndLocation { get; set; }


        public string Box { get; set; }

    }
}
