using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using giadinhthoxinh.Models;
using PagedList;

namespace giadinhthoxinh.Controllers
{
    public class HomeController : Controller
    {
        public giadinhthoxinhEntities db = new giadinhthoxinhEntities();
        public ActionResult Index(int? page)
        {
            int productInPage = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            giadinhthoxinhEntities db = new giadinhthoxinhEntities();
            List<tblProduct> ketQua = db.tblProducts.ToList();
            IPagedList<tblProduct> ketQuaFinal = null;
            ketQuaFinal = ketQua.ToPagedList(pageNumber, productInPage);
           // var ketQua = db.tblProducts.ToList();
           //  PagedList<tblProduct> ketQuaFinal = new PagedList<tblProduct>(ketQua, pageNumber, productInPage);
            return View(ketQuaFinal);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ProductDetail(int id)
        {
            var item = db.tblProducts.Find(id);

            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductDetail(int product_id, int quantity_value)
        {
            //xu ly them vao gio
                Cart giohang = (Cart)Session["giohang"];
                ProductInCart sanpham = new ProductInCart();
                sanpham.ProductID = product_id;
                sanpham.Quatity = quantity_value;
                int check = 0;
                if (giohang.lstproduct != null && giohang.lstproduct.Count > 0)
                    foreach (ProductInCart a in giohang.lstproduct)
                    {
                        if (a.ProductID == sanpham.ProductID)
                        {
                            a.Quatity++;
                            check = 1;
                            break;
                        }
                    }
                if (check == 0)
                {
                    giohang.lstproduct.Add(sanpham);
                }


            return RedirectToAction("ProductDetail", new { id = product_id });
        //    var item = db.tblProducts.Find(product_id);
         //   return View(item);
        }
        public ActionResult Checkout()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Promote()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}