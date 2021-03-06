using DocumentManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagementSystem.Web.DTOs
{
    public class UserTypeDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Code { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
