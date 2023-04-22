using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using giadinhthoxinh.Models;

namespace giadinhthoxinh.Areas.Admin.Controllers
{
    public class OrdersController : Controller
    {
        private giadinhthoxinhEntities db = new giadinhthoxinhEntities();

        // GET: Admin/Orders
        public ActionResult Index()
        {
            var tblOrders = db.tblOrders.Include(t => t.tblUser);
            return View(tblOrders.ToList());
        }

        // GET: Admin/Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblOrder tblOrder = db.tblOrders.Find(id);
            if (tblOrder == null)
            {
                return HttpNotFound();
            }
            return View(tblOrder);
        }

        // GET: Admin/Orders/Create
        public ActionResult Create()
        {
            ViewBag.FK_iAccountID = new SelectList(db.tblUsers, "PK_iAccountID", "sEmail");
            return View();
        }

        // POST: Admin/Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_iOrderID,FK_iAccountID,sCustomerName,sCustomerPhone,sDeliveryAddress,dInvoidDate,sBiller,iDeliveryMethod,fSurcharge,iPaid,iState")] tblOrder tblOrder)
        {
            if (ModelState.IsValid)
            {
                db.tblOrders.Add(tblOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_iAccountID = new SelectList(db.tblUsers, "PK_iAccountID", "sEmail", tblOrder.FK_iAccountID);
            return View(tblOrder);
        }

        // GET: Admin/Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblOrder tblOrder = db.tblOrders.Find(id);
            if (tblOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_iAccountID = new SelectList(db.tblUsers, "PK_iAccountID", "sEmail", tblOrder.FK_iAccountID);
            return View(tblOrder);
        }

        // POST: Admin/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_iOrderID,FK_iAccountID,sCustomerName,sCustomerPhone,sDeliveryAddress,dInvoidDate,sBiller,iDeliveryMethod,fSurcharge,iPaid,iState")] tblOrder tblOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_iAccountID = new SelectList(db.tblUsers, "PK_iAccountID", "sEmail", tblOrder.FK_iAccountID);
            return View(tblOrder);
        }

        // GET: Admin/Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblOrder tblOrder = db.tblOrders.Find(id);
            if (tblOrder == null)
            {
                return HttpNotFound();
            }
            return View(tblOrder);
        }

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblOrder tblOrder = db.tblOrders.Find(id);
            db.tblOrders.Remove(tblOrder);
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
