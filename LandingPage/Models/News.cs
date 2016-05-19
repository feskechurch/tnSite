using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LandingPage.Models
{
    public class News
    {
        [Key]
        public int PostID { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime PublishedDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}