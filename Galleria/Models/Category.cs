using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Galleria
{
    public partial class Category
    {
        public Category()
        {
            Photos = new HashSet<Photo>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
    }
}
