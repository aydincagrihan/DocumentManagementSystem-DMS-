using DocumentManagementSystem.Core.Enums;
using DocumentManagementSystem.Core.Services;
using DocumentManagementSystem.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagementSystem.Web.Models.StudentInfo
{
    public class StudentInfoModel
    {
        public int Id { get; set; }

        //public int UserId { get; set; }

        public int? ProgramsId { get; set; }

        public int? ProgramsCode { get; set; }

        public int CourseId { get; set; }

        public IEnumerable<SelectListItem> Courses { get; set; }

        public int ProjectTypeId { get; set; }

        /// <summary>
        /// Ajax.BeginForm tarafında submit edilebilmesi için
        /// Bu constructor eklendi
        /// </summary>
        public StudentInfoModel()
        {

        }

        
        public StudentInfoModel(int studentInfoId, string operationType, IService<DocumentManagementSystem.Core.Entities.StudentInfo> _studentInfoService, IService<DocumentManagementSystem.Core.Entities.Course> _courseService, IService<DocumentManagementSystem.Core.Entities.Programs> _programService)
        {
            var studentInfo = new DocumentManagementSystem.Core.Entities.StudentInfo();
            if (studentInfoId != 0 || Constants.OperationType.Update == operationType)
            {
                
                studentInfo = _studentInfoService.GetById(studentInfoId);
                ProgramsId = _courseService.GetById(studentInfo.CourseId).ProgramsId;
                ProgramsCode = _programService.GetById((int)ProgramsId).Code;
            }
            else
            {
                ProgramsId = null;
            }

            Id = studentInfo.Id;
            //UserId = SessionManagement.ActiveUserId;
            CourseId = studentInfo.CourseId;
            ProjectTypeId = studentInfo.ProjectTypeId;
        }
    }
}
