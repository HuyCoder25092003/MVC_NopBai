using NguyenDucHuy_0158.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace NguyenDucHuy_0158.Controllers
{
    public class CartController : Controller
    {
        NorthwindDataContext da = new NorthwindDataContext();
        // GET: CartModel
        public ActionResult Index()
        {
            return View();
        }
        List<CartModel> GetListCarts()
        {
            List<CartModel> carts = Session["CartModel"] as List<CartModel>;
            if(carts == null)
            {
                carts = new List<CartModel>();
                Session["CartModel"] = carts;
            }
            return carts;
        }
        public ActionResult ListCarts()
        {
            List<CartModel> carts = GetListCarts();
            ViewBag.CountProduct = carts.Sum(s => s.Quantity);
            ViewBag.Total = carts.Sum(s => s.Total);
            return View(carts);
        }
        public ActionResult AddToCart(int id)
        {
            List<CartModel> carts = GetListCarts();
            CartModel c = carts.Find(s => s.ProductId == id);
            if (c != null)
                c.Quantity++;
            else
            {
                c = new CartModel(id);
                carts.Add(c);
            }

            return RedirectToAction("ListCarts");
        }
        public ActionResult OrderProduct(FormCollection fCollection)
        {
            using (TransactionScope tranScope = new TransactionScope())
            {
                try
                {
                    Order order = new Order();
                    order.OrderDate = DateTime.Now;
                    da.Orders.InsertOnSubmit(order);
                    da.SubmitChanges();

                    List<CartModel> carts = GetListCarts();
                    foreach(var item in carts)
                    {
                        Order_Detail d = new Order_Detail();
                        d.OrderID = order.OrderID;
                        d.ProductID = item.ProductId;
                        d.Quantity = short.Parse(item.Quantity.ToString());
                        d.UnitPrice = (decimal)item.UnitPrice;
                        d.Discount = 0;
                        da.Order_Details.InsertOnSubmit(d);
                    }
                    da.SubmitChanges();

                    tranScope.Complete();
                    Session["Carts"] = null;
                }
                catch(Exception)
                {
                    tranScope.Dispose();
                    return RedirectToAction("ListCarts");
                }
            }
            return RedirectToAction("OrderDetailList");
        }
        public ActionResult OrderDetailList()
        {
            var p = da.Order_Details.OrderByDescending(s => s.OrderID).Select(s => s).ToList();
            return View(p);
        }
    }
}