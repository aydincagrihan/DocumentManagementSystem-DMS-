using DocumentManagementSystem.Core.Enums;
using DocumentManagementSystem.Core.Services;
using System.Linq;

namespace DocumentManagementSystem.Web.Models.Semester
{
    public class SemesterModel
    {
        public int Id { get; set; }
        public int Name { get; set; }

        public string SemesterType { get; set; }
        public int Code { get; set; }

        /// <summary>
        /// Ajax.BeginForm tarafında submit edilebilmesi için
        /// Bu constructor eklendi
        /// </summary>
        public SemesterModel()
        {

        }
       
        public SemesterModel(int semesterId, string operationType, IService<DocumentManagementSystem.Core.Entities.Semester> _semesterService)
        {
            var semester = new DocumentManagementSystem.Core.Entities.Semester();
            if (semesterId != 0 || Constants.OperationType.Update == operationType)
            {
                semester = _semesterService.GetById(semesterId);
                Code = (int)semester.Code;
            }
            else
            {
                //Code alanı otomatik veriliyor.
                var semesterList = _semesterService.GetAllAsync();
                Code = semesterList.Result.ToList().Count + 1;
            }
            Id = semester.Id;
            Name = semester.Name;
            SemesterType = semester.SemesterType;
        }
    }
}
