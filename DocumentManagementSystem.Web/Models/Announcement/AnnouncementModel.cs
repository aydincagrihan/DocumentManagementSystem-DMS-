using DocumentManagementSystem.Core.Entities;
using DocumentManagementSystem.Core.Enums;
using DocumentManagementSystem.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagementSystem.Web.Models.Announcement
{
    public class AnnouncementModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// Ajax.BeginForm tarafında submit edilebilmesi için
        /// Bu constructor eklendi
        /// </summary>
        public AnnouncementModel()
        {

        }

        public AnnouncementModel(int announcementId, string operationType, IService<DocumentManagementSystem.Core.Entities.Announcement> _announcementService)
        {
            var announcement = new DocumentManagementSystem.Core.Entities.Announcement();
            if (announcementId != 0 || Constants.OperationType.Update == operationType)
            {
                announcement = _announcementService.GetById(announcementId);
            }
            else
            {

            }
            Id = announcement.Id;
            Title = announcement.Title;
            Content = announcement.Content;
            ReleaseDate = announcement.ReleaseDate;
        }
    }
}
