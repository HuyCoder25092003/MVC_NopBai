using System;
using System.Collections.Generic;
using System.Linq;

namespace NguyenDucHuy_0158.Models
{
    public class CartModel
    {
        NorthwindDataContext da = new NorthwindDataContext();
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal? Total { get { return UnitPrice * Quantity; } }
        
        public CartModel(int id)
        {
            Product p = da.Products.FirstOrDefault(s => s.ProductID == id);
            ProductId = p.ProductID;
            ProductName = p.ProductName;
            UnitPrice = p.UnitPrice;
            Quantity = 1;
        }
    }
}