using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagementSystem.Core.Entities
{
	/// <summary>
	/// Courses Table
	/// </summary>
	public class Course
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Code { get; set; }

		public int Credit { get; set; }

		//public DateTime? Semester { get; set; }

        public int SemesterId { get; set; }
		public virtual Semester Semester { get; set; }

		public string Descripton { get; set; }

		public bool IsDeleted { get; set; }

		public int ProgramsId { get; set; }
		public virtual Programs Programs { get; set; }

		public ICollection<StudentInfo> StudenInfo { get; set; }

	}
}
