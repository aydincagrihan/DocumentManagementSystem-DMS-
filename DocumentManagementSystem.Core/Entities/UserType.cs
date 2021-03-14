using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DocumentManagementSystem.Core.Entities
{
    public class UserType
    {
        public UserType()
        {
            Users = new Collection<User>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int? Code { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
