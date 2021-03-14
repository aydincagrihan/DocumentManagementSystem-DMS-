using DocumentManagementSystem.Core.Enums;
using DocumentManagementSystem.Core.Services;
using System.Linq;

namespace DocumentManagementSystem.Web.Models.Reports
{
    public class ReportsModel
    {
        public int Id { get; set; }

        public int ReportNo { get; set; }
        public string ReportName { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Ajax.BeginForm tarafında submit edilebilmesi için
        /// Bu constructor eklendi
        /// </summary>
        public ReportsModel()
        {

        }
       
        public ReportsModel(int reportsId, string operationType, IService<DocumentManagementSystem.Core.Entities.Reports> _reportsService)
        {
            var reports = new DocumentManagementSystem.Core.Entities.Reports();
            if (reportsId != 0 || Constants.OperationType.Update == operationType)
            {
                reports = _reportsService.GetById(reportsId);
                ReportNo = (int)reports.ReportNo;
            }
            else
            {
                //Report No alanı otomatik veriliyor.
                var reportsList = _reportsService.GetAllAsync();
                ReportNo = reportsList.Result.ToList().Count + 1;
            }
            Id = reports.Id;
            ReportName = reports.ReportName;
            UserId = reports.UserId;
            Description = reports.Description;
        }
    }
}
