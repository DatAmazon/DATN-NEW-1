using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace giadinhthoxinh.Models
{
    public class ProductInCart
    {
        public int ProductID { get; set; }
        public int Quatity { get; set; }
        public ProductInCart(int productid, int quatity)
        {
            this.ProductID = productid;
            this.Quatity = quatity;
        }
        public ProductInCart() { }
    }
    




}