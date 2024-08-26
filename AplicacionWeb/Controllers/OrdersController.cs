using System;
using System.Linq;
using System.Web.Mvc;
using AplicacionWebNomina.Data;
using AplicacionWebNomina.Models;

public class OrderController : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: Order/Create
    public ActionResult Create()
    {
        ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FirstName");
        ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "StoreName");
        ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName");
        return View();
    }

    // POST: Order/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(OrderViewModel model)
    {
        if (ModelState.IsValid)
        {
            var order = new Order
            {
                CustomerId = model.CustomerId,
                StoreId = model.StoreId,
                StaffId = model.StaffId,
                OrderDate = DateTime.Now,
                OrderStatus = 1 // Assuming 1 is the status for a new order
            };

            foreach (var item in model.OrderItems)
            {
                var product = db.Products.Find(item.ProductId);
                var stock = db.Stocks.SingleOrDefault(s => s.StoreId == model.StoreId && s.ProductId == item.ProductId);
                if (product == null || stock == null || stock.Quantity < item.Quantity)
                {
                    ModelState.AddModelError("", $"No hay stock suficiente para el producto {item.ProductId}");
                    ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FirstName", model.CustomerId);
                    ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "StoreName", model.StoreId);
                    ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName", model.StaffId);
                    return View(model);
                }

                order.OrderItem.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    ListPrice = product.List_Price,
                    Discount = item.Discount
                });

                stock.Quantity -= item.Quantity;
            }

            db.Orders.Add(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FirstName", model.CustomerId);
        ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "StoreName", model.StoreId);
        ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName", model.StaffId);
        return View(model);
    }

    // GET: Order/Cancel
    public ActionResult Cancel()
    {
        return RedirectToAction("Create");
    }
}
