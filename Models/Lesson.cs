using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace authApi.Models
{
    public class Lesson
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string videoUrl { get; set; }

        [Required]
        public int order { get; set; }

        [ForeignKey("Section")]
        public virtual Guid sectionId { get; set; }
        public virtual Section Section { get; set; }


    }
}
