using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DocumentManagementSystem.Core.Entities
{
    public class Semester
    {
        public Semester()
        {
            Courses = new Collection<Course>();
        }

        public int Id { get; set; }

        public int Name { get; set; }

        public string SemesterType { get; set; }

        public virtual string NameSemesterType 
        { 
            get 
            {
                return string.Format("{0} / {1}", Name.ToString(), SemesterType);
            } 
        }

        public int? Code { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
