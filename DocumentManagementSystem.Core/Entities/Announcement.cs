using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagementSystem.Core.Entities
{
    public class Announcement
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime ReleaseDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
