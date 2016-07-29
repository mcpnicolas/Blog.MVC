using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using Blog.Models;

namespace Blog.Controllers
{
    public static class BlogData
    {
        static BlogData()
        {
            Blogs = new List<Models.Blog>();

            Models.Blog b1 = new Models.Blog(1234, "James Blake Meets Disclosure FanFic", "Sam Gaylert", "First Post");
            Models.Blog b2 = new Models.Blog(4321, "Oregon Lyfe", "Alex L.", "First Post");
            Models.Blog b3 = new Models.Blog(1342, "OMG SMC SUX", "Hannah P.", "First Post");

            Blogs.Add(b1);
            Blogs.Add(b2);
            Blogs.Add(b3);
        }
        public static List<Models.Blog> Blogs { get; set; }
    }
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult Index()
        {
            var IndexModel = new IndexModel();
            // IndexModel.BlogList = BlogList();

            IndexModel.BlogHeader = "eMarketer Company Blogs";
            IndexModel.BlogDescription = "Written by eMarketer employees on their favorite recreational topics.";

            IndexModel.BlogList = BlogData.Blogs;

            return View(IndexModel);
        }

        [HttpGet]
        public ActionResult Read(int bid)
        {
            var BlogModel = new BlogModel();

            Models.Blog thisBlog = BlogData.Blogs.Find(item => item.ID == bid);

            BlogModel.ID = thisBlog.ID;
            BlogModel.content = thisBlog.content;
            BlogModel.title = thisBlog.title;
            BlogModel.author = thisBlog.author;

            BlogModel.header = thisBlog.title;

            return View(BlogModel);
        }

        [HttpGet] 
        public ActionResult BlogEdit(int id)
        {
            var BlogModel = new BlogModel();

            Models.Blog thisBlog = BlogData.Blogs.Find(item => item.ID == id);

            BlogModel.ID = thisBlog.ID;
            BlogModel.content = thisBlog.content;
            BlogModel.title = thisBlog.title;
            BlogModel.author = thisBlog.author;

            BlogModel.header = thisBlog.title;

            return View(BlogModel);
        }

        [HttpGet]
        public ActionResult NewBlogEdit()
        {
            Random rnd = new Random();

            int blogID = rnd.Next(1000, 10000);
            
            if (BlogData.Blogs.Exists(item => item.ID == blogID)) 
            {
                blogID = rnd.Next(1000, 10000);
            }

            BlogData.Blogs.Add(new Models.Blog(blogID, "Untitled", ""));

            var BlogModel = new BlogModel();
            Models.Blog thisBlog = BlogData.Blogs.Find(item => item.ID == blogID);

            BlogModel.ID = thisBlog.ID;
            BlogModel.content = thisBlog.content;
            BlogModel.title = thisBlog.title;
            BlogModel.author = thisBlog.author;

            BlogModel.header = thisBlog.title;

            return RedirectToAction("BlogEdit", new { id = BlogModel.ID });

        }

        [HttpGet]
        public ActionResult PostEdit(int bid, int pid)
        {
            var PostModel = new PostModel();

            // List<Models.Blog> blogs = BlogList();
            Models.Blog thisBlog = BlogData.Blogs.Find(item => item.ID == bid);

            var posts = thisBlog.content;

            Models.BlogPost thisPost = posts.Find(item => item.postID == pid);

            PostModel.blogID = bid;
            PostModel.blogTitle = thisBlog.title;
            PostModel.postID = thisPost.postID;
            PostModel.title = thisPost.title;
            PostModel.body = thisPost.body;

            PostModel.posts = posts.Select(p => new SelectListItem
            {
                Text = p.title,
                Value = p.postID.ToString()
            });
                                                                           
            return View(PostModel);
        }

        [HttpGet]
        public ActionResult NewPostEdit(int bid)
        {
            
            BlogData.Blogs.Find(item => item.ID == bid).CreatePost();

            var PostModel = new PostModel();

            Models.Blog thisBlog = BlogData.Blogs.Find(item => item.ID == bid);

            var posts = BlogData.Blogs.Find(item => item.ID == bid).content;

            Models.BlogPost thisPost = posts[posts.Count - 1];

            PostModel.blogID = bid;
            PostModel.blogTitle = thisBlog.title;
            PostModel.postID = thisPost.postID;
            PostModel.title = thisPost.title;
            PostModel.body = thisPost.body;

            PostModel.posts = posts.Select(p => new SelectListItem
            {
                Text = p.title,
                Value = p.postID.ToString()
            });

            return RedirectToAction("PostEdit", new { bid = PostModel.blogID, pid = PostModel.postID });
        }

        [HttpGet]
        public ActionResult DeleteBlog(int bid)
        {
            BlogData.Blogs.Remove(BlogData.Blogs.Single(b => b.ID == bid));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeletePost(int bid, int pid)
        {
            var blogPosts = BlogData.Blogs.Find(item => item.ID == bid).content;
            blogPosts.Remove(blogPosts.Single(p => p.postID == pid));

            return RedirectToAction("BlogEdit", new { id = bid });
        }


        [HttpPost]
        public ActionResult BlogEdit(BlogModel model, string title, string author)
        {
            if (ModelState.IsValid)
            {

                BlogData.Blogs.Find(item => item.ID == model.ID).title = title;
                BlogData.Blogs.Find(item => item.ID == model.ID).author = author;

                // model.header = BlogData.Blogs.Find(item => item.ID == model.ID).title;
                // model.title = BlogData.Blogs.Find(item => item.ID == model.ID).title;
                // model.author = BlogData.Blogs.Find(item => item.ID == model.ID).author;
                // model.content = BlogData.Blogs.Find(item => item.ID == model.ID).content;

                return RedirectToAction("Index");
            }
            model.header = BlogData.Blogs.Find(item => item.ID == model.ID).title;
            model.content = BlogData.Blogs.Find(item => item.ID == model.ID).content;
            return View(model);
        }

        [HttpPost]
        public ActionResult PostEdit(PostModel model, string title, string body)
        {
            if (ModelState.IsValid)
            {
                BlogData.Blogs.Find(item => item.ID == model.blogID).content.Find(item => item.postID == model.postID).title = title;
                BlogData.Blogs.Find(item => item.ID == model.blogID).content.Find(item => item.postID == model.postID).body = body;

                model.headerMessage = "Your changes have been saved.";
            }

            model.posts = BlogData.Blogs.Find(item => item.ID == model.blogID).content.Select(p => new SelectListItem
            {
                Text = p.title,
                Value = p.postID.ToString()
            });

            model.blogTitle = BlogData.Blogs.Find(item => item.ID == model.blogID).title;

            return View(model);
        }

        [HttpPost]
        public ActionResult GoToPost(PostModel model, FormCollection form)
        {
            var postId = form["Posts"];

            /*
            if (Convert.ToInt32(form["postID"]) != model.postID)
            {
                return RedirectToAction("PostEdit", new { bid = model.blogID, pid = Convert.ToInt32(form["postID"]) });
            }
            */
            return RedirectToAction("PostEdit", new { bid = model.blogID, pid = postId });
        }
    }


}