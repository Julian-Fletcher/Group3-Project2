using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Group3_iFINANCEAPP.Models;

namespace Group3_iFINANCEAPP.Controllers
{
    public class NonAdminUsersController : Controller
    {
        private Group3_iFINANCEDBEntities db = new Group3_iFINANCEDBEntities();

        // GET: NonAdminUsers
        public ActionResult Index()
        {
            var nonAdminUser = db.NonAdminUser.Include(n => n.Administrator);
            return View(nonAdminUser.ToList());
        }

        // GET: NonAdminUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NonAdminUser nonAdminUser = db.NonAdminUser.Find(id);
            if (nonAdminUser == null)
            {
                return HttpNotFound();
            }
            return View(nonAdminUser);
        }

        // GET: NonAdminUsers/Create
        public ActionResult Create()
        {
            ViewBag.AdministratorID = new SelectList(db.Administrator, "ID", "name");
            return View();
        }

        // POST: NonAdminUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,name,address,email,AdministratorID,creator,userShadow")] NonAdminUser nonAdminUser)
        {
            if (ModelState.IsValid)
            {
                db.NonAdminUser.Add(nonAdminUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdministratorID = new SelectList(db.Administrator, "ID", "name", nonAdminUser.AdministratorID);
            return View(nonAdminUser);
        }

        // GET: NonAdminUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NonAdminUser nonAdminUser = db.NonAdminUser.Find(id);
            if (nonAdminUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdministratorID = new SelectList(db.Administrator, "ID", "name", nonAdminUser.AdministratorID);
            return View(nonAdminUser);
        }

        // POST: NonAdminUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,name,address,email,AdministratorID,creator,userShadow")] NonAdminUser nonAdminUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nonAdminUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdministratorID = new SelectList(db.Administrator, "ID", "name", nonAdminUser.AdministratorID);
            return View(nonAdminUser);
        }

        // GET: NonAdminUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NonAdminUser nonAdminUser = db.NonAdminUser.Find(id);
            if (nonAdminUser == null)
            {
                return HttpNotFound();
            }
            return View(nonAdminUser);
        }

        // POST: NonAdminUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NonAdminUser nonAdminUser = db.NonAdminUser.Find(id);
            db.NonAdminUser.Remove(nonAdminUser);
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
