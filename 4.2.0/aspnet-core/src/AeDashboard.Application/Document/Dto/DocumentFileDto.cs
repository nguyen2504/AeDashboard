using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.AutoMapper;
using AeDashboard.Calendar;

namespace AeDashboard.Document.Dto
{
    [AutoMapFrom(typeof(DocumentFile))]
    public  class DocumentFileDto
    {
        [Required]
        public  bool Important { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Url { get; set; }
        public string Notifications { get; set; }
        //public int Day { get; set; }
        //public int Weekend { get; set; }
        public bool IsActive { get; set; }
        public string Author { get; set; }
        public int IdUser { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }

    }
}
