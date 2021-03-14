using DocumentManagementSystem.Core.Enums;
using DocumentManagementSystem.Core.Services;
using System.Linq;

namespace DocumentManagementSystem.Web.Models.Program
{
    public class ProgramModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }

       

        /// <summary>
        /// Ajax.BeginForm tarafında submit edilebilmesi için
        /// Bu constructor eklendi
        /// </summary>
        public ProgramModel()
        {

        }
       
        public ProgramModel(int programId, string operationType, IService<DocumentManagementSystem.Core.Entities.Programs> _programService)
        {
            var program = new DocumentManagementSystem.Core.Entities.Programs();
            if (programId != 0 || Constants.OperationType.Update == operationType)
            {
                program = _programService.GetById(programId);
                Code = program.Code;
            }
            else
            {
                //Code alanı otomatik veriliyor.
                var programList = _programService.GetAllAsync();
                Code = programList.Result.ToList().Count + 1;
            }
            Id = program.Id;
            Name = program.Name;
        }
    }
}
