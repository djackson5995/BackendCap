using System.ComponentModel.DataAnnotations.Schema;

namespace FullStackAuth_WebAPI.Models
{
    public class Set
    {
        public int Id { get; set; }
        public int CompletedReps { get; set; }
        public int CompletedWeight { get; set; }

        [ForeignKey("ExerciseId")]
        public Exercise Exercise { get; set; }
        public int ExerciseId { get; set; }

        [ForeignKey("TrainingEventId")]
        public TrainingEvent TrainingEvent { get; set; }
        public int TrainingEventId { get; set; }    
    }
}
