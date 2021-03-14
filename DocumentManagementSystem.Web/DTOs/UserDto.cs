using DocumentManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagementSystem.Web.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string UserTypeName { get; set; }

        public int UserTypeCode { get; set; }

        public bool IsDeleted { get; set; }
    }
}
