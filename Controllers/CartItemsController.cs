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
    public class CartItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult CartMenu()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var user = db.Users.Find(userId);
                var itemCount = user.CartItems.Sum(y => y.Count);
                return PartialView("_CartMenu", itemCount);
            }
            return PartialView("_CartMenu", null);
        }

        // GET: CartItems
        [Authorize]
        public ActionResult Index()
        {
            var id = User.Identity.GetUserId();
            var user = db.Users.Find(id);
            return View(user.CartItems.ToList());
        }

        // GET: CartItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        // GET: CartItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CartItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? itemId)
        {
            var user = db.Users.Find(User.Identity.GetUserId());

            if (itemId != null || user != null)
            {                
                if (db.CartItems.Where(i => i.CustomerId == user.Id).Any(i => i.ItemId == itemId.Value))
                {
                    var existingCartItem = db.CartItems.Where(i => i.CustomerId == user.Id).FirstOrDefault(i => i.ItemId == itemId.Value);
                    existingCartItem.Count++;
                }
                else
                {
                    CartItem cartItem = new CartItem();
                    cartItem.ItemId = (int)itemId;
                    cartItem.CustomerId = user.Id;
                    cartItem.Count = 1;
                    cartItem.CreationDate = DateTime.Now;
                    db.CartItems.Add(cartItem);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: CartItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        // POST: CartItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ItemId,Count,CreationDate,CustomerId")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cartItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cartItem);
        }

        // GET: CartItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        // POST: CartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CartItem cartItem = db.CartItems.Find(id);
            db.CartItems.Remove(cartItem);
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
