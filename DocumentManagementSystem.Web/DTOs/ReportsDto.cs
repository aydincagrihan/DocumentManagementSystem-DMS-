using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagementSystem.Web.DTOs
{
    public class ReportsDto
    {
        public int Id { get; set; }

        public int ReportNo { get; set; }

        public string UserName { get; set; }

        public string UserSurname { get; set; }

        public string ReportName { get; set; }

        public string Description { get; set; }
    }
}
