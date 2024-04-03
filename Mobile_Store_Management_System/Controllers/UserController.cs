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
    public class UserController : Controller
    {
        private MSM_DBContext db = new MSM_DBContext();

        // GET: EmployeeModels
        public ActionResult Index()
        {
            return View(CLS_User.ViewUser());
        }

        //GET: GMSUsers/Home
        public ActionResult Home()
        {
            return View();
        }

        

        // GET: EmployeeModels/Details/5
        /*public ActionResult Details(int? id)
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
        }*/

        // GET: EmployeeModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "user_id,user_name,password,user_type,first_name,last_name,contactNumber,email")] user_details userModel)
        {
            if (ModelState.IsValid)
            {
                CLS_User.AddUser(userModel);
                return RedirectToAction("Login");
            }
            return View(userModel);
        }

        /*// GET: EmployeeModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userModel = CLS_User.FetchUser(id);
            if (userModel == null)
            {
                return HttpNotFound();
            }
            return View(userModel);
        }*/

        // POST: EmployeeModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "user_id,user_name,password,user_type,first_name,last_name,contactNumber,email")] user_details userModel)
        {
            if (ModelState.IsValid)
            {
                CLS_User.UpdateUser(userModel.user_id, userModel.user_name, userModel.password, userModel.user_type, userModel.first_name, userModel.last_name, userModel.contactNumber, userModel.email);
                return RedirectToAction("Index");
            }
            return View(userModel);
        }*/

        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include ="user_name,password")] user_details userModel)
        {
            if(ModelState.IsValid)
            {
                if(CLS_User.AuthenticateUser(userModel.user_name,userModel.password))
                {
                    var userDetails = CLS_User.FetchUserByName(userModel.user_name);
                    Session["user_id"] = userDetails.user_id;
                    Session["user_name"] = userDetails.user_name;
                    Session["user_type"] = userDetails.user_type;

                    return RedirectToAction("products", "Mobile");
                }
            }
            return View("Login");

        }

        public ActionResult LogOut()
        {
            Session["user_id"] = null;
            Session["user_name"] = null;
            Session["user_type"] = null;
            //return View("LogedIn");
            return RedirectToAction("Login");
        }

    }
}
