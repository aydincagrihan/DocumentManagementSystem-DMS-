using DocumentManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagementSystem.Web.DTOs
{
	public class StudentInfoDto
	{
		public int Id { get; set; }

        public string UserName { get; set; }

        public string UserSurname { get; set; }

        public Course Course { get; set; }

        public string ProjectTypeName { get; set; }
    }
}
