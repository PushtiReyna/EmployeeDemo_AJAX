using EmployeeCRRUDAJAX.Models;
using EmployeeCRRUDAJAX.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;

namespace EmployeeCRRUDAJAX.Controllers
{
    public class ClientController : Controller
    {
        public readonly ClientDbContext _db;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public ClientController(ClientDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string GetDataTable()
        {
            var userList = _db.UserMsts.ToList();
            var result = JsonConvert.SerializeObject(new { data = userList });
            return result;

        }
        [HttpGet]
        public IActionResult AddUser(int id)
        {
            var user = _db.UserMsts.FirstOrDefault(x => x.Id == id);
            return PartialView("_AddUserPartial", user);
        }
        [HttpPost]
        public ActionResult AddUser(UserMst user)
        {

            // ModelState.Remove("ProfilePic");
              ModelState.Remove("Profilephoto");
           // ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                //if(user.Id > 0)
                //{
                //    string uniquefilname = UploadFile(user);
                //    user.Profilephoto = uniquefilname;

                //    _db.Entry(user).State = EntityState.Modified;
                //}
                //else
                //{

                string uniquefilname = UploadFile(user);
                user.Profilephoto = uniquefilname;
                //user.IsActive = true;
                _db.UserMsts.Add(user);
                //}
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateUser(int id)
        {
            var user = _db.UserMsts.FirstOrDefault(x => x.Id == id);
            return PartialView("_UpdateUserPartial", user);
        }

        //[HttpPost]
        //public IActionResult EditUser(UserMst user)
        //{
        //    if (user.Id > 0)
        //    {
        //        if (_db.UserMsts.Where(x => x.Profilephoto == user.Profilephoto && x.Id == user.Id).Any())
        //        {
                   
        //            _db.Entry(user).State = EntityState.Modified;
        //            _db.SaveChanges();
        //        }
        //        else
        //        {
        //            string uniquefilname = UploadFile(user);
        //             user.Profilephoto = uniquefilname;

        //            _db.Entry(user).State = EntityState.Modified;
        //            _db.SaveChanges();
        //        }
        //     //   _db.Entry(user).State = EntityState.Modified;
        //      //  _db.SaveChanges();
        //    }
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public IActionResult EditUser(UserMstViewModel user)
        {
            if (user.Id > 0)
            {
                var updateuser = _db.UserMsts.FirstOrDefault(x => x.Id == user.Id);

              
                string uniquefilname = UploadFile(updateuser);
                updateuser.Profilephoto = uniquefilname;

                _db.Entry(updateuser).State = EntityState.Modified;
                    _db.SaveChanges();
                
                    

                  
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var user = _db.UserMsts.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                _db.UserMsts.Remove(user);
                _db.SaveChanges(true);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        private string UploadFile(UserMst user)
        {
            string fileName = null;

            if (user.ProfilePic != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + user.ProfilePic.FileName;

                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    user.ProfilePic.CopyTo(fileStream);
                }
            }
            return fileName;
        }

    }
}
