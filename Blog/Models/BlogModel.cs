using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class BlogModel
    {

        public string header { get; set; }
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter a Blog Title")]
        [Display(Name = "Blog Title")]
        public string title { get; set; }

        [Required(ErrorMessage = "Please enter an author")]
        [Display(Name = "Author")]
        public string author { get; set; }

        public List<BlogPost> content { get; set; }

    }
}