namespace tan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Faculty")]
    public partial class Faculty
    {
        // Hàm tạo để khởi tạo danh sách Students
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Faculty()
        {
            Students = new HashSet<Student>();
        }

        [Key] // Xác định đây là khóa chính
        public int FacultyID { get; set; }

        [Required]
        [StringLength(200)]
        public string FacultyName { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}