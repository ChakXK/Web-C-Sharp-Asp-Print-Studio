using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using print_studio.Models;

namespace print_studio.Controllers
{
    [Authorize]
    public class AdminPrintOrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PrintOrders
        public ActionResult Index(int? idstatus)
        {

            var statuses = db.OrderStatus;
            ViewBag.statuses = statuses.ToList();
            var printOrders = db.PrintOrders.Include(p => p.Employee).Include(p => p.OrderStatu).Include(p => p.SavedProduct).Include(p => p.Size);
            if(idstatus!=null)
            {
                printOrders = printOrders.Where(o => o.id_status == idstatus);
            }
            return View(printOrders.ToList());
        }

        // GET: PrintOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrintOrder printOrder = db.PrintOrders.Find(id);
            if (printOrder == null)
            {
                return HttpNotFound();
            }
            return View(printOrder);
        }

        // GET: PrintOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrintOrder printOrder = db.PrintOrders.Find(id);
            if (printOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_employee = new SelectList(db.Users, "Id", "Email", printOrder.id_employee);
            ViewBag.id_status = new SelectList(db.OrderStatus, "id", "name", printOrder.id_status);
            ViewBag.id_savedproduct = new SelectList(db.SavedProducts, "id", "id", printOrder.id_savedproduct);
            ViewBag.id_size = new SelectList(db.Sizes, "id", "name", printOrder.id_size);
            return View(printOrder);
        }

        // POST: PrintOrders/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,date,surname,name,adress,phone,id_size,id_status,id_employee,id_savedproduct,count")] PrintOrder printOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(printOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_employee = new SelectList(db.Users, "Id", "Email", printOrder.id_employee);
            ViewBag.id_status = new SelectList(db.OrderStatus, "id", "name", printOrder.id_status);
            ViewBag.id_savedproduct = new SelectList(db.SavedProducts, "id", "id", printOrder.id_savedproduct);
            ViewBag.id_size = new SelectList(db.Sizes, "id", "name", printOrder.id_size);
            return View(printOrder);
        }

        // GET: PrintOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrintOrder printOrder = db.PrintOrders.Find(id);
            if (printOrder == null)
            {
                return HttpNotFound();
            }
            return View(printOrder);
        }

        // POST: PrintOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PrintOrder printOrder = db.PrintOrders.Find(id);
            db.PrintOrders.Remove(printOrder);
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
