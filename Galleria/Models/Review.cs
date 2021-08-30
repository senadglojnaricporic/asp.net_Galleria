using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Galleria
{
    public partial class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserId { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PhotoId { get; set; }
        public int GradeId { get; set; }
        public DateTime? Timestamp { get; set; }
        [MaxLength(100)]
        public string Comment { get; set; }

        public virtual Grade Grade { get; set; }
        public virtual Photo Photo { get; set; }
    }
}
