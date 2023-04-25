using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using PagedList;

using MMS.Models;

using System.Web;
using Newtonsoft.Json.Linq;
using System.Data.Entity.Migrations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MMS.Controllers
{   [AllowAnonymous]
    public class UsersController : Controller
    {
        private MMSEntities db = new MMSEntities();
        private object err;
        SqlConnection oSqlConnection;
        SqlCommand oSqlCommand;
        SqlDataAdapter oSqlDataAdapter;
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
        string sqlQuery;
        // GET: Users
        public async Task<ActionResult> Index(int? page, string id, string currentFilter)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            char[] MyChar = { '/', '"', ' ' };
            if (id != null)
            {
                page = 1;
                ViewBag.currentFilter = id;
            }
            else
            {
                id = currentFilter;
                ViewBag.currentFilter = id;
            }

            if (!String.IsNullOrEmpty(id))
            {
                id = id.Trim(MyChar);
            }
            if (!String.IsNullOrEmpty(id))
            {
                var users = db.Users.OrderBy(u => u.UserID).Where(u => u.UserName.ToLower().Contains(id.ToLower()));

                var pageNumber = page ?? 1;
                var onePageOfProducts = users.ToPagedList(pageNumber, 10);

                ViewBag.OnePageOfProducts = onePageOfProducts;
                return View(await users.ToListAsync());
            }
            else
            {
                var users = db.Users.OrderBy(u => u.UserID);

                var pageNumber = page ?? 1;
                var onePageOfProducts = users.ToPagedList(pageNumber, 10);

                ViewBag.OnePageOfProducts = onePageOfProducts;
                return View(await users.ToListAsync());
            }
            
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName");
            ViewBag.UserID = new SelectList(db.UserProfiles, "UserID", "LocationID");
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName");
            ViewBag.RANK1 = new SelectList(db.ranks, "RANK1", "LONG_NAME");
            ViewBag.ClinicTypeID = new SelectList(db.Clinic_Type, "ClinicTypeID", "ClinicDetails");
            return View();
        }
        public class menureader
        {

            public string tmenuid { get; set; }
            public string tsmenuid { get; set; }
            public string tmenu { get; set; }
            public string tsmenu { get; set; }
        }

        public JsonResult Saveuser(string ServiceNo, string fname, string lname, string pass, string LocationID, string DivisionID, string ClinicTypeID, string userItems)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            char[] MyChar = { '/', '"', ' ' };
            ServiceNo = ServiceNo.Trim(MyChar);
            fname = fname.Trim(MyChar);
            lname = lname.Trim(MyChar);
            pass = pass.Trim(MyChar);
            LocationID = LocationID.Trim(MyChar);
            DivisionID = DivisionID.Trim(MyChar);
            ClinicTypeID = ClinicTypeID.Trim(MyChar);
            userItems = userItems.Trim(MyChar);
            IndexGeneration indi = new IndexGeneration();
            //}
         string   opdid = (String)Session["userlocid1"];
            try
                {
                DataTable oDataSet = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = " select UserName from [dbo].[Users] with (nolock) where UserName='" + ServiceNo + "' ";
                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlConnection.Open();
                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSet);
                oSqlConnection.Close();
                if (oDataSet.Rows.Count > 0)
                {
                    err = "Username already registered in system";
                }
                else
                {



                    int userID = 0;

                    if (Session["UserID"] != null)
                    {
                        userID = (int)Session["UserID"];
                    }
                    var maxID = db.Users
                    .Max(p => p.UserID);
                    var maxID1 = db.UserPermissions
                      .Max(p => p.UserProfID);

                    byte[] encPwd = EncriptPassword(pass);
                    User user1 = new User();
                    user1.Pass = encPwd;
                    user1.LocationID = LocationID;

                    user1.UserID = maxID + 1;
                    user1.CreatedDate = System.DateTime.Now;
                    user1.RoleID = "R001";
                    user1.UserName = ServiceNo;
                    user1.ServiceNo = ServiceNo;
                    string locid = "";
                    //int clid = Convert.ToInt32(DivisionID);
                    //var locidselect = from s in db.Clinic_Master

                    //                  where s.LocationID == user.LocationID
                    //                  where s.ClinicTypeID == clid
                    //                  select new { s.Clinic_ID };

                    //foreach (var item in locidselect)
                    //{
                    //    locid = item.Clinic_ID;
                    //}

                    Staff_Master oStaff_Master = new Staff_Master();
                    oStaff_Master.LocationID = LocationID;
                    oStaff_Master.CreatedDate = System.DateTime.Now;
                    oStaff_Master.CreatedBy = userID.ToString();
                    oStaff_Master.SID = indi.CreateSTID();
                    oStaff_Master.Surname = lname;
                    oStaff_Master.Initials = fname;
                    oStaff_Master.Service_Type = 2;
                    oStaff_Master.UserID = maxID + 1;
                    oStaff_Master.LOCID = DivisionID;

                    oStaff_Master.Job_CategoryID = 1;
                    oStaff_Master.ServiceNo = ServiceNo;
                    oStaff_Master.SpecialityID = Convert.ToInt32(ClinicTypeID);

                    var objs = JsonConvert.DeserializeObject<List<menureader>>(userItems);
                    int objcount = objs.Count;
                    UserPermission[] objVital = new UserPermission[100];
                    int[] mnlist = new int[100];
                    int i = 0;
                    int h = 0;
                    foreach (menureader p in objs)
                    {
                        int mainmnu = Convert.ToInt32(p.tmenuid);
                        UserPermission rpr = (from rr in db.UserPermissions
                                              where rr.MenuID == mainmnu
                                              && rr.UserID == (maxID + 1)
                                              select rr).FirstOrDefault();

                        if (rpr == null && h == 0 || !mnlist.Contains(mainmnu))
                        {
                            UserPermission upf1 = new UserPermission();

                            upf1.UserID = maxID + 1;
                            upf1.LocationID = LocationID;
                            upf1.DivisionID = DivisionID;
                            upf1.RoleID = "R001";
                            upf1.SysID = "Test";
                            upf1.MenuID = Convert.ToInt32(p.tmenuid);
                            upf1.SHO = 0;
                            upf1.NEW = 0;
                            upf1.EDT = 0;
                            upf1.DEL = 0;
                            upf1.PRN = 0;
                            upf1.SER = 0;
                            upf1.CER = 0;
                            upf1.CreatedDate = DateTime.Now;
                            upf1.CreatedUser = 1;

                            upf1.UserProfID = maxID1 + (i + 1);

                            objVital[i] = upf1;
                            mnlist[i] = mainmnu;
                            h++;
                            i++;
                        }


                        UserPermission upf = new UserPermission();

                        upf.UserID = maxID + 1;
                        upf.LocationID = LocationID;
                        upf.DivisionID = DivisionID;
                        upf.RoleID = "R001";
                        upf.SysID = "Test";
                        upf.MenuID = Convert.ToInt32(p.tsmenuid);
                        upf.SHO = 0;
                        upf.NEW = 0;
                        upf.EDT = 0;
                        upf.DEL = 0;
                        upf.PRN = 0;
                        upf.SER = 0;
                        upf.CER = 0;
                        upf.CreatedDate = DateTime.Now;
                        upf.CreatedUser = 1;

                        upf.UserProfID = maxID1 + (i + 1);

                        objVital[i] = upf;
                        i++;
                        // db.Vitals.Add(oVital);

                    }
                    objVital = objVital.Where(x => x != null).ToArray();
                    db.UserPermissions.AddRange(objVital);
                    db.Staff_Master.Add(oStaff_Master);
                    db.Users.Add(user1);
                    db.SaveChanges();







                    err = "Saved";
                }
            }
                catch (Exception ex)
                {
                err = ex.ToString();
            }
                return Json(err, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Savemenu(string ServiceNo, string LocationID, string DivisionID, string ClinicTypeID, string menuid)
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            char[] MyChar = { '/', '"', ' ' };
            ServiceNo = ServiceNo.Trim(MyChar);
            LocationID = LocationID.Trim(MyChar);
            DivisionID = DivisionID.Trim(MyChar);
            ClinicTypeID = ClinicTypeID.Trim(MyChar);
            menuid = menuid.Trim(MyChar);
            int mid = Convert.ToInt32(menuid);
            IndexGeneration indi = new IndexGeneration();
            //}
            string opdid = (String)Session["userlocid1"];
            try
            {
                int userID = 0;
                int userID1 = 0;
                if (Session["UserID"] != null)
                {
                    userID = (int)Session["UserID"];
                }
                

               
                string locid = "";
                var uss = (from rr in db.Users
                          where rr.UserName == ServiceNo

                          select rr).ToList();
                foreach (var tt in uss)
                {
                    userID1= tt.UserID;
                }
                    Staff_Master oStaff_Master = new Staff_Master();
                oStaff_Master.LocationID = LocationID;
                oStaff_Master.CreatedDate = System.DateTime.Now;
                oStaff_Master.CreatedBy = userID.ToString();
                oStaff_Master.SID = indi.CreateSTID();
                oStaff_Master.Surname = "";
                oStaff_Master.Initials = "";
                oStaff_Master.Service_Type = 2;
                oStaff_Master.UserID = userID1;
                oStaff_Master.LOCID = DivisionID;

                oStaff_Master.Job_CategoryID = 1;
                oStaff_Master.ServiceNo = ServiceNo;
                oStaff_Master.SpecialityID = Convert.ToInt32(ClinicTypeID);
                var maxID = db.UserPermissions
                   .Max(p => p.UserProfID);

                
                    UserPermission rpr1 = (from rr in db.UserPermissions
                                           where rr.MenuID == mid
                                           && rr.UserID == userID1
                                           select rr).FirstOrDefault();
                    if (rpr1 == null)
                    {

                        UserPermission upf = new UserPermission();

                        upf.UserID = userID1;
                        upf.LocationID = LocationID;
                        upf.DivisionID = DivisionID;
                        upf.RoleID = "R001";
                        upf.SysID = "Test";
                        upf.MenuID = mid;
                        upf.SHO = 1;
                        upf.NEW = 1;
                        upf.EDT = 1;
                        upf.DEL = 1;
                        upf.PRN =1;
                        upf.SER = 1;
                        upf.CER = 1;
                        upf.CreatedDate = DateTime.Now;
                        upf.CreatedUser = 1;


                        maxID = maxID + 1;
                        upf.UserProfID = maxID;
                        db.UserPermissions.Add(upf);
                    }

                    var mi = (from rr in db.MenuItems
                              where rr.ParentMenuID == mid

                              select rr).ToList();
                    foreach (var ti in mi)
                    {

                        UserPermission rpr = (from rr in db.UserPermissions
                                              where rr.MenuID == ti.MenuID
                                              && rr.UserID == userID1
                                              select rr).FirstOrDefault();
                        if (rpr == null)
                        {

                            UserPermission upf = new UserPermission();

                            upf.UserID = userID1;
                            upf.LocationID = LocationID;
                            upf.DivisionID = DivisionID;
                            upf.RoleID = "R001";
                            upf.SysID = "Test";
                            upf.MenuID = ti.MenuID;
                            upf.SHO = 1;
                            upf.NEW = 1;
                            upf.EDT = 1;
                            upf.DEL = 1;
                            upf.PRN = 1;
                            upf.SER = 1;
                            upf.CER = 1;
                            upf.CreatedDate = DateTime.Now;
                            upf.CreatedUser = 1;

                            maxID = maxID + 1;
                            upf.UserProfID = maxID;
                            db.UserPermissions.Add(upf);
                        }
                    }
                
            



                db.Staff_Master.Add(oStaff_Master);
                
                db.SaveChanges();







                err = "Saved";
            }
            catch (Exception ex)
            {
                err = ex.ToString();
            }
            return Json(err, JsonRequestBehavior.AllowGet);
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserName,Pass,ConPass,Salutation,FName,LName,ServiceNo,RANK1,ClinicTypeID,LocationID,ContactNo,Email")] UserModel user, [Bind(Include = "UserName,Status,Salutation,FName,LName,ServiceNo,Rank,Trade,Designation,CompetentTrade,LocationID,DivisionID,NIC,ContactNo,Email,Address1,Address2,Address3,DOB,Gender,MaritalStatus,LastVisit,CreatedDate,CreatedUser,ModifiedDate,ModifiedUser,msrepl_tran_version,Directorate,RoleID")] User user1)
        {
            //if (user.ConPass != user.Pass)
            //{
            IndexGeneration indi = new IndexGeneration();
            //}
            ModelState.Remove("UserName");
            ModelState.Remove("RoleID");
            if (ModelState.IsValid)
            {
                try
                {
                    db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
                    DataTable oDataSet = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = " select UserName from [dbo].[Users] with (nolock) where UserName='" + user.ServiceNo + "' ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlConnection.Open();
                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                    oSqlDataAdapter.Fill(oDataSet);
                    oSqlConnection.Close();
                    if (oDataSet.Rows.Count > 0)
                    {
                        return RedirectToAction("Create");
                    }
                    else
                    {




                        int userID = 0;

                        if (Session["UserID"] != null)
                        {
                            userID = (int)Session["UserID"];
                        }
                        var maxID = db.Users
                        .Max(p => p.UserID);


                        byte[] encPwd = EncriptPassword(user.Pass);

                        user1.Pass = encPwd;
                        user1.LocationID = user.LocationID;
                        user1.UserID = maxID + 1;
                        user1.CreatedDate = System.DateTime.Now;
                        user1.RoleID = "R001";
                        user1.UserName = user.ServiceNo;
                        string locid = "";
                        int clid = Convert.ToInt32(user.ClinicTypeID);
                        var locidselect = from s in db.Clinic_Master

                                          where s.LocationID == user.LocationID
                                          where s.ClinicTypeID == clid
                                          select new { s.Clinic_ID };

                        foreach (var item in locidselect)
                        {
                            locid = item.Clinic_ID;
                        }

                        Staff_Master oStaff_Master = new Staff_Master();
                        oStaff_Master.LocationID = user.LocationID;
                        oStaff_Master.CreatedDate = System.DateTime.Now;
                        oStaff_Master.CreatedBy = userID.ToString();
                        oStaff_Master.SID = indi.CreateSTID();
                        oStaff_Master.Surname = user.LName;
                        oStaff_Master.Initials = user.FName;
                        oStaff_Master.Service_Type = 2;
                        oStaff_Master.UserID = maxID + 1;
                        oStaff_Master.LOCID = locid;
                        oStaff_Master.Job_CategoryID = 2;
                        oStaff_Master.ServiceNo = user.UserName;
                        oStaff_Master.SpecialityID = Convert.ToInt32(user.ClinicTypeID);
                        db.Staff_Master.Add(oStaff_Master);
                        db.Users.Add(user1);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");

                    }
                }
                catch (Exception ex)
                {
                    return HttpNotFound();
                }

            }

            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", user.RoleID);
            ViewBag.UserID = new SelectList(db.UserProfiles, "UserID", "LocationID", user.UserID);
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", user.RoleID);
            ViewBag.UserID = new SelectList(db.UserProfiles, "UserID", "LocationID", user.UserID);
            return View(user);
        }
        public ActionResult changepass()
        {
            
           
          
            return View();
        }
        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserID,RoleID,UserName,Pass,Status,Salutation,FName,LName,ServiceNo,Rank,Trade,Designation,CompetentTrade,LocationID,DivisionID,NIC,ContactNo,Email,Address1,Address2,Address3,DOB,Gender,MaritalStatus,LastVisit,CreatedDate,CreatedUser,ModifiedDate,ModifiedUser,msrepl_tran_version,Directorate")] User user, string Password)
        {
            if (ModelState.IsValid)
            {
                if (!String.IsNullOrEmpty(Password))
                {
                    byte[] encPwd = EncriptPassword(Password);

                    user.Pass = encPwd;
                }
                else
                {
                    user.Pass = user.Pass;
                }

                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", user.RoleID);
            ViewBag.UserID = new SelectList(db.UserProfiles, "UserID", "LocationID", user.UserID);
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> changepass([Bind(Include = "Pass,ConPass")] UserModel user,[Bind(Include = "UserID")] User user1)
        {
            try
            {
                ModelState.Remove("UserName");
                ModelState.Remove("RoleID");
                if (ModelState.IsValid)
                {
                    if (!String.IsNullOrEmpty((user.Pass).ToString()))
                    {
                        byte[] encPwd = EncriptPassword((user.Pass).ToString());

                        user1.Pass = encPwd;
                    }
                    else
                    {

                    }
                    User user3 =  db.Users.Find(Convert.ToInt32(Session["UserID"]));
                    user1.UserID = Convert.ToInt32(Session["UserID"]);
                    user1.UserName = user3.UserName;
                    user1.RoleID = user3.RoleID;
                    user1.ServiceNo = user3.ServiceNo;
                    user1.LocationID = user3.LocationID;
                    user1.LName = user3.LName;
                    user1.FName = user3.FName;
                    user1.Salutation = user3.Salutation;
                    //var user2 = new User() { UserID = user1.UserID, Pass = user1.Pass ,RoleID= user3.RoleID,UserName= user3.UserName};
                    //db.Users.Attach(user2);
                    //db.Entry(user2).Property(x => x.Pass).IsModified = true;
                    //db.Entry(user2).Property(x => x.RoleID).IsModified = false;
                    //db.Entry(user2).Property(x => x.UserName).IsModified = false;
                    //db.Entry(user1).State = EntityState.Modified;
                    db.Set<User>().AddOrUpdate(user1);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index", "Home", "");
                }
             
                return View(user);
            }
            catch (Exception ex)
            {
                return View(user);
            }
        }

        [HttpGet]
      
        public async Task<ActionResult> resetpass(string id)
        {
            try
            {
                db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
                User user1 = new User();
               User user3 = db.Users.Find(Convert.ToInt32(id));
                byte[] encPwd = EncriptPassword("123");

                user1.Pass = encPwd;
                user1.UserID = user3.UserID;
                    user1.UserName = user3.UserName;
                    user1.RoleID = user3.RoleID;
                    user1.ServiceNo = user3.ServiceNo;
                    user1.LocationID = user3.LocationID;
                    user1.LName = user3.LName;
                    user1.FName = user3.FName;
                    user1.Salutation = user3.Salutation;
                    //var user2 = new User() { UserID = user1.UserID, Pass = user1.Pass ,RoleID= user3.RoleID,UserName= user3.UserName};
                    //db.Users.Attach(user2);
                    //db.Entry(user2).Property(x => x.Pass).IsModified = true;
                    //db.Entry(user2).Property(x => x.RoleID).IsModified = false;
                    //db.Entry(user2).Property(x => x.UserName).IsModified = false;
                    //db.Entry(user1).State = EntityState.Modified;
                    db.Set<User>().AddOrUpdate(user1);
                    await db.SaveChangesAsync();
                 return RedirectToAction("Index", "Users", ""); 

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Users", ""); 
            }
        }


        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        public JsonResult Getloc()
        {
            var status = db.Vw_Establishment.Where(x => x.HelpDeskStatus =="1").Select(x => new { x.LocationID, x.LocationName}).ToList();
            return Json(status, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Getdiv(string id,string id1)
        {
            char[] MyChar = { '/', '"', ' ' };
            if (!String.IsNullOrEmpty(id)) { id = id.Trim(MyChar); }
            if (!String.IsNullOrEmpty(id1)) { id1 = id1.Trim(MyChar); }
            if (id1 == "24")
            {
                var status = db.Vw_Formation.Where(x => x.LocationID == id).Select(x => new {  x.DivisionID, x.DivisionName }).ToList();
                return Json(status, JsonRequestBehavior.AllowGet);
            }
           else if (id1 == "26")
            {
                int ct = Convert.ToInt32(id1);
                var status = db.Clinic_Master.Where(x => x.LocationID == id).Where(x => x.ClinicTypeID == 19).Select(x => new { DivisionID = x.Clinic_ID, DivisionName = x.Clinic_Detail }).ToList();
                return Json(status, JsonRequestBehavior.AllowGet);
            }
            else if (id1 == "27")
            {
                int ct = Convert.ToInt32(id1);
                var status = db.Clinic_Master.Where(x => x.LocationID == id).Where(x => x.ClinicTypeID == 19).Select(x => new { DivisionID = x.Clinic_ID, DivisionName = x.Clinic_Detail }).ToList();
                return Json(status, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int ct = Convert.ToInt32(id1);
                var status = db.Clinic_Master.Where(x => x.LocationID == id).Where(x => x.ClinicTypeID == ct).Select(x => new { DivisionID= x.Clinic_ID, DivisionName= x.Clinic_Detail }).ToList();
                return Json(status, JsonRequestBehavior.AllowGet);
            }
          
           

        }
        public JsonResult Getdivnurs(string id)
        {
            char[] MyChar = { '/', '"', ' ' };
            if (!String.IsNullOrEmpty(id)) { id = id.Trim(MyChar); }
          // if (!String.IsNullOrEmpty(id1)) { id1 = id1.Trim(MyChar); }
           
                var status = db.Vw_Formation.Where(x => x.LocationID == id).Select(x => new { x.DivisionID, x.DivisionName }).ToList();
                return Json(status, JsonRequestBehavior.AllowGet);
           


        }
        public JsonResult Getmmenu()
        {
            char[] MyChar = { '/', '"', ' ' };
           // if (!String.IsNullOrEmpty(id)) { id = id.Trim(MyChar); }
            // if (!String.IsNullOrEmpty(id1)) { id1 = id1.Trim(MyChar); }

            var status = db.MenuItems.Where(x => x.ParentMenuID == 0).Select(x => new { x.MenuID, x.MenuName }).ToList();
            return Json(status, JsonRequestBehavior.AllowGet);



        }
        public JsonResult Getsubmmenu(string id)
        {
            char[] MyChar = { '/', '"', ' ' };
            if (!String.IsNullOrEmpty(id)) { id = id.Trim(MyChar); }
            // if (!String.IsNullOrEmpty(id1)) { id1 = id1.Trim(MyChar); }
            int mid = Convert.ToInt32(id);
            var status = db.MenuItems.Where(x => x.ParentMenuID == mid).Select(x => new { x.MenuID, x.MenuName }).ToList();
            return Json(status, JsonRequestBehavior.AllowGet);



        }
        public JsonResult Getcltype(string id)
        {
            var status = db.Clinic_Type.Select(x => new { x.ClinicTypeID, x.ClinicDetails}).ToList();
            return Json(status, JsonRequestBehavior.AllowGet);

        }
        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            User user = await db.Users.FindAsync(id);
            db.Users.Remove(user);
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

        public ActionResult LogOff()
        {
           
            FormsAuthentication.SignOut();
            try
            {
                //var lo = .GetOwinContext().Authentication;
                //lo.SignOut(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie);
                Session.Abandon();
                User us = GetCurrentUser();
                return RedirectToAction("Login", "Users");
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Users");
            }


        }

        private User GetCurrentUser()
        {
            User us = new User();
            int userID = 0;
            try
            {

                if (Session["UserID"] != null)
                {
                    userID = (int)Session["UserID"];
                }
                //int userID = Int32.Parse(HttpContext.User.Identity.Name);

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

        ///
        // Charmara Methods

        //Save User ID in Cookie and redirect to UserPermission Index
        public ActionResult ListRolePermission(int id)
        {
            User us = new User();
            us = (from u in db.Users
                  where u.UserID == id
                  select u).FirstOrDefault();

            FormsAuthentication.SetAuthCookie(us.UserID.ToString(), false);

            return RedirectToAction("UserPermissionIndex", "UserPermissions");
        }

        public ActionResult AssignUserPermission(int id)
        {

            try
            {
                User us = new User();
                us = (from u in db.Users
                      where u.UserID == id
                      select u).FirstOrDefault();

                FormsAuthentication.SetAuthCookie(us.UserID.ToString(), false);

                return RedirectToAction("Create", "UserPermission");
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        public ActionResult ChangePwd(int id)
        {
            try
            {
                User us = new User();
                us = (from u in db.Users
                      where u.UserID == id
                      select u).FirstOrDefault();

                FormsAuthentication.SetAuthCookie(us.UserID.ToString(), false);

                return RedirectToAction("ChangePwd", "ChangePassword");
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }
        public ActionResult Setlocids(string id)
        {
            char[] MyChar = { '/', '"', ' ' };
            string NewString = id.Trim(MyChar);
            if (!String.IsNullOrEmpty(NewString))
            {
                dynamic data = JObject.Parse(NewString);
                NewString=(String)data.Clinic_ID;
               
                
                Session["userlocid1"] = NewString.ToString();
            }
            return null;
        }
        public JsonResult loadsubloc(string id)
        {

            try
            {
                string isofficer = "";
                string ploc = "";
                string pform = "";
             
                char[] MyChar = { '/', '"', ' ' };
                string NewString = id.Trim(MyChar);
                //////////////////////////////////////////

                IndexGeneration indi = new IndexGeneration();
                DataTable oDataSetv8 = new DataTable();
                oSqlConnection = new SqlConnection(conStr);
                oSqlCommand = new SqlCommand();
                sqlQuery = "   select * from [dbo].[Vw_Formation] where LocationID='" + NewString + "' ";

                oSqlCommand.Connection = oSqlConnection;
                oSqlCommand.CommandText = sqlQuery;
                oSqlCommand.CommandTimeout = 120;

                oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                oSqlDataAdapter.Fill(oDataSetv8);

                var items = oDataSetv8.AsEnumerable()
     .Select(dataRow => new Vw_Formation
     {


         DivisionID = dataRow.Field<string>("DivisionID").ToString(),
         DivisionName = dataRow.Field<string>("DivisionName"),
        

     }).ToList();





                var jsonResult = Json(items, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                


            }
            catch (Exception ex)
            {
                var jsonResult = Json("", JsonRequestBehavior.AllowGet);
                return jsonResult;

            }
        }
        public JsonResult Getuserdept(string id)
        {

            try
            {
                string isofficer = "";
            string ploc = "";
            string pform = "";
            //db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            //db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
            //db.Database.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;");
            char[] MyChar = { '/', '"', ' ' };
            string NewString = id.Trim(MyChar);
            //////////////////////////////////////////

            IndexGeneration indi = new IndexGeneration();
            DataTable oDataSetv8 = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = "   select top 1 Posted_Formation,Posted_Location,ServiceType from [dbo].[PersonalDetails] where ServiceNo='" + NewString + "' ";

            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlCommand.CommandTimeout = 120;

            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSetv8);

            if (oDataSetv8.Rows.Count > 0)
            {
                int cnt = 0;

                foreach (DataRow row in oDataSetv8.Rows)
                {
                    isofficer = row["ServiceType"].ToString();
                    ploc = row["Posted_Location"].ToString();
                    pform = row["Posted_Formation"].ToString();
                }
            }


                        
            //////////////////////////////////////////

            var items = from s in db.Clinic_Master.Where(p => p.Clinic_ID == "") select new { s.Clinic_Detail, s.Clinic_ID };
            var ser = from s in db.Users.Where(p => p.UserName == NewString) select new { s.UserID };
            var joblist = new String[100];
           // var locret1 = (dynamic)null;
           // var locret2 = (dynamic)null;
            var locret3 = (dynamic)null;
           
                /////////////////////////////////////////////////

                if (ser.Count() <= 0)
                {



                    if (isofficer.Equals("1"))
                    {
                        int userID = 0;

                        if (Session["UserID"] != null)
                        {
                            userID = (int)Session["UserID"];
                        }
                        var maxID = db.Users
                        .Max(p => p.UserID);


                        byte[] encPwd2 = EncriptPassword("123");
                        User user1 = new User();


                        user1.LocationID = ploc;
                        user1.UserID = maxID + 1;
                        user1.CreatedDate = System.DateTime.Now;
                        user1.RoleID = "R001";
                        user1.UserName = NewString;
                        string locid = "";

                        var maxIDu = db.UserPermissions
          .Max(p => p.UserProfID);


                        Staff_Master oStaff_Master = new Staff_Master();
                        oStaff_Master.LocationID = ploc;
                        oStaff_Master.CreatedDate = System.DateTime.Now;
                        oStaff_Master.CreatedBy = userID.ToString();
                        oStaff_Master.SID = indi.CreateSTID();
                        oStaff_Master.Surname = "";
                        oStaff_Master.Initials = "";
                        oStaff_Master.Service_Type = 1;
                        oStaff_Master.UserID = maxID + 1;
                        oStaff_Master.LOCID = pform;
                        oStaff_Master.Job_CategoryID = 1;
                        oStaff_Master.ServiceNo = NewString;
                        oStaff_Master.SpecialityID = 24;

                        UserPermission rpr1 = (from rr in db.UserPermissions
                                               where rr.MenuID == 78
                                               && rr.UserID == maxID + 1
                                               select rr).FirstOrDefault();
                        if (rpr1 == null)
                        {
                            UserPermission upf = new UserPermission();

                            upf.UserID = maxID + 1;
                            upf.LocationID = ploc;
                            upf.DivisionID = pform;
                            upf.RoleID = "R001";
                            upf.SysID = "Test";
                            upf.MenuID = 78;
                            upf.SHO = 1;
                            upf.NEW = 1;
                            upf.EDT = 1;
                            upf.DEL = 1;
                            upf.PRN = 1;
                            upf.SER = 1;
                            upf.CER = 1;
                            upf.CreatedDate = DateTime.Now;
                            upf.CreatedUser = 1;


                            maxIDu = maxIDu + 1;
                            upf.UserProfID = maxIDu;
                            db.UserPermissions.Add(upf);
                        }

                        var mi = (from rr in db.MenuItems
                                  where rr.ParentMenuID == 78

                                  select rr).ToList();
                        foreach (var ti in mi)
                        {

                            UserPermission rpr = (from rr in db.UserPermissions
                                                  where rr.MenuID == ti.MenuID
                                                  && rr.UserID == maxID + 1
                                                  select rr).FirstOrDefault();
                            if (rpr == null)
                            {

                                UserPermission upf = new UserPermission();

                                upf.UserID = maxID + 1;
                                upf.LocationID = ploc;
                                upf.DivisionID = pform;
                                upf.RoleID = "R001";
                                upf.SysID = "Test";
                                upf.MenuID = ti.MenuID;
                                upf.SHO = 1;
                                upf.NEW = 1;
                                upf.EDT = 1;
                                upf.DEL = 1;
                                upf.PRN = 1;
                                upf.SER = 1;
                                upf.CER = 1;
                                upf.CreatedDate = DateTime.Now;
                                upf.CreatedUser = 1;

                                maxIDu = maxIDu + 1;
                                upf.UserProfID = maxIDu;
                                db.UserPermissions.Add(upf);
                            }






                            db.Staff_Master.Add(oStaff_Master);
                            db.Users.Add(user1);
                            db.SaveChanges();

                        }


                    }

                }
                //////////////////////////////////////////////////////







                foreach (var item in ser)
            {

               var  serv = from s in db.Staff_Master.Where(p => p.UserID == item.UserID) select new { s.LOCID,s.Job_CategoryID,s.LocationID,s.SpecialityID };







                    
                    int ij = 0;
                    foreach (var item1 in serv)
                {

                        //if (item1.SpecialityID == 24)
                        //{
                        //    var items3 = from s in db.Vw_Establishment.Where(p => p.HelpDeskStatus == "1") select new { Clinic_Detail = s.LocationName, Clinic_ID = s.LocationID };
                        //    items = items3.Concat(items);
                          
                        //}




                        if (item1.Job_CategoryID == 2)
                        {
                            var items4 = from s in db.Clinic_Master.Where(p => p.ClinicTypeID==19|| p.ClinicTypeID==20) select new { s.Clinic_Detail, s.Clinic_ID };
                            items = items4.Concat(items);
                            break;
                        }
                        else
                        {

                            var items2 = from s in db.Clinic_Master.Where(p => p.Clinic_ID == item1.LOCID) select new { s.Clinic_Detail, s.Clinic_ID };

                          
                            var items3 = from s in db.Vw_Formation.Where(p => p.DivisionID == item1.LOCID).Where(p => p.LocationID == item1.LocationID) select new { Clinic_Detail=s.DivisionName, Clinic_ID= s.DivisionID };
                            if (ij != 0)
                            {
                                items = items2.Concat(items);

                            }
                            else
                            {

                                items = items2;
                            }
                            items = items3.Concat(items);



                        }
                            ij++;
                        }



                    if (isofficer.Equals("1"))
                    {
                        var items3 = from s in db.Vw_Establishment.Where(p => p.HelpDeskStatus == "1") select new { Clinic_Detail = s.LocationName, Clinic_ID = s.LocationID };


                        items = items3.Concat(items);

                    }



                    var jsonResult = Json(items, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
                }

                var jsonResult1 = Json("", JsonRequestBehavior.AllowGet);
                return jsonResult1;

            }
            catch (Exception ex)
            {
                var jsonResult = Json("", JsonRequestBehavior.AllowGet);
                return jsonResult;

            }
        }



        public JsonResult loginuser(string username,string passwd,string Clinic_ID, string DivisionID)
        {
            try
            {
                char[] MyChar = { '\\', '"', ' ' };
                string username1 = username.Trim(MyChar);
                string passwd1 = passwd.Trim(MyChar);
                string Clinic_ID1 = Clinic_ID.Trim(MyChar);
                string DivisionID1 = DivisionID.Trim(MyChar);
                string finalclicid = "";
                string finallocid = "";
                string locid11 = "";
                SqlConnection oSqlConnection;
                SqlCommand oSqlCommand;
                SqlDataAdapter oSqlDataAdapter;
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;

                if (!String.IsNullOrEmpty(DivisionID1))
                {
                    finalclicid = DivisionID1;
                    finallocid = Clinic_ID1;
                }
                else
                {
                    DataTable oDataSetv8 = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    //sqlQuery = "   SELECT [LocationID] FROM [MMS].[dbo].[Staff_Master] where [LOCID]='" + Clinic_ID1 + "' and [ServiceNo]='" + username1 + "' ";
                   // sqlQuery = "   SELECT a.[LocationID]    FROM[MMS].[dbo].[Staff_Master] as a inner join[MMS].[dbo].[Users] as b on a.UserID=b.userid where a.[LOCID]='" + Clinic_ID1 + "' and b.[UserName]= '" + username1 + "' ";
                    sqlQuery = " SELECT [LocationID] FROM[MMS].[dbo].[Clinic_Master] where[Clinic_ID] = '" + Clinic_ID1 + "' ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlCommand.CommandTimeout = 120;

                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                    oSqlDataAdapter.Fill(oDataSetv8);

                    if (oDataSetv8.Rows.Count > 0)
                    {
                        int cnt = 0;

                        foreach (DataRow row in oDataSetv8.Rows)
                        {
                            locid11 = row["LocationID"].ToString();
                          
                        }
                    }


                    finallocid = locid11;
                    finalclicid = Clinic_ID1;
                }
                if (string.IsNullOrEmpty(finallocid))
                {

                    DataTable oDataSetv8 = new DataTable();
                    oSqlConnection = new SqlConnection(conStr);
                    oSqlCommand = new SqlCommand();
                    sqlQuery = "   SELECT [LocationID] FROM [MMS].[dbo].[Staff_Master] where [LOCID]='" + Clinic_ID1 + "' and [ServiceNo]='" + username1 + "' ";
                    // sqlQuery = "   SELECT a.[LocationID]    FROM[MMS].[dbo].[Staff_Master] as a inner join[MMS].[dbo].[Users] as b on a.UserID=b.userid where a.[LOCID]='" + Clinic_ID1 + "' and b.[UserName]= '" + username1 + "' ";
                    //sqlQuery = " SELECT [LocationID] FROM[MMS].[dbo].[Clinic_Master] where[Clinic_ID] = '" + Clinic_ID1 + "' ";
                    oSqlCommand.Connection = oSqlConnection;
                    oSqlCommand.CommandText = sqlQuery;
                    oSqlCommand.CommandTimeout = 120;

                    oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
                    oSqlDataAdapter.Fill(oDataSetv8);

                    if (oDataSetv8.Rows.Count > 0)
                    {
                        int cnt = 0;

                        foreach (DataRow row in oDataSetv8.Rows)
                        {
                            locid11 = row["LocationID"].ToString();

                        }
                    }
                    finallocid = locid11;
                }
              
                byte[] encPwd = EncriptPassword(passwd1);
                // var obj2 = (dynamic)null;
                var obj = (from usr in db.EpasUsers
                           where usr.userid == username1
                           && usr.password == encPwd
                           select usr).FirstOrDefault();









                //  var rt = items.ToList();
                // int ipos = Convert.ToInt32(loclisi);
                if (string.IsNullOrEmpty( finalclicid)|| string.IsNullOrEmpty(finallocid))
                {
                    var jsonResult = Json("Error Occured try again!", JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
             else   if (obj != null)
                {

                    var obj2 = (from usr in db.Users
                                where usr.UserName == obj.userid

                                select usr).FirstOrDefault();
                    Session["UserID"] = obj2.UserID;
                    Session["userlocid1"] = finalclicid;

                    Session["userloc"] = finallocid;

                    FormsAuthentication.SetAuthCookie(obj2.UserID.ToString(), false);
                    User varUser = db.Users.Find(obj2.UserID);
                    varUser.LastVisit = System.DateTime.Now;
                    db.SaveChanges();
                    Session["loginerror"] = "";
                    var jsonResult = Json("ok", JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
              else  if (obj == null && username1.All(Char.IsLetter))
                {
                    var obj2 = (from usr in db.Users
                                where usr.UserName == username1
                                && usr.Pass == encPwd
                                select usr).FirstOrDefault();
                    Session["UserID"] = obj2.UserID;
                    Session["userlocid1"] = finalclicid;

                    Session["userloc"] = finallocid;

                    FormsAuthentication.SetAuthCookie(obj2.UserID.ToString(), false);
                    User varUser = db.Users.Find(obj2.UserID);
                    varUser.LastVisit = System.DateTime.Now;
                    db.SaveChanges();
                    Session["loginerror"] = "";

                    var jsonResult = Json("ok", JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                    //  return RedirectToAction("Index", "Home");
                }
                else
                {
                    var jsonResult = Json("Incorrect Username or Password!", JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
               
            }
            catch (Exception ex)
            {
                Session["loginerror"] = "Incorrect Username or Password!";
                var jsonResult = Json("Incorrect Username or Password!", JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
                //return RedirectToAction("Login", "Users");
            }
        }






        public JsonResult Getphloc()
        {
            db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
          
            db.Database.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;");
            char[] MyChar = { '/', '"', ' ' };
            DataTable oDataSet1 = new DataTable();
            //  string NewString = id.Trim(MyChar);
            string opdid = (String)Session["userlocid1"];
            DataTable oDataSet = new DataTable();
            oSqlConnection = new SqlConnection(conStr);
            oSqlCommand = new SqlCommand();
            sqlQuery = "SELECT        Clinic_ID, Clinic_Detail "+
" FROM Clinic_Master WHERE  LocationID  =( SELECT LocationID  FROM Clinic_Master WHERE Clinic_ID = '" + opdid + "') AND ClinicTypeID = 22 ";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSet);
            oSqlConnection.Close();
            var opd = oDataSet.AsEnumerable()
      .Select(dataRow => new Clinic_Master
      {
          Clinic_ID = dataRow.Field<string>("Clinic_ID"),
          Clinic_Detail = dataRow.Field<string>("Clinic_Detail")

      }).ToList();
            var jsonResult = Json(opd, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        [HttpGet]
        public ActionResult Login()
        {
            try
            { }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(UserModel user, string returnUrl)
        {

            try
            {
                db.Database.ExecuteSqlCommand("SET LOCK_TIMEOUT 10000;");
                byte[] encPwd = EncriptPassword(user.Pass);

                var obj = (from usr in db.Users
                           where usr.UserName == user.UserName
                           && usr.Pass == encPwd
                           select usr).FirstOrDefault();
               
                if (obj != null)
                {
                    Session["UserID"] = obj.UserID.ToString();
                    FormsAuthentication.SetAuthCookie(obj.UserID.ToString(), false);
                    User varUser = db.Users.Find(obj.UserID);
                    varUser.LastVisit = System.DateTime.Now;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                return View();
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        public byte[] EncriptPassword(string Passwd)
        {
            MD5CryptoServiceProvider MD5Pass = new MD5CryptoServiceProvider();
            byte[] HashPass;
            UTF8Encoding Encoder = new UTF8Encoding();
            HashPass = MD5Pass.ComputeHash(Encoder.GetBytes(Passwd));
            return HashPass;
        }

        //Save User ID in Cookie and redirect to UserPermission Index


    }
}
