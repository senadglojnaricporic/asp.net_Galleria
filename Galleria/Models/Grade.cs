using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Galleria
{
    public partial class Grade
    {
        public Grade()
        {
            Reviews = new HashSet<Review>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GradeId { get; set; }
        public int GradeNum { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
