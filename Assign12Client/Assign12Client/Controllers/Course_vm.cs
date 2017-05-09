using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assign12Client.Controllers
{
    public class CourseBase
    {
        [Key]
        public int CourseId { get; set; }

        public string CourseCode { get; set; }

        public string CourseName { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateRetired { get; set; }

        public string ProgramCode { get; set; }

        public int ProgramId { get; set; }

        public int Semester { set; get; }

        public string Status { get; set; }
    }
}