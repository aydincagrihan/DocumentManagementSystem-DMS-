using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagementSystem.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime RegisterDate { get; set; }
        public string PictureUrl { get; set; }
        public string Gender { get; set; }
        public bool IsDeleted { get; set; }
        public int UserTypeId { get; set; }
        public virtual UserType UserType { get; set; }
        public ICollection<StudentInfo> StudenInfo { get; set; }
        public ICollection<Reports> Reports { get; set; }
    }
}
