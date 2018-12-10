using Attempt8.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Attempt8.Controllers
{
    public class PostController : Controller
    {
       
        public ActionResult Index()
        {
            if(DLSInterface.loggedEmail == null)
            {
                return RedirectToAction("Login", "Person");
            }
            SE_ProjectEntities db = new SE_ProjectEntities();
            List<PostViewModels> Posts = new List<PostViewModels>();
            foreach(Post p in db.Posts)
            {
                if(p.class_id == DLSInterface.ClassEntered)
                {
                    PostViewModels each_post = new PostViewModels();
                    each_post.Email = p.email;
                    each_post.id = p.id;
                    each_post.Summary = p.Summary;
                    each_post.details = p.Details;
                    each_post.image = p.Picture;
                    Posts.Add(each_post);
                }


            }
            return View(Posts);
        }

        // GET: Post/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        
        // GET: Post/Create
        public ActionResult Create()
        {
            if (DLSInterface.loggedEmail == null)
            {
                RedirectToAction("Login", "Person");
             }
            return View();
        }

        // POST: Post/Create
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file, string Summary, string Details)
        {
            try
            {
                if (DLSInterface.loggedEmail == null)
                {
                    RedirectToAction("Login", "Person");
                }

                SE_ProjectEntities db = new SE_ProjectEntities();
                Post p = new Post();
                p.class_id = DLSInterface.ClassEntered;
                p.email = DLSInterface.loggedEmail;
                p.Summary = Summary;
                p.Details = Details;
                if (file != null)
                {
                    var bs = new byte[file.ContentLength];
                    using (var fs = file.InputStream)
                    {
                        var offset = 0;
                        do
                        {
                            offset += fs.Read(bs, offset, bs.Length - offset);
                        } while (offset < bs.Length);
                    }
                    p.Picture = bs;
                }
                else
                {
                    p.Picture = null;
                }

                db.Posts.Add(p);
                db.SaveChanges();
                return RedirectToAction("Create", "Post");
            }
            catch
            {
                return View();
            }
        }
        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        // GET: Post/Edit/5
        public ActionResult Edit(int id)
        {
            EditViewModels l = new EditViewModels();
            SE_ProjectEntities db = new SE_ProjectEntities();
            Post p = db.Posts.Find(id);
            DLSInterface.image = p.Picture;
            l.summary = p.Summary;
            l.details = p.Details;
             

            return View(l);
        }

        // POST: Post/Edit/5
        [HttpPost]
        public ActionResult Edit(int id,EditViewModels e, HttpPostedFileBase file)
        {
            try
            {
                if(file != null)
                {
                    var bs = new byte[file.ContentLength];
                    using (var fs = file.InputStream)
                    {
                        var offset = 0;
                        do
                        {
                            offset += fs.Read(bs, offset, bs.Length - offset);
                        } while (offset < bs.Length);
                    }
                    DLSInterface.image = bs;
                    return View();

                }
                else
                {
                    SE_ProjectEntities db = new SE_ProjectEntities();
                    db.Posts.Find(id).Summary = e.summary;
                    db.Posts.Find(id).Details = e.details;
                    db.Posts.Find(id).Picture = DLSInterface.image;
                    db.SaveChanges();
                    DLSInterface.image = null;
                    return RedirectToAction("Index");
                }
                
                

                
            }
            catch
            {
                return View();
            }
        }

        // GET: Post/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                SE_ProjectEntities db = new SE_ProjectEntities();
                Post p = db.Posts.Find(id);
                db.Posts.Remove(p);
                db.SaveChanges();
                return RedirectToAction("Index", "Post");

            }
            catch
            {
                return RedirectToAction("Index");
            }
            
           
        }

        // POST: Post/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection c)
        {
            try
            {
                return View();
            }
            catch
            {
                return View();
            }
        }
        
    }
}
