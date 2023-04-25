using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MMS.Models;
using System.Security.Cryptography;
using System.Web.Security;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace MMS.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private MMSEntities db = new MMSEntities();

        //private ApplicationSignInManager _signInManager;
        //private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserModel user,string loclisi, string returnUrl)
        {
            try
            {
               
                SqlConnection oSqlConnection;
                SqlCommand oSqlCommand;
                SqlDataAdapter oSqlDataAdapter;
                string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MMScon"].ConnectionString;
                string sqlQuery;
                byte[] encPwd = EncriptPassword(user.Pass);
               // var obj2 = (dynamic)null;
                var obj = (from usr in db.EpasUsers
                           where usr.userid == user.UserName
                           && usr.password == encPwd
                           select usr).FirstOrDefault();


                //if (obj == null)
                //{
              

                //}

                //if (obj != null)
                //{
               
                //}


                    char[] MyChar = { '/', '"', ' ' };
                string NewString = user.UserName.ToString();
              

                var items = from s in db.Clinic_Master.Where(p => p.Clinic_ID == "") select new { s.Clinic_Detail, s.Clinic_ID,s.LocationID };
                var ser = from s in db.Users.Where(p => p.UserName == NewString) select new { s.UserID };
                var joblist = new String[100];
                // var locret1 = (dynamic)null;
                // var locret2 = (dynamic)null;
                var locret3 = (dynamic)null;
                try
                {

                    
                    foreach (var item in ser)
                    {

                        var serv = from s in db.Staff_Master.Where(p => p.UserID == item.UserID) select new { s.LOCID,s.Job_CategoryID,s.LocationID };
                                 ////////////////////////////////////////

                        int ij = 0;
                        foreach (var item1 in serv)
                        {






                            if (item1.Job_CategoryID == 2)
                            {
                                var items3 = from s in db.Clinic_Master.Where(p => p.ClinicTypeID == 19 || p.ClinicTypeID == 20) select new { s.Clinic_Detail, s.Clinic_ID,s.LocationID };
                                items = items3.Concat(items);
                                break;
                            }
                            else
                            {

                                var items2 = from s in db.Clinic_Master.Where(p => p.Clinic_ID == item1.LOCID) select new { s.Clinic_Detail, s.Clinic_ID,s.LocationID };
                                var items3 = from s in db.Vw_Formation.Where(p => p.DivisionID == item1.LOCID).Where(p => p.LocationID == item1.LocationID) select new { Clinic_Detail = s.DivisionName, Clinic_ID = s.DivisionID ,s.LocationID};
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









                    }
                }
                catch (Exception ex)
                {


                }
           var rt=     items.ToList();
                int ipos = Convert.ToInt32(loclisi);

                if (obj != null)
                {

                    var obj2 = (from usr in db.Users
                                where usr.UserName == obj.userid

                                select usr).FirstOrDefault();
                    Session["UserID"] = obj2.UserID;
                    Session["userlocid1"] = rt[ipos].Clinic_ID;
                    
                        Session["userloc"] = rt[ipos].LocationID;
                    
                    FormsAuthentication.SetAuthCookie(obj2.UserID.ToString(), false);
                    User varUser = db.Users.Find(obj2.UserID);
                    varUser.LastVisit = System.DateTime.Now;
                    db.SaveChanges();
                    Session["loginerror"] = "";
                    return RedirectToAction("Index", "Home");
                }
                if (obj == null&& user.UserName.All(Char.IsLetter))
                {
                    var obj2 = (from usr in db.Users
                                where usr.UserName == user.UserName
                                && usr.Pass == encPwd
                                select usr).FirstOrDefault();
                    Session["UserID"] = obj2.UserID;
                    Session["userlocid1"] = rt[ipos].Clinic_ID;

                    Session["userloc"] = rt[ipos].LocationID;

                    FormsAuthentication.SetAuthCookie(obj2.UserID.ToString(), false);
                    User varUser = db.Users.Find(obj2.UserID);
                    varUser.LastVisit = System.DateTime.Now;
                    db.SaveChanges();
                    Session["loginerror"] = "";
                    return RedirectToAction("Index", "Home");
                }
                //return View(returnUrl);
                return RedirectToAction("Login", "Users");
            }
            catch (Exception ex)
            {
                Session["loginerror"] = "Incorrect Username or Password!";
                return RedirectToAction("Login", "Users");
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

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser() { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}
