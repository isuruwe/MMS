using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;

using MMS.Models;
using System.Web.Security;
using System.Data.Entity.Validation;
using PagedList;
using System.Data.SqlClient;

namespace MMS.Controllers
{
    public class UserPermissionsController : Controller
    {
        private MMSEntities db = new MMSEntities();
        SqlConnection oSqlConnection;
        SqlCommand oSqlCommand;
        SqlDataAdapter oSqlDataAdapter;
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
        string sqlQuery;
        public ActionResult UserPermissionIndex(int? page)
        {
            try
            {
                var onePageOfProducts = (dynamic)null;
                List<UserProfileModel> lstUserPrfModel = new List<UserProfileModel>();
                User us = GetCurrentUser();
                if (us != null)
                {
                    var obj = from up in db.UserPermissions
                              
                              orderby up.MenuItem.MenuID
                              select up;

                    foreach (var k in obj)
                    {
                        UserProfileModel upm = new UserProfileModel();
                        upm.UserProfID = k.UserProfID;
                        upm.UserID = k.UserID;
                        upm.LocationID = k.LocationID;
                        upm.DivisionID = k.DivisionID;
                        upm.RoleID = k.RoleID;
                        upm.SysID = k.SysID;
                        upm.MenuID = k.MenuID;
                        upm.MenuName = k.MenuItem.MenuName;
                        upm.SHO = k.SHO == 1 ? true : false;
                        upm.NEW = k.NEW == 1 ? true : false;
                        upm.EDT = k.EDT == 1 ? true : false;
                        upm.DEL = k.DEL == 1 ? true : false;
                        upm.PRN = k.PRN == 1 ? true : false;
                        upm.SER = k.SER == 1 ? true : false;
                        upm.CER = k.CER == 1 ? true : false;
                        upm.CreatedDate = DateTime.Now;
                        upm.CreatedUser = 1;
                        lstUserPrfModel.Add(upm);
                    }
                    var pageNumber = page ?? 1;
                    onePageOfProducts = lstUserPrfModel.ToPagedList(pageNumber, 10);
                    ViewBag.OnePageOfProducts = onePageOfProducts;
                    return View();
                }
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
            return HttpNotFound();
        }


        ///////////////////////////////////////////////////////////

        public ActionResult mCreate()
        {
            try
            {
                User us = GetCurrentUser();
                if (us != null)
                {
                    List<UserProfileModel> lstUserPermision = new List<UserProfileModel> { new UserProfileModel { UserProfID = 0, SysID = "", MenuID = 0, SHO = false, NEW = false, EDT =false,
                                                                                                    DEL=false,PRN=false,SER=false,CER=false,CreatedDate=new DateTime(),CreatedUser=0} };
                    var menuObj = from mm in db.RolePermissions
                                  where mm.RoleID == us.RoleID
                                  select mm.MenuItem;


                    ViewBag.MenuID = new SelectList(db.MenuItems, "MenuID", "MenuName");
                    ViewBag.userlist = new SelectList(db.Users, "UserID", "UserName");
                    return View(lstUserPermision);
                }
                return HttpNotFound();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return HttpNotFound();
            }
            catch (Exception Ex)
            {
                return View("Create");
            }
        }

        // POST: UserPermissions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult mCreate(List<UserProfileModel> lstUserPer)
        {
            try
            {

                User us = GetCurrentUser();
                // TODO: Add insert logic here
                if (us != null)
                {
                    var maxID = db.UserPermissions
                    .Max(p => p.UserProfID);

                    foreach (var i in lstUserPer)
                    {
                        UserPermission rpr = (from rr in db.UserPermissions
                                              join rts in db.Users on rr.UserID equals rts.UserID
                                              where rr.MenuID == i.MenuID
                                              && rts.UserName == i.LocationID
                                              select rr).FirstOrDefault();
                        var rpr2 = (from rr in db.Users 
                                              where rr.UserName == i.LocationID
                                              select rr);
                        if (rpr == null)
                        {
                            UserPermission upf = new UserPermission();
                            foreach (var sd in rpr2) {
                                upf.UserID = sd.UserID;
                            }
                           
                            upf.LocationID = i.LocationID;
                            upf.DivisionID = i.DivisionID;
                            upf.RoleID = "R001";
                            upf.SysID = "Test";
                            upf.MenuID = i.MenuID;
                            upf.SHO = i.SHO == true ? 1 : 0;
                            upf.NEW = i.NEW == true ? 1 : 0;
                            upf.EDT = i.EDT == true ? 1 : 0;
                            upf.DEL = i.DEL == true ? 1 : 0;
                            upf.PRN = i.PRN == true ? 1 : 0;
                            upf.SER = i.SER == true ? 1 : 0;
                            upf.CER = i.CER == true ? 1 : 0;
                            upf.CreatedDate = DateTime.Now;
                            upf.CreatedUser = 1;

                            //MenuItem mi = (from m in db.MenuItems
                            //               where m.MenuID == i.MenuID
                            //               select m).FirstOrDefault();

                            ////Check considered RolePermission's menu item,s parent menu item is exist
                            //if (mi.ParentMenuID > 0)
                            //{
                            //    UserProfileModel upm = (from ls in lstUserPer
                            //                            where ls.MenuID == mi.ParentMenuID
                            //                            select ls).FirstOrDefault();

                            //    UserPermission up = (from uu in db.UserPermissions
                            //                         where uu.MenuID == mi.ParentMenuID
                            //                         && uu.UserID == us.UserID
                            //                         select uu).FirstOrDefault();

                            //    if (upm == null && up == null)
                            //    {
                            //        MenuItem rpm = (from r in db.MenuItems
                            //                        where r.MenuID == mi.ParentMenuID
                            //                        select r).FirstOrDefault();

                            //        if (rpm != null)
                            //        {
                            //            UserPermission upp = new UserPermission();

                            //            upp.UserID = us.UserID;
                            //            upp.LocationID = i.LocationID;
                            //            upp.DivisionID = i.DivisionID;
                            //            upp.RoleID = i.RoleID;
                            //            upp.SysID = "Test";
                            //            upp.MenuID = i.MenuID;
                            //            upp.SHO = i.SHO == true ? 1 : 0;
                            //            upp.NEW = i.NEW == true ? 1 : 0;
                            //            upp.EDT = i.EDT == true ? 1 : 0;
                            //            upp.DEL = i.DEL == true ? 1 : 0;
                            //            upp.PRN = i.PRN == true ? 1 : 0;
                            //            upp.SER = i.SER == true ? 1 : 0;
                            //            upp.CER = i.CER == true ? 1 : 0;
                            //            upp.CreatedDate = DateTime.Now;
                            //            upp.CreatedUser = 1;
                            //            db.UserPermissions.Add(upp);
                            //        }
                            //    }
                            //}
                            maxID = maxID + 1;
                            upf.UserProfID = maxID;
                            db.UserPermissions.Add(upf);
                        }
                    }
                }
                db.SaveChanges();
                Session["permerror"] = "Saved!";
                return RedirectToAction("mCreate", "UserPermissions");
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return HttpNotFound();
            }
            catch (Exception Ex)
            {
                Session["permerror"] = "Error!";
                return View("Create");
            }
        }







        /// <summary>
        /// /////////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        private User GetCurrentUser()
        {
            User us = new User();
            int userID = 0;
            try
            {

                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    userID = Convert.ToInt32(FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name);
                }
                //userID = Int32.Parse(HttpContext.User.Identity.Name);

                us = (from u in db.Users
                      where u.UserID == userID
                      select u).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            return us;

        }

        public JsonResult getuserperlist(string id)
        {
            try
            {
                char[] MyChar = { '/', '"', ' ' };

                // string catid1 = catid.Trim(MyChar);
                string svc1 = id.Trim(MyChar);

                DataTable oDataSetv7 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "  SELECT a.*,b.Clinic_Detail FROM [MMS].[dbo].[Staff_Master]as a inner join [MMS].[dbo].[Clinic_Master] as b on a.LOCID=b.Clinic_ID where" +
  " ServiceNo = '" + svc1 + "' ";
               
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlCommand.CommandTimeout = 120;
                //   oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSetv7);
                // oSqlConnection.Close();

                var joined3 = oDataSetv7.AsEnumerable()
     .Select(dataRow => new getsickdata2
     {

         rt= dataRow.Field<int?>("sid"),
         service = dataRow.Field<string>("ServiceNo"),
         cat = dataRow.Field<string>("LocationID"),
         fname = dataRow.Field<string>("Clinic_Detail"),


     }).ToList();
                if (oDataSetv7.Rows.Count < 1)
                {
                    DataTable oDataSetv71 = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = "   SELECT a.*,b.DivisionName FROM [MMS].[dbo].[Staff_Master] as a inner join [dbo].[Vw_Formation]  as b on a.LOCID=b.DivisionID and a.LocationID=b.LocationID where " +
      " a.ServiceNo = '" + svc1 + "' ";

                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlCommand.CommandTimeout = 120;
                    //   oSqlConnection.Open();
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                    oSqlDataAdapter.Fill(oDataSetv71);
                    // oSqlConnection.Close();

                    joined3 = oDataSetv71.AsEnumerable()
        .Select(dataRow => new getsickdata2
        {

            rt = dataRow.Field<int?>("sid"),
            service = dataRow.Field<string>("ServiceNo"),
            cat = dataRow.Field<string>("LocationID"),
            fname = dataRow.Field<string>("DivisionName"),


        }).ToList();

                }



                return Json(joined3, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }


        }


        public JsonResult remdiv(string id)
        {
            try
            {
                char[] MyChar = { '/', '"', ' ' };

                // string catid1 = catid.Trim(MyChar);
                string svc1 = id.Trim(MyChar);

                DataTable oDataSetv7 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "  delete FROM [MMS].[dbo].[Staff_Master] where" +
  " sid = '" + svc1 + "' ";

                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlCommand.CommandTimeout = 120;
                //   oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSetv7);
                // oSqlConnection.Close();

              
                return Json("ok", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }


        }

        // GET: UserPermissions
        public async Task<ActionResult> Index()
        {
            var userPermissions = db.UserPermissions.Include(u => u.MenuItem).Include(u => u.User);
            return View(await userPermissions.ToListAsync());
        }

        // GET: UserPermissions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPermission userPermission = await db.UserPermissions.FindAsync(id);
            if (userPermission == null)
            {
                return HttpNotFound();
            }
            return View(userPermission);
        }

        // GET: UserPermissions/Create
        public ActionResult Create()
        {
            try
            {
                



                User us = GetCurrentUser();
                if (us != null)
                {
                    List<UserProfileModel> lstUserPermision = new List<UserProfileModel> { new UserProfileModel { UserProfID = 0, SysID = "", MenuID = 0, SHO = false, NEW = false, EDT =false,
                                                                                                    DEL=false,PRN=false,SER=false,CER=false,CreatedDate=new DateTime(),CreatedUser=0} };
                    var menuObj = from mm in db.RolePermissions
                                  where mm.RoleID == us.RoleID
                                  select mm.MenuItem;


                    ViewBag.MenuID = new SelectList(db.MenuItems.Where(a=>a.ParentMenuID==0), "MenuID", "MenuName");
                    ViewBag.userlist = new SelectList(db.Users, "UserID", "UserName");
                    return View(lstUserPermision);
                }
                return HttpNotFound();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return HttpNotFound();
            }
            catch (Exception Ex)
            {
                return View("Create");
            }
        }

        // POST: UserPermissions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(List<UserProfileModel> lstUserPer)
        {
            try
            {
                


                User us = GetCurrentUser();
                // TODO: Add insert logic here
                if (us != null)
                {
                    var maxID = db.UserPermissions
                    .Max(p => p.UserProfID);

                    foreach (var i in lstUserPer)
                    {
                        UserPermission rpr1 = (from rr in db.UserPermissions
                                              where rr.MenuID == i.MenuID
                                              && rr.UserID == i.UserID
                                              select rr).FirstOrDefault();
                        if (rpr1 == null)
                        {

                            UserPermission upf = new UserPermission();

                            upf.UserID = i.UserID;
                            upf.LocationID = i.LocationID;
                            upf.DivisionID = i.DivisionID;
                            upf.RoleID = "R001";
                            upf.SysID = "Test";
                            upf.MenuID = i.MenuID;
                            upf.SHO = i.SHO == true ? 1 : 0;
                            upf.NEW = i.NEW == true ? 1 : 0;
                            upf.EDT = i.EDT == true ? 1 : 0;
                            upf.DEL = i.DEL == true ? 1 : 0;
                            upf.PRN = i.PRN == true ? 1 : 0;
                            upf.SER = i.SER == true ? 1 : 0;
                            upf.CER = i.CER == true ? 1 : 0;
                            upf.CreatedDate = DateTime.Now;
                            upf.CreatedUser = 1;

                            
                            maxID = maxID + 1;
                            upf.UserProfID = maxID;
                            db.UserPermissions.Add(upf);
                        }

                        var mi = (from rr in db.MenuItems
                                       where rr.ParentMenuID == i.MenuID
                                             
                                              select rr).ToList();
                        foreach (var ti in mi) {

                            UserPermission rpr = (from rr in db.UserPermissions
                                                  where rr.MenuID == ti.MenuID
                                                  && rr.UserID == i.UserID
                                                  select rr).FirstOrDefault();
                            if (rpr == null)
                            {

                                UserPermission upf = new UserPermission();

                                upf.UserID = i.UserID;
                                upf.LocationID = i.LocationID;
                                upf.DivisionID = i.DivisionID;
                                upf.RoleID = "R001";
                                upf.SysID = "Test";
                                upf.MenuID = ti.MenuID;
                                upf.SHO = i.SHO == true ? 1 : 0;
                                upf.NEW = i.NEW == true ? 1 : 0;
                                upf.EDT = i.EDT == true ? 1 : 0;
                                upf.DEL = i.DEL == true ? 1 : 0;
                                upf.PRN = i.PRN == true ? 1 : 0;
                                upf.SER = i.SER == true ? 1 : 0;
                                upf.CER = i.CER == true ? 1 : 0;
                                upf.CreatedDate = DateTime.Now;
                                upf.CreatedUser = 1;

                                maxID = maxID + 1;
                                upf.UserProfID = maxID;
                                db.UserPermissions.Add(upf);
                            }
                        }
                    }
                }
                db.SaveChanges();
                Session["permerror"] = "Saved!";
                return RedirectToAction("Create", "UserPermissions");
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return HttpNotFound();
            }
            catch (Exception Ex)
            {
                Session["permerror"] = "Error!";
                return View("Create");
            }
        }

        // GET: UserPermissions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPermission userPermission = await db.UserPermissions.FindAsync(id);
            if (userPermission == null)
            {
                return HttpNotFound();
            }
            ViewBag.MenuID = new SelectList(db.MenuItems, "MenuID", "MenuName", userPermission.MenuID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "RoleID", userPermission.UserID);
            return View(userPermission);
        }

        // POST: UserPermissions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserProfID,UserID,SysID,MenuID,LocationID,DivisionID,RoleID,SHO,NEW,EDT,DEL,PRN,SER,CER,CreatedDate,CreatedUser")] UserPermission userPermission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userPermission).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MenuID = new SelectList(db.MenuItems, "MenuID", "MenuName", userPermission.MenuID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "RoleID", userPermission.UserID);
            return View(userPermission);
        }

        // GET: UserPermissions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPermission userPermission = await db.UserPermissions.FindAsync(id);
            if (userPermission == null)
            {
                return HttpNotFound();
            }
            return View(userPermission);
        }

        // POST: UserPermissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            UserPermission userPermission = await db.UserPermissions.FindAsync(id);
            db.UserPermissions.Remove(userPermission);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
