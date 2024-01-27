using System.ComponentModel.DataAnnotations.Schema;

namespace FullStackAuth_WebAPI.Models
{
    public class TrainingEvent
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("RoutineId")]
        public Routine Routine { get; set; }
        public int RoutineId { get; set; }


        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
