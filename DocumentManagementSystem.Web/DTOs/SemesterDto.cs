using DocumentManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagementSystem.Web.DTOs
{
    public class SemesterDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string SemesterType { get; set; }

        public int Code { get; set; }
    }
}
