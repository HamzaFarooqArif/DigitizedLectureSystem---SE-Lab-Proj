using Attempt8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Attempt8.Controllers
{
    public class ClassController : Controller
    {
        // GET: Class
        public ActionResult Index()
        {
            return View();
        }

        // GET: Class/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Class/Create
        public ActionResult Create()
        {
            ViewBag.Warn = "";
            return View();
        }

        // POST: Class/Create
        [HttpPost]
        public ActionResult Create(ClassViewModels model)
        {
            try
            {
                SE_ProjectEntities db = new SE_ProjectEntities();

                if (DLSInterface.loggedEmail == null)
                {
                    ViewBag.Warn = "Please Login First";
                }
                else if(db.TeacherTbls.Any(t => t.Email.Equals(DLSInterface.loggedEmail)))
                {
                    ClassTbl c = new ClassTbl();
                    c.Name = model.name;
                    c.School_Id = db.SchoolTbls.Where(s => s.Name.Equals(model.schoolName)).FirstOrDefault().Id;
                    c.Code = model.classCode;

                    bool isexist = false;

                    foreach(ClassTbl cls in db.ClassTbls)
                    {
                        if(cls.Name == model.name && cls.School_Id == c.School_Id)
                        {
                            isexist = true;
                        }
                    }
                    if(!isexist)
                    {
                        db.ClassTbls.Add(c);
                        db.SaveChanges();
                    }
                    

                    int teacherId = db.TeacherTbls.Where(t => t.Email.Equals(DLSInterface.loggedEmail)).FirstOrDefault().Id;
                    int classId = db.ClassTbls.Where(C => C.Name.Equals(model.name)).FirstOrDefault().Id;

                    bool isExist = false;
                    foreach(TeacherClassOTM tc in db.TeacherClassOTMs)
                    {
                        if(tc.Teacher_Id == teacherId && tc.Class_Id == classId)
                        {
                            isExist = true;
                        }
                    }

                    if(isExist)
                    {
                        ViewBag.Warn = "Already Exists";
                    }
                    else
                    {
                        TeacherClassOTM tc = new TeacherClassOTM();
                        tc.Teacher_Id = teacherId;
                        tc.Class_Id = classId;

                        db.TeacherClassOTMs.Add(tc);
                        db.SaveChanges();
                        ViewBag.Warn = "Success";
                    }
                }
                else
                {
                    ViewBag.Warn = "You Must Be Teacher To Register Class";
                }
                return View("Create");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ViewClass()
        {
            DLSInterface.ClassEntered = -1;
            SE_ProjectEntities db = new SE_ProjectEntities();
            List<SchoolTbl> SchoolList = db.SchoolTbls.ToList();
            ViewBag.SchoolList = new SelectList(SchoolList, "Id", "Name");

            ViewBag.Warn = "";
            return View();
        }

        public JsonResult GetClassList(string schoolName)
        {
            SE_ProjectEntities db = new SE_ProjectEntities();

            if (!db.SchoolTbls.Any(s=>s.Name.Equals(schoolName)))
            {
                List<string> classListEmpty = new List<string>();
                return Json(classListEmpty, JsonRequestBehavior.AllowGet);
            }
            int schoolId = db.SchoolTbls.Where(s => s.Name.Equals(schoolName)).FirstOrDefault().Id;

            db.Configuration.ProxyCreationEnabled = false;
            //List<ClassTbl> ClassList = db.ClassTbls.Where(x => x.School_Id == schoolId).ToList();
            List<string> classList = new List<string>();
            foreach (ClassTbl c in db.ClassTbls)
            {
                if(c.School_Id==schoolId)
                {
                    classList.Add(c.Name);
                }
            }
            return Json(classList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ViewClass(ViewClassViewModels model)
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

                    bool isExist = false;
                    foreach (TeacherClassOTM tc in db.TeacherClassOTMs)
                    {
                        if (tc.Teacher_Id == teacherId && tc.Class_Id == classId)
                        {
                            isExist = true;
                        }
                    }

                    if (!isExist)
                    {
                        ViewBag.Warn = "Not Registered in Class";
                        return View();
                    }
                    
                }
                else if (db.StudentTbls.Any(t => t.Email.Equals(DLSInterface.loggedEmail)))
                {
                    int studentId = db.StudentTbls.Where(s => s.Email.Equals(DLSInterface.loggedEmail)).FirstOrDefault().Id;
                    int classId = db.ClassTbls.Where(c => c.Name.Equals(model.className)).FirstOrDefault().Id;

                    bool isExist = false;
                    foreach (StudentClassOTM sc in db.StudentClassOTMs)
                    {
                        if (sc.Student_Id == studentId && sc.Class_Id == classId)
                        {
                            isExist = true;
                        }
                    }
                    if (!isExist)
                    {
                        ViewBag.Warn = "Not Registered in Class";
                        return View();
                    }
                }
                else
                {
                    int schoolId = db.SchoolTbls.Where(s => s.Name.Equals(model.schoolName)).FirstOrDefault().Id;

                    bool isexist = false;
                    foreach (ClassTbl cl in db.ClassTbls)
                    {
                        if (cl.School_Id == schoolId && cl.Name == model.className)
                        {
                            isexist = true;
                        }
                    }
                    if (!isexist)
                    {
                        ViewBag.Warn = "Class Not Found";
                        return View();
                    }
                }

                int actualClassId = -1;
                foreach(ClassTbl c in db.ClassTbls)
                {
                    if(c.School_Id.ToString().Equals(model.schoolName) && c.Name.Equals(model.className))
                    {
                        actualClassId = c.Id;
                    }
                }
                DLSInterface.ClassEntered = actualClassId;
                return RedirectToAction("ViewClass", "Class");

            }
            catch
            {
                return View();
            }
        }

        public ActionResult ClassDashboard()
        {
            return View();
        }


        // GET: Class/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Class/Edit/5
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

        // GET: Class/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Class/Delete/5
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
