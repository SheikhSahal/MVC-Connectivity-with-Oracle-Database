using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private OracleEntities db = new OracleEntities();

        // GET: Home
        public ActionResult Index()
        {
            var eMPs = db.EMPs.Include(e => e.EMP2);
            return View(eMPs.ToList());
        }

        // GET: Home/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMP eMP = db.EMPs.Find(id);
            if (eMP == null)
            {
                return HttpNotFound();
            }
            return View(eMP);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            ViewBag.MGR = new SelectList(db.EMPs, "EMPNO", "ENAME");
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EMPNO,ENAME,JOB,MGR,HIREDATE,SAL,COMM,DEPTNO")] EMP eMP)
        {
            if (ModelState.IsValid)
            {
                db.EMPs.Add(eMP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MGR = new SelectList(db.EMPs, "EMPNO", "ENAME", eMP.MGR);
            return View(eMP);
        }

        // GET: Home/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMP eMP = db.EMPs.Find(id);
            if (eMP == null)
            {
                return HttpNotFound();
            }
            ViewBag.MGR = new SelectList(db.EMPs, "EMPNO", "ENAME", eMP.MGR);
            return View(eMP);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EMPNO,ENAME,JOB,MGR,HIREDATE,SAL,COMM,DEPTNO")] EMP eMP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eMP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MGR = new SelectList(db.EMPs, "EMPNO", "ENAME", eMP.MGR);
            return View(eMP);
        }

        // GET: Home/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMP eMP = db.EMPs.Find(id);
            if (eMP == null)
            {
                return HttpNotFound();
            }
            return View(eMP);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            EMP eMP = db.EMPs.Find(id);
            db.EMPs.Remove(eMP);
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
