using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attempt8
{
    public class DLSInterface
    {
        public static string ViewEmail { get; set; }
        public static int Profileid { get; set; }
        public static int ClassEntered{ get; set; }
        public static byte[] image { get; set; }
        public static string loggedEmail { get; set; }

        public static byte[] profileImage { get; set; }
        public static bool isTeacher(string email)
        {
            SE_ProjectEntities db = new SE_ProjectEntities();
            if (db.TeacherTbls.Any(t => t.Email.Equals(email)))
            {
                return true;
            }
            return false;
        }
        public static bool logout()
        {
            if (loggedEmail != null)
            {
                ClassEntered = -1;
                loggedEmail = null;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static int GetIdByEmail(string email)
        {
            SE_ProjectEntities db = new SE_ProjectEntities();

            foreach(TeacherTbl k in db.TeacherTbls)
            {
                if(k.Email == email)
                {
                    return k.Id;
                }
            }
            foreach (StudentTbl s in db.StudentTbls)
            {
                if (s.Email == email)
                {
                    return s.Id;
                }
            }
            return -1;


        }
        public static string GeEmailById(int id)
        {
            SE_ProjectEntities db = new SE_ProjectEntities();

            if (db.TeacherTbls.Any(t => t.Id == id))
            {
                return db.TeacherTbls.Where(t => t.Id == id).FirstOrDefault().Email;
            }
            else if (db.StudentTbls.Any(s => s.Id == id))
            {
                return db.StudentTbls.Where(s => s.Id == id).FirstOrDefault().Email;
            }
            else
            {
                return null;
            }

        }

        public static string GetNameById(int id)
        {
            SE_ProjectEntities db = new SE_ProjectEntities();

            if (db.TeacherTbls.Any(t => t.Id == id))
            {
                return db.TeacherTbls.Where(t => t.Id == id).FirstOrDefault().Name;
            }
            else if (db.StudentTbls.Any(s => s.Id == id))
            {
                return db.StudentTbls.Where(s => s.Id == id).FirstOrDefault().Name;
            }
            else
            {
                return null;
            }

        }
        public static int getNumberofClassesEnrolled(string email)
        {
            int id = 0;
            int count = 0;
            SE_ProjectEntities db = new SE_ProjectEntities();
            if(db.StudentTbls.Any(s => s.Email.Equals(email)))
            {
                id = DLSInterface.GetIdByEmail(email);
                foreach (StudentClassOTM k in db.StudentClassOTMs)
                {
                    if (k.Student_Id == id)
                    {
                        count++;
                    }
                }

            }
            else
            {
                id = DLSInterface.GetIdByEmail(email);
                foreach (TeacherClassOTM s in db.TeacherClassOTMs)
                {
                    if (s.Teacher_Id == id)
                    {
                        count++;
                    }
                }
            }

            return count;
        }
        public static string getControllerName(string url)
        {
            string result = "";
            bool start = false;
            for(int i = 0; i < url.Length; i++)
            { 
                if(url[i].Equals('/'))
                {
                    if(!start)
                    {
                        start = true;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    result += url[i];
                }
            }
            return result;
        }

        public static string getActionName(string url)
        {
            string result = "";
            int start = 0;
            for (int i = 0; i < url.Length; i++)
            {
                if (url[i].Equals('/'))
                {
                    if (start < 2)
                    {
                        start += 1;
                    }
                }
                if (start >= 2)
                {
                    result += url[i];
                }
            }
            string result2 = "";
            for (int i = 1; i < result.Length; i++)
            {
                result2 += result[i];
            }
            return result2;
        }

        public static void ImageFromEmail(string email)
        {
            SE_ProjectEntities db = new SE_ProjectEntities();
            if(db.Profiles.Any(p => p.Email.Equals(email)))
            {
                profileImage = db.Profiles.Where(p => p.Email.Equals(email)).FirstOrDefault().ProfilePicture;
            }
            else
            {
                profileImage = null;
            }
            
        }

        public static string getNameByEmail(string email)
        {
            SE_ProjectEntities db = new SE_ProjectEntities();
            string result = "";
            if (db.TeacherTbls.Any(t=>t.Email.Equals(email)))
            {
                result = db.TeacherTbls.Where(t => t.Email.Equals(email)).FirstOrDefault().Name;
            }
            else if(db.StudentTbls.Any(s => s.Email.Equals(email)))
            {
                result = db.StudentTbls.Where(t => t.Email.Equals(email)).FirstOrDefault().Name;
            }
            return result;
        }


    }
}