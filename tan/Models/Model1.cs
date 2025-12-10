using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace tan.Models
{
    public class StudentContext : DbContext
    {
        public StudentContext() : base("name=Model1") { } // Chuỗi kết nối trong App.config
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
    }
}
