using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class Blog
    {

        public int ID;
        public string title;
        public string author;
        public List<BlogPost> content;

        public int postCount = 1;
        public Blog(int id, string t, string a, string ct = "", string cb = "")
        {
            ID = id;
            title = t;
            author = a;
            content = new List<BlogPost>();

            if (ct != "")
            {
                CreatePost(ct, cb);
            }
        }

        public void CreatePost(string t = "New Post", string b = "")
        {
            BlogPost p = new BlogPost(postCount++, t, b);
            content.Add(p);
        }
    }

    public class BlogPost
    {

        public int postID;
        public string title;
        public string body;

        public BlogPost(int c, string t, string b = "")
        {

            postID = c;
            title = t;
            body = b;
        }
    }



}