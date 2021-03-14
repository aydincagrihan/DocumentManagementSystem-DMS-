using DocumentManagementSystem.Core.Enums;
using DocumentManagementSystem.Core.Services;
using System.Linq;

namespace DocumentManagementSystem.Web.Models.UserType
{
    public class UserTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }

       

        /// <summary>
        /// Ajax.BeginForm tarafında submit edilebilmesi için
        /// Bu constructor eklendi
        /// </summary>
        public UserTypeModel()
        {

        }
       
        public UserTypeModel(int userTypeId, string operationType, IService<DocumentManagementSystem.Core.Entities.UserType> _userTypeService)
        {
            var userType = new DocumentManagementSystem.Core.Entities.UserType();
            if (userTypeId != 0 || Constants.OperationType.Update == operationType)
            {
                userType = _userTypeService.GetById(userTypeId);
                Code = (int)userType.Code;
            }
            else
            {
                //Code alanı otomatik veriliyor.
                var userTypeList = _userTypeService.GetAllAsync();
                Code = userTypeList.Result.ToList().Count + 1;
            }
            Id = userType.Id;
            Name = userType.Name;
        }
    }
}
