using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagementSystem.Core.Entities
{
	/// <summary>
	/// Project Type Table
	/// </summary>
	public class ProjectType
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int Code { get; set; }

		public bool IsDeleted { get; set; }

		public ICollection<StudentInfo> StudenInfo { get; set; }
	}
}
