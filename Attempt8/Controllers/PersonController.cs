using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Attempt8.Models
{
    public class PersonController : Controller
    {
        // GET: Person
        public ActionResult Index()
        {
            ViewBag.Message ="Hello";
            ViewBag.Date = DateTime.Now;
            return View();
        }

        // GET: Person/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Person/Create
        public ActionResult Create()
        {
            DLSInterface.ClassEntered = -1;
            ViewBag.Message = "";
            return View();
        }

        // POST: Person/Create
        [HttpPost]
        public ActionResult Create(PersonViewModels model)
        {
            
            try
            {
                SE_ProjectEntities db = new SE_ProjectEntities();
                
                if(db.TeacherTbls.Any(t=>t.Email.Equals(model.email)) || db.StudentTbls.Any(t => t.Email.Equals(model.email)))
                {
                    ViewBag.Message = "Email Already Taken!";
                }
                else
                {
                    if (model.isTeacher)
                    {
                        TeacherTbl t = new TeacherTbl();
                        t.Name = model.name;
                        t.Email = model.email;
                        t.Password = model.password;
                        DLSInterface.loggedEmail = t.Email;
                        db.TeacherTbls.Add(t);
                        db.SaveChanges();
                        ViewBag.Message = "Success!";
                    }
                    else
                    {
                        StudentTbl s = new StudentTbl();
                        s.Name = model.name;
                        s.Email = model.email;
                        s.Password = model.password;
                        DLSInterface.loggedEmail = s.Email;
                        db.StudentTbls.Add(s);
                        db.SaveChanges();
                        ViewBag.Message = "Success!";
                    }
                }
                if(DLSInterface.loggedEmail != null)
                {
                   
                    Profile first = new Profile();
                    first.Email = DLSInterface.loggedEmail;
                    first.Name = null;
                    first.ProfilePicture = null;
                    first.PhoneNumber = null;
                    first.DateOfBirth = null;
                    first.RelationshipStatus = null;
                    first.Designation = null;
                    first.NumberOfClassesEnrolled = DLSInterface.getNumberofClassesEnrolled(first.Email);
                    first.PersonalInfo = null;
                    first.Gender = null;
                    db.Profiles.Add(first);
                    db.SaveChanges();
                    return RedirectToAction("ViewClass", "Class");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Login()
        {
            DLSInterface.ClassEntered = -1;
            ViewBag.Message = "";
            DLSInterface.logout();
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModels model)
        {
            SE_ProjectEntities db = new SE_ProjectEntities();
            if(db.TeacherTbls.Any(t=>t.Email.Equals(model.email)))
            {
                if(db.TeacherTbls.Where(t => t.Email.Equals(model.email)).FirstOrDefault().Password.Equals(model.password))
                {
                    DLSInterface.loggedEmail = db.TeacherTbls.Where(t => t.Email.Equals(model.email)).FirstOrDefault().Email;
                }
                else
                {
                    ViewBag.Message = "Email or Password Doesn't match";
                }
            }
            else if(db.StudentTbls.Any(t => t.Email.Equals(model.email)))
            {
                if (db.StudentTbls.Where(t => t.Email.Equals(model.email)).FirstOrDefault().Password.Equals(model.password))
                {
                    DLSInterface.loggedEmail = db.StudentTbls.Where(t => t.Email.Equals(model.email)).FirstOrDefault().Email;
                }
                else
                {
                    ViewBag.Message = "Email or Password Doesn't match";
                }
            }
            else
            {
                ViewBag.Message = "Email not Registered";
            }
            if(DLSInterface.loggedEmail != null)
            {
                return RedirectToAction("ViewClass", "Class");
            }
            return View();
        }

        public ActionResult AddClass()
        {
            SE_ProjectEntities db = new SE_ProjectEntities();
            List<SchoolTbl> SchoolList = db.SchoolTbls.ToList();
            ViewBag.SchoolList = new SelectList(SchoolList, "Id", "Name");

            ViewBag.Warn = "";
            return View();
        }

        [HttpPost]
        public ActionResult AddClass(ViewClassViewModels model)
        {
            try
            {
                SE_ProjectEntities db = new SE_ProjectEntities();

                List<SchoolTbl> SchoolList = db.SchoolTbls.ToList();
                ViewBag.SchoolList = new SelectList(SchoolList, "Id", "Name");

                if (DLSInterface.loggedEmail == null)
                {
                    ViewBag.Warn = "Login First";
                    return View();
                }
                else if (db.TeacherTbls.Any(t => t.Email.Equals(DLSInterface.loggedEmail)))
                {
                    int teacherId = db.TeacherTbls.Where(t => t.Email.Equals(DLSInterface.loggedEmail)).FirstOrDefault().Id;
                    int classId = db.ClassTbls.Where(c => c.Name.Equals(model.className)).FirstOrDefault().Id;
                    string classCode = db.ClassTbls.Where(c => c.Name.Equals(model.className)).FirstOrDefault().Code;

                    bool isExist = false;
                    foreach (TeacherClassOTM tc in db.TeacherClassOTMs)
                    {
                        if (tc.Teacher_Id == teacherId && tc.Class_Id == classId)
                        {
                            isExist = true;
                        }
                    }
                    if(isExist)
                    {
                        ViewBag.Warn = "Already Registered in Class";
                        return View();
                    }
                    else
                    {
                        TeacherClassOTM tc = new TeacherClassOTM();
                        tc.Teacher_Id = teacherId;
                        tc.Class_Id = classId;
                        if(classCode.Equals(model.classCode))
                        {
                            db.TeacherClassOTMs.Add(tc);
                            db.SaveChanges();
                            ViewBag.Warn = "Success!";
                            return View();
                        }
                        else
                        {
                            ViewBag.Warn = "Invalid code!";
                            return View();
                        }
                        
                    }
                }
                else if (db.StudentTbls.Any(t => t.Email.Equals(DLSInterface.loggedEmail)))
                {
                    int studentId = db.StudentTbls.Where(s => s.Email.Equals(DLSInterface.loggedEmail)).FirstOrDefault().Id;
                    int classId = db.ClassTbls.Where(c => c.Name.Equals(model.className)).FirstOrDefault().Id;
                    string classCode = db.ClassTbls.Where(c => c.Name.Equals(model.className)).FirstOrDefault().Code;

                    bool isExist = false;
                    foreach (StudentClassOTM sc in db.StudentClassOTMs)
                    {
                        if (sc.Student_Id == studentId && sc.Class_Id == classId)
                        {
                            isExist = true;
                            break;
                        }
                    }
                    if(isExist)
                    {
                        ViewBag.Warn = "Already Registered in Class";
                        return View();
                    }
                    else
                    {
                        StudentClassOTM sc = new StudentClassOTM();
                        sc.Student_Id = studentId;
                        sc.Class_Id = classId;

                        if (classCode.Equals(model.classCode))
                        {
                            db.StudentClassOTMs.Add(sc);
                            db.SaveChanges();
                            ViewBag.Warn = "Success!";
                            return View();
                        }
                        else
                        {
                            ViewBag.Warn = "Invalid code!";
                            return View();
                        }
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

    

        // GET: Person/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Person/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Person/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Person/Delete/5
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
