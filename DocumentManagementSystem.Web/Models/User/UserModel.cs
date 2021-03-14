using DocumentManagementSystem.Core.Entities;
using DocumentManagementSystem.Core.Enums;
using DocumentManagementSystem.Core.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DocumentManagementSystem.Web.Models.User
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public bool IsDeleted { get; set; }
        public int UserTypeId { get; set; }

        /// <summary>
        /// Ajax.BeginForm tarafında submit edilebilmesi için
        /// Bu constructor eklendi
        /// </summary>
        public UserModel()
        {

        }


        public UserModel(int userId, string operationType, IService<DocumentManagementSystem.Core.Entities.User> _userService)
        {
            var user = new DocumentManagementSystem.Core.Entities.User();
            if (userId != 0 || Constants.OperationType.Update == operationType)
            {
                user = _userService.GetById(userId);
            }
            else
            {

            }

            Id = user.Id;
            Password = user.Password;
            Name = user.Name;
            Surname = user.Surname;
            Email = user.Email;
            Gender = user.Gender;
            UserTypeId = user.UserTypeId;
        }
    }
}
