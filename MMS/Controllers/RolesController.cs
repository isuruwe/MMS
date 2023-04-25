using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;

using System.Web.Security;

namespace MMS.Controllers
{
    public class RolesController : Controller
    {
        private MMSEntities db = new MMSEntities();

        // GET: Roles
        public async Task<ActionResult> Index()
        {
            var roles = db.Roles.Include(r => r.UserRole_Type);
            return View(await roles.ToListAsync());
        }

        // GET: Roles/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = await db.Roles.FindAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            ViewBag.RoleType = new SelectList(db.UserRole_Type, "UserRoleType_ID", "UserRoleType_Name");
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RoleID,RoleName,RoleType,Status,SchemaType,CreatedDate,CreatedUser,ModifiedDate,ModifiedUser")] Role role)
        {
            if (ModelState.IsValid)
            {
                int userID = 0;
                if (Session["UserID"] != null)
                {
                    userID = (int)Session["UserID"];
                }

                role.CreatedDate = System.DateTime.Now;
                role.CreatedUser = userID;
                db.Roles.Add(role);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.RoleType = new SelectList(db.UserRole_Type, "UserRoleType_ID", "UserRoleType_Name", role.RoleType);
            return View(role);
        }

        // GET: Roles/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = await db.Roles.FindAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleType = new SelectList(db.UserRole_Type, "UserRoleType_ID", "UserRoleType_Name", role.RoleType);
            return View(role);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RoleID,RoleName,RoleType,Status,SchemaType,CreatedDate,CreatedUser,ModifiedDate,ModifiedUser")] Role role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.RoleType = new SelectList(db.UserRole_Type, "UserRoleType_ID", "UserRoleType_Name", role.RoleType);
            return View(role);
        }

        // GET: Roles/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = await db.Roles.FindAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Role role = await db.Roles.FindAsync(id);
            db.Roles.Remove(role);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //Redirect to view list of role's permission action of 'IndexRolePermission' Controller
        public ActionResult ListRolePermission(string id)
        {
            try
            {
                Role role = new Role();
                role = (from rl in db.Roles
                        where rl.RoleID == id
                        select rl).FirstOrDefault();
                FormsAuthentication.SetAuthCookie(role.RoleID.ToString(), false);


                return RedirectToAction("IndexRolePermission", "RolePermissions", new { roleID = id });
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        //Redirect to permission assigning action of 'RolePermission' Controller
        public ActionResult AssignRolePermission(string id)
        {
            try
            {
                Role role = new Role();
                role = (from rl in db.Roles
                        where rl.RoleID == id
                        select rl).FirstOrDefault();
                //FormsAuthentication.SetAuthCookie(role.RoleID, false);
                FormsAuthentication.SetAuthCookie(role.RoleID.ToString(), false);

                return RedirectToAction("Create", "RolePermissions", new { roleID = id });
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
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
