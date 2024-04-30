using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Routing;

namespace TrainSchedule.Model
{
    public class Train
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "Not given";

        [Required]
        public string Time { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string StartLocation { get; set; }

        [Required]
        public string EndLocation { get; set; }

        [Required]
        public string Box { get; set; }

      

    }
}
