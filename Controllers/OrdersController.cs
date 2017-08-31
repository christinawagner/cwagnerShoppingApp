using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using cwagnerShoppingApp.Models;
using cwagnerShoppingApp.Models.CodeFirst;
using Microsoft.AspNet.Identity;

namespace cwagnerShoppingApp.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            var id = User.Identity.GetUserId();
            var user = db.Users.Find(id);
            return View(user.Orders.OrderByDescending(o => o.OrderDate).ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Address,City,State,ZipCode,Country,Phone,Total,OrderDate,CustomerId,OrderDetails")] Order order)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var cartItems = db.CartItems.Where(c => c.CustomerId == userId).ToList();
                order.OrderItems = cartItems.Select(o => new OrderItem
                {
                    UnitPrice = o.Item.Price,
                    Quantity = o.Count,
                    ItemId = o.ItemId
                }).ToList();

                order.OrderDate = DateTime.Now;
                order.CustomerId = userId;
                order.Total = order.OrderItems.Sum(t => t.Quantity * t.UnitPrice);

                db.Orders.Add(order);
                db.SaveChanges();
                foreach (var item in cartItems)
                {
                    db.CartItems.Remove(item);
                }
                db.SaveChanges();
                return RedirectToAction("Details", "Orders", new { id = order.Id });
            }

            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Address,City,State,ZipCode,Country,Phone,Total,OrderDate,CustomerId,OrderDetails")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult OrderConfirm(bool Completed, int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }
            Order order = db.Orders.Find(id);
            if(order != null)
            {
                order.Completed = true;
                db.SaveChanges();
             }
            return View(order);
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
