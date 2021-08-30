using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Galleria
{
    public class ReviewData
    {
        public class Reviews
        {
            public string Username { get;set; }
            public int Grade { get;set; }
            public DateTime? Timestamp { get; set; }
            public string Comment { get; set; }
        }
        public string Username { get;set; } = "";
        public string FileUrl { get;set; }
        [Display(Name = "Grade")]
        public int newGrade { get;set; }
        [Display(Name = "Comment")]
        [StringLength(100)]
        public string newComment { get;set; }
        public int photoId { get;set; }
        public IList<Grade> GradesList { get;set; }
        public IList<Reviews> ReviewList { get;set; }
        public bool isDuplicate { get;set; } = false;
        public double averageGrade { get;set; }
    }
}

