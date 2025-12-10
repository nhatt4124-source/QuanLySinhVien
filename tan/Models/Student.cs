namespace tan.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Student")]
    public class Student
    {
        [Key]
        [StringLength(20)] // Khớp với nvarchar(20) trong SQL
        public string StudentID { get; set; }

        [Required]
        [StringLength(200)] // Khớp với nvarchar(200) trong SQL
        public string FullName { get; set; }

        public double AverageScore { get; set; } // SQL float tương đương double trong C#

        public int FacultyID { get; set; }

        [ForeignKey("FacultyID")]
        public virtual Faculty Faculty { get; set; }
    }
}