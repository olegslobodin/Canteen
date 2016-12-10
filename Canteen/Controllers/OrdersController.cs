using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Canteen.Models;
using Canteen.ViewModels;
using Microsoft.AspNet.Identity;

namespace Canteen.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private CanteenEntities db = new CanteenEntities();

        // GET: Orders
        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();

            var orders = User.IsInRole("Admin") ?
                db.Orders.Include(o => o.AspNetUser) :
                db.Orders.Where(o => o.UserId == currentUserId);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(long? id)
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

        public JsonResult GetOrderPrice(List<long> ids)
        {
            var price = db.Dishes.Where(d => ids.Contains(d.Id)).Sum(d => d.Price);
            return Json(new { price });
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.Dishes = new MultiSelectList(db.Dishes.Select(dish => new { Value = dish.Id, Text = dish.Title }), "Value", "Text");
            ViewBag.Budget = db.AspNetUsers.Find(User.Identity.GetUserId()).Budget ?? 0;
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,Date,Price,DishIds")] OrderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    UserId = orderViewModel.UserId,
                    Date = orderViewModel.Date ?? DateTime.Now,
                    Price = User.IsInRole("Admin") && orderViewModel.Price.HasValue ?
                        orderViewModel.Price.Value :
                        (decimal)db.Dishes.Where(dish => orderViewModel.DishIds.Contains(dish.Id)).Sum(x => x.Price)
                };

                var user = db.AspNetUsers.Find(order.UserId);

                if (!User.IsInRole("Admin") && (user.Budget ?? 0) < order.Price)
                {
                    ViewBag.NotEnoughMoney = true;
                }
                else
                {
                    db.Orders.Add(order);
                    db.SaveChanges();
                    foreach (var dishId in orderViewModel.DishIds)
                    {
                        db.Orders_Dishes.Add(new Orders_Dishes { Dish_Id = dishId, Order_Id = order.id });
                    }
                    user.Budget -= order.Price;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", orderViewModel.UserId);
            ViewBag.Dishes = new MultiSelectList(db.Dishes.Select(dish => new
            {
                Value = dish.Id,
                Text = dish.Title
            }), "Value", "Text");
            ViewBag.Budget = db.AspNetUsers.Find(User.Identity.GetUserId()).Budget ?? 0;
            return View(orderViewModel);
        }

        // GET: Orders/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(long? id)
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
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", order.UserId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "id,UserId,Date,Price")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", order.UserId);
            return View(order);
        }

        // GET: Orders/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(long? id)
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
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(long id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
