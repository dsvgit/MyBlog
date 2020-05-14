using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Core.Models;
using MyBlog.EntityFrameworkCore;

namespace MyBlog.Web.Mvc.Controllers
{
    public class PostsController : Controller
    {
        readonly BlogDbContext db;
        public PostsController(BlogDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            var list = db.Posts.OrderByDescending(post => post.Id).ToList();
            list.ForEach(post =>
            {   
                post.Text = post.Text.Substring(0, Math.Min(post.Text.Length, 200)) + "...";
            });
            return View(list);
        }

        [HttpGet]
        public IActionResult Show(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            return View(db.Posts.Find(id));
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(Post post)
        {
            db.Posts.Add(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            return View(db.Posts.Find(id));
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(Post post)
        {
            db.Posts.Update(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            var post = new Post { Id = (int) id };
            db.Posts.Attach(post);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
