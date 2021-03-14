using DocumentManagementSystem.Core.Entities;
using DocumentManagementSystem.Core.Enums;
using DocumentManagementSystem.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagementSystem.Web.Models.Course
{
    public class CourseModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Code { get; set; }

        public int Credit { get; set; }

        public string Descripton { get; set; }

        public int ProgramsId { get; set; }

        //public DateTime? Semester { get; set; }

        public int SemesterId { get; set; }

        /// <summary>
        /// Ajax.BeginForm tarafında submit edilebilmesi için
        /// Bu constructor eklendi
        /// </summary>
        public CourseModel()
        {

        }
       
        public CourseModel(int courseId, string operationType, IService<DocumentManagementSystem.Core.Entities.Course> _courseService)
        {
            var course = new DocumentManagementSystem.Core.Entities.Course();
            if (courseId != 0 || Constants.OperationType.Update == operationType)
            {
                course = _courseService.GetById(courseId);
                Code = course.Code;
            }
            else
            {
                //Code alanı otomatik veriliyor.
                var courseList = _courseService.GetAllAsync();
                Code = courseList.Result.ToList().Count + 1;
            }
            Id = course.Id;
            Name = course.Name;
            Credit = course.Credit;
            ProgramsId = course.ProgramsId;
            SemesterId = course.SemesterId;
        }
    }
}
