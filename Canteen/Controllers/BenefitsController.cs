using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Canteen.Models;

namespace Canteen.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BenefitsController : Controller
    {
        private CanteenEntities db = new CanteenEntities();

        // GET: Benefits
        public ActionResult Index()
        {
            try
            {
                return View(db.Benefits.ToList());
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Benefits/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Benefit benefit = db.Benefits.Find(id);
            if (benefit == null)
            {
                return HttpNotFound();
            }
            return View(benefit);
        }

        // GET: Benefits/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Benefits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Discount")] Benefit benefit)
        {
            if (ModelState.IsValid)
            {
                db.Benefits.Add(benefit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(benefit);
        }

        // GET: Benefits/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Benefit benefit = db.Benefits.Find(id);
            if (benefit == null)
            {
                return HttpNotFound();
            }
            return View(benefit);
        }

        // POST: Benefits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Discount")] Benefit benefit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(benefit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(benefit);
        }

        // GET: Benefits/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Benefit benefit = db.Benefits.Find(id);
            if (benefit == null)
            {
                return HttpNotFound();
            }
            return View(benefit);
        }

        // POST: Benefits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Benefit benefit = db.Benefits.Find(id);
            db.Benefits.Remove(benefit);
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
