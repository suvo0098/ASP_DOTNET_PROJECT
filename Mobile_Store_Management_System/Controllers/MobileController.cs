using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mobile_Store_Management_System.Models;
using Mobile_DAO;
using System.Runtime.InteropServices;

namespace Mobile_Store_Management_System.Controllers
{
    public class MobileController : Controller
    {
        private MSM_DBContext db = new MSM_DBContext();


       
        public ActionResult Index()
        {
            int id = (int) Session["user_id"];
            return View(CLS_Mobile.ViewMobileById(id));
        }

        public ActionResult products()
        {
            //int id = (int)Session["user_id"];
            return View(CLS_Mobile.ViewMobile());
        }


        [HttpPost]
        public ActionResult products([Bind(Include = "mobileId,price,stock_quantity")] mobile_details mobiles)
        {
            if (mobiles.stock_quantity < 1)
            {
                return View(CLS_Mobile.ViewMobile());

            }

            order_details ord = new order_details();

            ord.user_id = (int)Session["user_id"];

            ord.mobileId = mobiles.mobileId;

            ord.quantity = 1;

            ord.total = mobiles.price;

            int orid = CLS_Order.AddOrder(ord);

            TempData["orderId"] = orid;

            return RedirectToAction("Details", "order");

        }

        

        
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var mobileModel = CLS_Mobile.FetchMobile(id);
        //    if (mobileModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(mobileModel);
        //}


        //[HttpPost]
        //public ActionResult Details([Bind(Include = "mobileId,price,stock_quantity,orderquantity")] mobile_details gMSProduct)
        //{
        //    //here i do order confirmation operation here i need to to decrease stoke after each product and if stock is less than 1 then
        //    //   not order will be performed
        //    if ((int)gMSProduct.order_quantity > gMSProduct.stock_quantity)
        //    {
        //        mobile_details prod = CLS_Mobile.FetchMobile(gMSProduct.mobileId);
        //        return View(gMSProduct);
        //    }
        //    //this below method decrease the quantity of product in database after order
        //    //clsProduct.UpdateProductQuantity(gMSProduct.productId,(int)gMSProduct.orderQuantity);
        //    order_details ord = new order_details();
        //    ord.user_id = (int)Session["user_id"];
        //    ord.mobileId= gMSProduct.mobileId;
        //    ord.quantity = (int)gMSProduct.stock_quantity;
        //    var totalValue = (int)gMSProduct.stock_quantity * gMSProduct.price;
        //    ord.total = totalValue;
        //    int orid = CLS_Order.AddOrder(ord);

        //    TempData["orderId"] = orid;
        //    return RedirectToAction("Details", "Order");
        //}

        public ActionResult Create()
        {
            //ViewBag.user_id = new SelectList(CLS_Mobile.ViewSeller(), "user_id", "first_name");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "mobileId,mobileName,description,price,stock_quantity")] mobile_details mobileModel)
        {
            mobileModel.user_id = (int)Session["user_id"];
            mobileModel.image_name = "./NoImage";
            if (ModelState.IsValid)
            {
                CLS_Mobile.AddMobile(mobileModel);
                return RedirectToAction("Index");
            }

           // ViewBag.user_id = new SelectList(CLS_Mobile.ViewSeller(), "user_id", "first_name", mobileModel.user_id);
            return View(mobileModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var mobileModel = CLS_Mobile.FetchMobile(id);
            if (mobileModel == null)
            {
                return HttpNotFound();
            }
            //ViewBag.user_id = new SelectList(CLS_Mobile.ViewSeller(), "user_id", "first_name", mobileModel.user_id);
            return View(mobileModel);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mobileId,user_id,mobileName,description,price,stock_quantity,image_name")] mobile_details mobileModel)
        {
            if (ModelState.IsValid)
            {
                CLS_Mobile.UpdateMobile(mobileModel.mobileId,mobileModel.mobileName, mobileModel.description, mobileModel.price, mobileModel.stock_quantity);
                return RedirectToAction("Index");
            }
            //ViewBag.user_id = new SelectList(CLS_Mobile.ViewSeller(), "user_id", "first_name", mobileModel.user_id);
            return View(mobileModel);
        }

        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var mobileModel = CLS_Mobile.FetchMobile(id);
            if (mobileModel == null)
            {
                return HttpNotFound();
            }
            return View(mobileModel);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CLS_Mobile.DeleteMobile(id);
            return RedirectToAction("Index");
        }


    }
}
