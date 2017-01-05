using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ToDowithAuthentication.Models;

namespace ToDowithAuthentication.Controllers
{
    [Authorize]
    public class UserTasksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserTasks
        public ActionResult Index()
        {
            var userTasks = db.UserTasks.Include(u => u.User);
            return View(userTasks.ToList());
        }

        // GET: UserTasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTask userTask = db.UserTasks.Find(id);
            if (userTask == null)
            {
                return HttpNotFound();
            }
            return View(userTask);
        }

        // GET: UserTasks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,description,title")] UserTask userTask)
        {
            if (ModelState.IsValid)
            {
                userTask.userId = HttpContext.User.Identity.GetUserId();
                userTask.dateCreated = DateTime.Now;
                userTask.isComplete = false;
                db.UserTasks.Add(userTask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userTask);
        }

        // GET: UserTasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTask userTask = db.UserTasks.Find(id);
            if (userTask == null)
            {
                return HttpNotFound();
            }
            return View(userTask);
        }

        // POST: UserTasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,description,title,isComplete,dateCreated,DateCompleted,userId")] UserTask userTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userTask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.userId = new SelectList(db.ApplicationUsers, "Id", "Email", userTask.userId);
            return View(userTask);
        }

        //TODO: Ask why complete doesnt work with a post or a validate, why when I activate the page it is not used.
        //[HttpPost]
        //[ValidateAntiForgeryToken] // TODO: Ask
        //public ActionResult Complete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    UserTask userTask = db.UserTasks.Find(id);
        //    if (userTask == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(userTask);
        //}
        public ActionResult Complete(UserTask userTask)
        {
            userTask = db.UserTasks.First(f => f.id == userTask.id);
            if (ModelState.IsValid && (userTask.isComplete == false || userTask.isComplete == null) && userTask.id != 0)
            {
                userTask.isComplete = true;
                userTask.DateCompleted = DateTime.Now;
                db.Entry(userTask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.userId = new SelectList(db.ApplicationUsers, "Id", "Email", userTask.userId);
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: UserTasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTask userTask = db.UserTasks.Find(id);
            if (userTask == null)
            {
                return HttpNotFound();
            }
            return View(userTask);
        }

        // POST: UserTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserTask userTask = db.UserTasks.Find(id);
            db.UserTasks.Remove(userTask);
            db.SaveChanges();
            return RedirectToAction("Index");
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
