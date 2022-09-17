using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AspMvcCrudWithImage.Models;

namespace AspMvcCrudWithImage.Controllers
{
    public class ImagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Images
        public ActionResult Index()
        {
            return View(db.Images.ToList());
        }
        // GET: Images/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }
        // GET: Images/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Images/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase ImagePath, Image image)
        {
            if (ModelState.IsValid)
            {
                Image img = new Image();
                img.Title = image.Title;
                img.ImagePath = UploadImage(ImagePath);

                db.Images.Add(img);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        // GET: Images/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase ImagePath, Image image)
        {
            if (ModelState.IsValid)
            {
                Image imgs = new Image();
                imgs.Id = image.Id;
                imgs.Title = image.Title;
                imgs.ImagePath = UploadImage(ImagePath);
                db.Entry(imgs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        // GET: Images/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }
        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Image image = db.Images.Find(id);
            db.Images.Remove(image);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        private string UploadImage(HttpPostedFileBase ImagePath)
        {
            string fileName = Path.GetFileNameWithoutExtension(ImagePath.FileName);
            string extension = Path.GetExtension(ImagePath.FileName);
            string newFileName = fileName + "-" + DateTime.Now.ToString("yyyymmssffff") + extension;
            string UploadPath = Path.Combine(Server.MapPath("~/Content/Img/"), newFileName);
            string uniqueFileName = "Content/Img/" + newFileName;
            ImagePath.SaveAs(UploadPath);
            return uniqueFileName;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
