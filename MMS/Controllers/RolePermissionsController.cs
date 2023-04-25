using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;

using MMS.Models;
using System.Web.Security;
using System.Data.Entity.Validation;

namespace MMS.Controllers
{
    public class RolePermissionsController : Controller
    {
        private MMSEntities db = new MMSEntities();

        // GET: RolePermissions
        public async Task<ActionResult> Index()
        {
            var rolePermissions = db.RolePermissions.Include(r => r.MenuItem).Include(r => r.Role);
            return View(await rolePermissions.ToListAsync());
        }

        // GET: RolePermissions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RolePermission rolePermission = await db.RolePermissions.FindAsync(id);
            if (rolePermission == null)
            {
                return HttpNotFound();
            }
            return View(rolePermission);
        }

        // GET: RolePermissions/Create
        public ActionResult Create()
        {
            try
            {
                Role rl = GetCurrentRole();
                ViewBag.RoleName = rl.RoleName;

                List<RolePermissionModel> lstRolePermision = new List<RolePermissionModel> { new RolePermissionModel { RPermID = 0, RoleID = "", SysID = "", MenuID = 0, SHO = false, NEW = false, EDT =false,
                                                                                                    DEL=false,PRN=false,SER=false,CER=false,CreatedDate=new DateTime(),CreatedUser=0} };
                //ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName");
                ViewBag.MenuID = new SelectList(db.MenuItems, "MenuID", "MenuName");
                return View(lstRolePermision);
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        // POST: RolePermissions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(List<RolePermissionModel> lstRolePer)
        {
            try
            {
                #region UserID
                int userID = 0;

                //User us = GetCurrentUser();
                if (Session["UserID"] != null)
                {
                    userID = (int)Session["UserID"];
                }
                #endregion

                int rpid = 0;
                Role rl = GetCurrentRole();

                if (rl != null)
                {
                    var maxID = db.RolePermissions
                    .Max(p => p.RPermID);

                    foreach (var i in lstRolePer)
                    {
                        RolePermission rpr = (from rr in db.RolePermissions
                                              where rr.MenuID == i.MenuID
                                              && rr.RoleID == rl.RoleID
                                              select rr).FirstOrDefault();

                        if (rpr == null)
                        {
                            RolePermission upf = new RolePermission();

                            upf.RPermID = rpid;
                            upf.RoleID = rl.RoleID;
                            upf.SysID = "Test";
                            upf.MenuID = i.MenuID;
                            upf.ObjectCode = "36";
                            upf.SHO = i.SHO == true ? 1 : 0;
                            upf.NEW = i.NEW == true ? 1 : 0;
                            upf.EDT = i.EDT == true ? 1 : 0;
                            upf.DEL = i.DEL == true ? 1 : 0;
                            upf.PRN = i.PRN == true ? 1 : 0;
                            upf.SER = i.SER == true ? 1 : 0;
                            upf.CER = i.CER == true ? 1 : 0;
                            upf.CreatedDate = DateTime.Now;
                            upf.CreatedUser = userID;

                            MenuItem mi = (from m in db.MenuItems
                                           where m.MenuID == i.MenuID
                                           select m).FirstOrDefault();

                            //Check considered RolePermission's menu item,s parent menu item is exist
                            if (mi.ParentMenuID > 0)
                            {
                                RolePermissionModel upm = (from ls in lstRolePer
                                                           where ls.MenuID == mi.ParentMenuID
                                                           select ls).FirstOrDefault();

                                RolePermission up = (from uu in db.RolePermissions
                                                     where uu.MenuID == mi.ParentMenuID
                                                     && uu.RoleID == rl.RoleID
                                                     select uu).FirstOrDefault();

                                if (upm == null && up == null)
                                {
                                    MenuItem rpm = (from r in db.MenuItems
                                                    where r.MenuID == mi.ParentMenuID
                                                    select r).FirstOrDefault();

                                    if (rpm != null)
                                    {
                                        RolePermission rm = new RolePermission();

                                        rm.RPermID = rpid;
                                        rm.MenuID = rpm.MenuID;
                                        rm.SysID = "Test";
                                        rm.ObjectCode = "36";
                                        rm.RoleID = rl.RoleID;
                                        rm.SHO = 1;
                                        rm.NEW = 1;
                                        rm.EDT = 1;
                                        rm.DEL = 1;
                                        rm.PRN = 1;
                                        rm.SER = 1;
                                        rm.CER = 1;
                                        rm.CreatedDate = DateTime.Now;
                                        rm.CreatedUser = 1;
                                        db.RolePermissions.Add(rm);
                                    }
                                }
                            }
                            maxID = maxID + 1;
                            upf.RPermID = maxID;
                            db.RolePermissions.Add(upf);
                        }
                    }
                }
                db.SaveChanges();
                return RedirectToAction("IndexRolePermission", "RolePermissions");

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

        private User GetCurrentUser()
        {
            User us = new User();
            int userID = 0;
            try
            {
                //if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                //{
                //    userID = Convert.ToInt32(FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name);
                //}
                if (Session["UserID"] != null)
                {
                    userID = (int)Session["UserID"];
                }

                //int userID = Int32.Parse(HttpContext.User.Identity.Name);

                us = (from u in db.Users
                      where u.UserID == userID
                      select u).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
            return us;
        }

        // GET: RolePermissions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RolePermission rolePermission = await db.RolePermissions.FindAsync(id);
            if (rolePermission == null)
            {
                return HttpNotFound();
            }
            ViewBag.MenuID = new SelectList(db.MenuItems, "MenuID", "MenuName", rolePermission.MenuID);
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", rolePermission.RoleID);
            return View(rolePermission);
        }

        // POST: RolePermissions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RPermID,RoleID,SysID,MenuID,ObjectCode,SHO,NEW,EDT,DEL,PRN,SER,CER,CreatedDate,CreatedUser")] RolePermission rolePermission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rolePermission).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MenuID = new SelectList(db.MenuItems, "MenuID", "MenuName", rolePermission.MenuID);
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", rolePermission.RoleID);
            return View(rolePermission);
        }

        // GET: RolePermissions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RolePermission rolePermission = await db.RolePermissions.FindAsync(id);
            if (rolePermission == null)
            {
                return HttpNotFound();
            }
            return View(rolePermission);
        }

        // POST: RolePermissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RolePermission rolePermission = await db.RolePermissions.FindAsync(id);
            db.RolePermissions.Remove(rolePermission);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //Get role permisions of considering role
        public ActionResult IndexRolePermission(string id)
        {
            try
            {
                List<RolePermissionModel> lstRPM = new List<RolePermissionModel>();
                string userID = "0";
                //string userName = HttpContext.User.Identity.Name;

                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    userID = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                }

                //  Role role = GetCurrentRole(id);

                var obj = from rp in db.RolePermissions
                          where rp.RoleID == userID
                          orderby rp.MenuItem.MenuID
                          select rp;

                foreach (var k in obj)
                {
                    RolePermissionModel rpm = new RolePermissionModel();
                    rpm.RPermID = k.RPermID;
                    rpm.RoleID = k.RoleID;
                    rpm.SysID = k.SysID;
                    rpm.MenuID = k.MenuID != null ? (Int32)k.MenuID : 0;
                    rpm.MenuName = k.MenuItem.MenuName;
                    rpm.RoleID = k.RoleID;
                    rpm.SysID = k.SysID;
                    rpm.SHO = k.SHO == 1 ? true : false;
                    rpm.NEW = k.NEW == 1 ? true : false;
                    rpm.EDT = k.EDT == 1 ? true : false;
                    rpm.DEL = k.DEL == 1 ? true : false;
                    rpm.PRN = k.PRN == 1 ? true : false;
                    rpm.SER = k.SER == 1 ? true : false;
                    rpm.CER = k.CER == 1 ? true : false;
                    rpm.CreatedDate = k.CreatedDate;
                    rpm.CreatedUser = k.CreatedUser;

                    lstRPM.Add(rpm);
                }

                return View(lstRPM);
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        //Find relvent role by using cookie
        private Role GetCurrentRole()
        {
            Role role = new Role();
            try
            {
                string userName = "";
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    userName = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                }


                role = (from rl in db.Roles
                        where rl.RoleID == userName
                        select rl).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
            return role;
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
