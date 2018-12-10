using Attempt8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Attempt8.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            int id = DLSInterface.GetIdByEmail(DLSInterface.loggedEmail);
            if(DLSInterface.loggedEmail == null)
            {
                RedirectToAction("Login", "Person");
            }
            SE_ProjectEntities db = new SE_ProjectEntities();
            Profile p = new Profile();
            foreach (Profile k in db.Profiles)
            {
                if (k.Email == DLSInterface.loggedEmail)
                {
                    p.Email = k.Email;
                    p.Name = k.Name;
                    p.id = k.id;
                    p.NumberOfClassesEnrolled = k.NumberOfClassesEnrolled;
                    p.ProfilePicture = k.ProfilePicture;
                    p.PhoneNumber = k.PhoneNumber;
                    p.DateOfBirth = k.DateOfBirth;
                    p.RelationshipStatus = k.RelationshipStatus;
                    p.Designation = k.Designation;
                    p.PersonalInfo = k.PersonalInfo;
                    p.Gender = k.Gender;
                    break;
                }
            }
            ProfileEditViewModels display = new ProfileEditViewModels();
            display.id = p.id;
            display.Email = p.Email;
            display.ProfilePicture = p.ProfilePicture;
            display.PhoneNumber = p.PhoneNumber;
            display.DateOfBirth = p.DateOfBirth;
            display.RelationshipStatus = p.RelationshipStatus;
            display.NumberOfClassesEnrolled = DLSInterface.getNumberofClassesEnrolled(DLSInterface.loggedEmail);
            display.Designation = p.Designation;
            display.PersonalInfo = p.PersonalInfo;
            display.Gender = p.Gender;
            display.Name = p.Name;
            DLSInterface.Profileid = display.id;
            return View(display);
        }

        // GET: Profile/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Profile/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profile/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Profile/Edit/5
        public ActionResult Edit()
        {
            try
            {
                if (DLSInterface.loggedEmail == null)
                {
                    return RedirectToAction("Login", "Person");
                }
                int idx = DLSInterface.Profileid;
               
                ProfileEditViewModels p = new ProfileEditViewModels();
                SE_ProjectEntities db = new SE_ProjectEntities();
                Profile myprofile = db.Profiles.Find(idx);
                p.id = myprofile.id;
                p.Name = myprofile.Name;
                p.PersonalInfo = myprofile.PersonalInfo;
                p.ProfilePicture = myprofile.ProfilePicture;
                DLSInterface.image = myprofile.ProfilePicture;
                p.RelationshipStatus = myprofile.RelationshipStatus;
                p.PhoneNumber = myprofile.PhoneNumber;
                p.Email = myprofile.Email;
                p.Designation = myprofile.Designation;
                p.DateOfBirth = myprofile.DateOfBirth;
                p.Gender = myprofile.Gender;
                return View(p);
            }
            catch
            {
                return View();
            }
            
        }

        // POST: Profile/Edit/5
        [HttpPost]
        public ActionResult Edit(ProfileEditViewModels collection, HttpPostedFileBase file)
        {
            try
            {
                // TODO: Add update logic here
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
                    DLSInterface.image = bs;
                    return View();

                }
                else
                {
                    SE_ProjectEntities db = new SE_ProjectEntities();
                   
                    db.Profiles.Where(p => p.Email.Equals(DLSInterface.loggedEmail)).FirstOrDefault().Name = collection.Name;
                    db.Profiles.Where(p => p.Email.Equals(DLSInterface.loggedEmail)).FirstOrDefault().Gender = collection.Gender;
                    db.Profiles.Where(p => p.Email.Equals(DLSInterface.loggedEmail)).FirstOrDefault().Designation = collection.Designation;
                    db.Profiles.Where(p => p.Email.Equals(DLSInterface.loggedEmail)).FirstOrDefault().RelationshipStatus = collection.RelationshipStatus;
                    db.Profiles.Where(p => p.Email.Equals(DLSInterface.loggedEmail)).FirstOrDefault().DateOfBirth = collection.DateOfBirth;
                    db.Profiles.Where(p => p.Email.Equals(DLSInterface.loggedEmail)).FirstOrDefault().PhoneNumber = collection.PhoneNumber;
                    db.Profiles.Where(p => p.Email.Equals(DLSInterface.loggedEmail)).FirstOrDefault().PersonalInfo = collection.PersonalInfo;
                    db.Profiles.Where(p => p.Email.Equals(DLSInterface.loggedEmail)).FirstOrDefault().ProfilePicture = DLSInterface.image;

                    db.SaveChanges();
                    return RedirectToAction("Index", "Profile");

                }

               
            }
            catch
            {
                return View();
            }
        }

        // GET: Profile/Delete/5
        public ActionResult Delete()
        {

            int id = DLSInterface.GetIdByEmail(DLSInterface.loggedEmail);
            SE_ProjectEntities db = new SE_ProjectEntities();
            foreach (Post p in db.Posts)
            {
                if (p.email == DLSInterface.loggedEmail)
                {
                    db.Posts.Remove(p);
                }
            }
            foreach(Profile my in db.Profiles)
            {
                if(my.Email == DLSInterface.loggedEmail)
                {
                    db.Profiles.Remove(my);
                    break;
                }
            }
           
            if (db.TeacherTbls.Find(id) != null && db.TeacherTbls.Find(id).Email == DLSInterface.loggedEmail)
            {
                db.TeacherTbls.Remove(db.TeacherTbls.Find(id));
               
                foreach(TeacherClassOTM totm in db.TeacherClassOTMs)
                {
                    if(totm.Teacher_Id == id)
                    {
                        db.TeacherClassOTMs.Remove(totm);
                    }
                }

            }
            else
            {
                foreach (StudentClassOTM t in db.StudentClassOTMs)
                {
                    if (t.Student_Id == id)
                    {
                        db.StudentClassOTMs.Remove(t);
                    }
                }
                db.StudentTbls.Remove(db.StudentTbls.Find(id));

            }
            db.SaveChanges();
            DLSInterface.logout();
            return RedirectToAction("Create", "Person");
        }

        // POST: Profile/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
