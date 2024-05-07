using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NguyenDucHuy_0158.Controllers
{
    public class ProductController : Controller
    {
        NorthwindDataContext da = new NorthwindDataContext();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListProducts()
        {
            var ds = da.Products.OrderByDescending(s => s.ProductID).ToList();
            return View(ds);
        }
        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            Product p = da.Products.FirstOrDefault(s => s.ProductID == id);
            return View(p);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ViewData["NCC"] = new SelectList(da.Suppliers, "SupplierId", "CompanyName");
            ViewData["LSP"] = new SelectList(da.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, Product p)
        {
            try
            {
                Product product = new Product();
                product = p;
                product.SupplierID = int.Parse(collection["NCC"]);
                product.CategoryID = int.Parse(collection["LSP"]);
                // TODO: Add insert logic here
                da.Products.InsertOnSubmit(product);
                da.SubmitChanges();
                return RedirectToAction("ListProducts");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var p = da.Products.FirstOrDefault(s => s.ProductID == id);
            ViewData["NCC"] = new SelectList(da.Suppliers, "SupplierId", "CompanyName");
            ViewData["LSP"] = new SelectList(da.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                Product product = da.Products.FirstOrDefault(s => s.ProductID == id);
                product.ProductName = collection["ProductName"];
                product.UnitPrice = decimal.Parse(collection["UnitPrice"]);
                product.SupplierID = int.Parse(collection["NCC"]);
                product.CategoryID = int.Parse(collection["LSP"]);
                product.QuantityPerUnit = (collection["QuantityPerUnit"]).ToString();

                da.SubmitChanges();
                return RedirectToAction("ListProducts");


            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            var p = da.Products.FirstOrDefault(s => s.ProductID == id);
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Product product = da.Products.First(s => s.ProductID == id);
                da.Products.DeleteOnSubmit(product);
                da.SubmitChanges();
                return RedirectToAction("ListProducts");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        
    }
}
