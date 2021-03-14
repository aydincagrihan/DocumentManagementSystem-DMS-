using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagementSystem.Core.Entities
{
	public class Reports
	{
		public int Id { get; set; }

        public int ReportNo { get; set; }

        public int UserId { get; set; }
		public User User { get; set; }

		public string ReportName { get; set; }

		public string Description { get; set; }

        public string ReportPath { get; set; }

        public bool IsDeleted { get; set; }

		public ICollection<StudentFinalGrade> StudentFinalGrades { get; set; }
	}
}
