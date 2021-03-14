using DocumentManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagementSystem.Web.DTOs
{
	public class CourseDto
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int Code { get; set; }

		public bool IsDeleted { get; set; }

        public int? ProgramsCode { get; set; }

        public string ProgramsName { get; set; }

        public Semester Semester { get; set; }
        public int Credit { get; set; }

        public string Descripton { get; set; }
    }
}
