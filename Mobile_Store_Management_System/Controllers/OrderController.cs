using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mobile_DAO;
using Mobile_Store_Management_System.Models;

namespace Mobile_Store_Management_System.Controllers
{
    public class OrderController : Controller
    {
        private MSM_DBContext db = new MSM_DBContext();

        public ActionResult Index()
        {
            return View(CLS_Order.ViewOrder());
        }

        /*// GET: EmployeeModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var OrderModel = CLS_Order.FetchOrder(id);
            if (OrderModel == null)
            {
                return HttpNotFound();
            }
            return View(OrderModel);
        }*/

        // GET: EmployeeModels/Create
        public ActionResult Create()
        {
            ViewBag.user_id = new SelectList(CLS_Mobile.ViewUser(), "user_id", "user_name");
            ViewBag.mobileId = new SelectList(CLS_Mobile.ViewMobile(), "mobileId", "mobileName");
            return View();
        }

        public ActionResult Details()
        {
            int id = (int)TempData["orderId"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order_details OrderData = CLS_Order.FetchOrder(id);
            if (OrderData == null)
            {
                return HttpNotFound();
            }
            return View(OrderData);
        }

        // POST: EmployeeModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "order_id,user_id,product_id,quantity,total")] order_details orderModel)
        {
            if (ModelState.IsValid)
            {
                CLS_Order.AddOrder(orderModel);
                return RedirectToAction("Index");
            }

            //ViewBag.user_id = new SelectList(cls_product.ViewUser(), "user_id", "user_name");
            //ViewBag.product_id = new SelectList(cls_product.ViewUser(), "product_id", "product_name");
            return View(orderModel);
        }


        // GET: EmployeeModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var orderModel = CLS_Order.FetchOrder(id);
            if (orderModel == null)
            {
                return HttpNotFound();
            }
            return View(orderModel);
        }

        // POST: EmployeeModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CLS_Order.DeleteOrder(id);
            return RedirectToAction("Index");
        }
    }
}
