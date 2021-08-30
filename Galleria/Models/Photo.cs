using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Galleria
{
    public partial class Photo
    {
        public Photo()
        {
            Reviews = new HashSet<Review>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhotoId { get; set; }
        public string UserId { get; set; }
        public int? CategoryId { get; set; }
        public int? ColorId { get; set; }
        public DateTime? Timestamp { get; set; }
        public string FileUrl { get; set; }
        public string DisplayName { get; set; }

        public virtual Category Category { get; set; }
        public virtual Color Color { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
