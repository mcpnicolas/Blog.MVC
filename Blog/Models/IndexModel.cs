using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Models
{
    public class IndexModel
    {
        public List<Blog> BlogList;
        public string BlogHeader { get; set; }
        public string BlogDescription { get; set; }
        public int SelectedBlogID { get; set; }
        public IEnumerable<SelectListItem> Blogs { get; set; }
    }
}