using giadinhthoxinh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace giadinhthoxinh.Controllers
{
    public class CartController : Controller
    {
        private static giadinhthoxinhEntities db = new giadinhthoxinhEntities();
        private  List<tblProduct> listProduct = db.tblProducts.ToList();
        // GET: Cart
        public ActionResult Cart()
        {
            Cart giohang = (Cart)Session["giohang"];
            var listitem = giohang.lstproduct;
            ViewBag.list = listitem;
            return View();
        }
        public ActionResult Checkout()
        {
            return View();
        }
        /*
         * Thanh toán 
         * giohang.lstproduct.Clear();
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout([Bind(Include = "sCustomerName,sCustomerPhone,sDeliveryAddress,iDeliveryMethod")] tblOrder tblOrder)
        {
            //string name = tblOrder.sCustomerName;
            //string phone = tblOrder.sCustomerPhone;
            //int method = (int)tblOrder.iDeliveryMethod;
            //string biller = tblOrder.sBiller;
            Cart giohang = (Cart)Session["giohang"];
            if (giohang.lstproduct != null && giohang.lstproduct.Count > 0)
            {
                tblOrder.FK_iAccountID = (int)Session["idUser"];
                tblOrder.dInvoidDate = DateTime.Now;
                tblOrder.sBiller = "Linh";
                tblOrder.fSurcharge = 0;
                //int Paid = (int)tblOrder.iPaid;
                tblOrder.iPaid = 0;
                tblOrder.iState = 0;
                //PK_iOrderID,FK_iAccountID,dInvoidDate,sBiller,fSurcharge,iPaid,iState
                db.tblOrders.Add(tblOrder);
                db.SaveChanges();
                int orderid = tblOrder.PK_iOrderID;
                //Xử lý thêm vào CheckoutDetail
                foreach(var item in giohang.lstproduct)
                {
                    double price = getProductPrice(item.ProductID);
                    tblCheckoutDetail ChiTiet = new tblCheckoutDetail();
                    ChiTiet.fPrice = price;
                    ChiTiet.FK_iOrderID = orderid;
                    ChiTiet.FK_iProductID = item.ProductID;
                    ChiTiet.iQuantity = item.Quatity;
                    db.tblCheckoutDetails.Add(ChiTiet);
                }
                db.SaveChanges();
                giohang.lstproduct.Clear();
            }    
            else
            { }    
            return View();
        }
        private double getProductPrice(int productid)
        {
            double price = 0;
            foreach(var item in listProduct)
            {
                if (item.PK_iProductID == productid)
                {
                    price = (double)item.fPrice;
                    break;
                }
            }
            return price;
        }
    }
}