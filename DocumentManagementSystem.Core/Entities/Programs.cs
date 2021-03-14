using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagementSystem.Core.Entities
{
	/// <summary>
	/// Courses Table
	/// </summary>
	public class Programs
	{
		/// <summary>
		/// Program Id Bilgisi
		/// </summary>
		public int Id { get; set; }

		public string Name { get; set; }

		public int Code { get; set; }

		public bool IsDeleted { get; set; }

		public virtual ICollection<Course> Courses { get; set; }
	}
}
