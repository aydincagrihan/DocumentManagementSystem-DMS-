using DocumentManagementSystem.Core.Enums;
using DocumentManagementSystem.Core.Services;
using System.Linq;

namespace DocumentManagementSystem.Web.Models.ProjectType
{
    public class ProjectTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }

       

        /// <summary>
        /// Ajax.BeginForm tarafında submit edilebilmesi için
        /// Bu constructor eklendi
        /// </summary>
        public ProjectTypeModel()
        {

        }
       
        public ProjectTypeModel(int projectTypeId, string operationType, IService<DocumentManagementSystem.Core.Entities.ProjectType> _projectTypeService)
        {
            var projectType = new DocumentManagementSystem.Core.Entities.ProjectType();
            if (projectTypeId != 0 || Constants.OperationType.Update == operationType)
            {
                projectType = _projectTypeService.GetById(projectTypeId);
                Code = projectType.Code;
            }
            else
            {
                //Code alanı otomatik veriliyor.
                var projectTypeList = _projectTypeService.GetAllAsync();
                Code = projectTypeList.Result.ToList().Count + 1;
            }
            Id = projectType.Id;
            Name = projectType.Name;
        }
    }
}
