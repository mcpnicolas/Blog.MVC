using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class PostModel
    {

        public IEnumerable<SelectListItem> posts { get; set; }
        public string headerMessage { get; set; }
        public string blogTitle { get; set; }
        public int blogID { get; set; }
        public int postID { get; set; }
        [Required(ErrorMessage = "Please enter a Post Title")]
        [Display(Name = "Post Title")]
        public string title { get; set; }
        [Display(Name = "Body")]
        public string body { get; set; }
    }
}