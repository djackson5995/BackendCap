using System.ComponentModel.DataAnnotations.Schema;

namespace FullStackAuth_WebAPI.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int Weight { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }


        [ForeignKey("RoutineId")]
        public int RoutineId { get; set; }

        public Routine Routine { get; set; }
    }
}
