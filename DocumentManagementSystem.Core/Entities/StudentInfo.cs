using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagementSystem.Core.Entities
{
    public class StudentInfo
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } //One-to-many

        public int CourseId { get; set; }
        public Course Course { get; set; } //one-to-many

        public int ProjectTypeId { get; set; } //one-to-many
        public ProjectType ProjectType { get; set; }

        public bool IsDeleted { get; set; }
    }
}
