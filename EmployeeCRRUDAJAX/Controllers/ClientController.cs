using EmployeeCRRUDAJAX.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
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
             //return View("AddUser", user);
        }

        [HttpPost]
        public ActionResult AddUser(UserMst user)
        {
            ModelState.Remove("Profilephoto");

            if (ModelState.IsValid)
            {
                string uniquefilname = UploadFile(user);
                user.Profilephoto = uniquefilname;
                _db.UserMsts.Add(user);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return PartialView("_UpdateUserPartial");
            }
        }

        [HttpGet]
        public IActionResult UpdateUser(int id)
        {
            var user = _db.UserMsts.FirstOrDefault(x => x.Id == id);
            return PartialView("_UpdateUserPartial", user);
        }

        [HttpPost]
        public IActionResult EditUser(UserMst user)
        {
            if (user.Id > 0)
            {
                if (user.ProfileImage != null)
                {
                    string uniquefilname = UploadFile(user);
                    user.Profilephoto = uniquefilname;

                    _db.Entry(user).State = EntityState.Modified;
                    _db.SaveChanges();
                }
                else
                {
                    _db.Entry(user).State = EntityState.Modified;
                    _db.SaveChanges();
                }
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

            if (user.ProfileImage != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + user.ProfileImage.FileName;

                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    user.ProfileImage.CopyTo(fileStream);
                }

            }
            return fileName;
        }




    }
}
