using System;
using System.Collections.Generic;
using DocumentManagementSystem.Core.Entities;

namespace DocumentManagementSystem.Web.DTOs
{
	public class ProgramDto
	{
        public int Id { get; set; }

		public string Name { get; set; }

		public int Code { get; set; }

		public bool IsDeleted { get; set; }
    }
}
