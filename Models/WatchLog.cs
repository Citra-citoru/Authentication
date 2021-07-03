using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace authApi.Models
{
    public class WatchLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id { get; set; }

        [Required]
        public Guid? courseId { get; set; }
        [ForeignKey("courseId")]
        public virtual Course Course { get; set; }

        [Required]
        public Guid? lessonId { get; set; }
        [ForeignKey("lessonId")]
        public virtual Lesson Lesson { get; set; }

        [Required]
        public Guid? userId { get; set; }
        [ForeignKey("userId")]
        public virtual User User { get; set; }

        [Required]        
        public int percentageWatched { get; set; }
    }
}
