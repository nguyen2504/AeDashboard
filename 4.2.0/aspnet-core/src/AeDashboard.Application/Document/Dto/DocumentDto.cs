using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.AutoMapper;
using AeDashboard.Calendar;

namespace AeDashboard.Document.Dto
{
    [AutoMapFrom(typeof(Document))]
    public  class DocumentDto
    {
        [Required]
        public DateTime BeginDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Url { get; set; }
        public int Day { get; set; }
        public int Weekend { get; set; }
        public bool IsActive { get; set; }
        public string Author { get; set; }
    }
}
