using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Eregister.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Eregister.App_Start;
using System.Net;

namespace Eregister.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        #region Variables

        public static string EConfUser { get; set; }
        public static string connection = GetConnectionString("DefaultConnection");
        public static string command = null;
        public static string parameterName = null;
        public static string methodName = null;
        string codeType = null;

        public static string OEmail { get; set; }
        public static string OBirthday { get; set; }
        public static string OFname { get; set; }
        public static string OLname { get; set; }

        #endregion Variables

        #region Login
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            var custEmailConf = EmailConfirmation(model.LoginUsername);
            var custUserName = FindUserName(model.LoginUsername);
            var result = await SignInManager.PasswordSignInAsync(model.LoginUsername, model.LoginPassword, model.RememberMe, shouldLockout: false);
            if (custEmailConf == false && custUserName != null && result.ToString() == "Success")
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                EConfUser = model.LoginUsername;
                return RedirectToAction("EmailConfirmationFailed", "Account");
            }
            else
            {
                ViewBag.ReturnUrl = returnUrl;
                if (ModelState.IsValid)
                {
                    switch (result)
                    {
                        case SignInStatus.Success:
                            UpdateLastLoginDate(model.LoginUsername);
                            return RedirectToLocal(returnUrl);
                        case SignInStatus.LockedOut:
                            return View("Lockout");
                        case SignInStatus.RequiresVerification:
                            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                        case SignInStatus.Failure:
                        default:
                            ModelState.AddModelError("", "Invalid login attempt.");
                            return View("Login");
                    }
                }
                // If we got this far, something failed, redisplay form
                return View("Login");
            }
        }

        // login z ProjektMVC
        // POST: /Account/Login
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    // This doesn't count login failures towards account lockout
        //    // To enable password failures to trigger account lockout, change to shouldLockout: true
        //    var result = await SignInManager.PasswordSignInAsync(model.LoginUsername, model.LoginPassword, model.RememberMe, shouldLockout: false);
        //    switch (result)
        //    {
        //        case SignInStatus.Success:
        //            return RedirectToLocal(returnUrl);
        //            //updatelogin
        //           // return RedirectToAction("Lista", "Posts");
        //        case SignInStatus.LockedOut:
        //            return View("Lockout");
        //        case SignInStatus.RequiresVerification:
        //            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
        //        case SignInStatus.Failure:
        //        default:
        //            ModelState.AddModelError("", "Invalid login attempt.");
        //            return View("Login");

        //    }
        //}
        #endregion Login

        #region EnterCode
        #endregion

        #region Register

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
                //TODO
                //var custEmail = FindEmail(model.RegisterEmail);
                //var custUserName = FindUserName(model.RegisterUsername);

                string custEmail = null;
                string custUserName = null;

                string imageUrl = @"https://wiki.jenkins.io/images/icons/profilepics/default.png";
                byte[] imageBytes;
                HttpWebRequest imageRequest = (HttpWebRequest)WebRequest.Create(imageUrl);
                WebResponse imageResponse = imageRequest.GetResponse();

                Stream responseStream = imageResponse.GetResponseStream();

                using (BinaryReader br = new BinaryReader(responseStream))
                {
                    imageBytes = br.ReadBytes(500000);
                    br.Close();
                }
                responseStream.Close();
                imageResponse.Close();


                var user = new ApplicationUser
                {
                    UserName = model.RegisterUsername,
                    Email = model.RegisterEmail,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Country = model.Country,
                    BirthDate = model.BirthDate,
                    JoinDate = DateTime.Now,
                    EmailLinkDate = DateTime.Now,
                    LastLoginDate = DateTime.Now,
                    NameSurname = model.FirstName + " " + model.LastName,

                    CustomSkin = "green",
                    TokenValue = GetAuthorizationTokenValue("Student"),
                    TokenValueAdv = GetAuthorizationTokenValue("Teacher"),
                    TokenIsValid = true,
                    TokenAdvIsValid = true,
                    PersonSex = model.Sex,
                    EmailConfirmed = true,
                    ImageByte = imageBytes
                };

                var result = await UserManager.CreateAsync(user, model.RegisterPassword);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, "Candidate");

                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", "Home");
            }

            else
                return View();
            //return RedirectToAction("Fail", "Home");
        }
        //    if (custEmail == null && custUserName == null)
        //    {
        //        var result = await UserManager.CreateAsync(user, model.RegisterPassword);
        //        if (result.Succeeded)
        //        {
        //            UserManager.AddToRole(user.Id, "Candidate");
        //            // Send an email with this link
        //            codeType = "EmailConfirmation";
        //            await SendEmail("ConfirmEmail", "Account", user, model.RegisterEmail, "WelcomeEmail", "Confirm your account");
        //            return RedirectToAction("ConfirmationEmailSent", "Account");
        //        }
        //        AddErrors(result);
        //    }
        //    else
        //    {
        //        if (custEmail != null)
        //        {
        //            ModelState.AddModelError("", "Email is already registered.");
        //        }
        //        if (custUserName != null)
        //        {
        //            ModelState.AddModelError("", "Username " + model.RegisterUsername.ToLower() + " is already taken.");
        //        }
        //    }
        //}
        //// If we got this far, something failed, redisplay form
        //return View(model);
        //}
        #region Index


        #endregion


        #endregion Register

        #region ConfirmEmail

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, DateTime date, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return RedirectToAction("ConfirmationLinkExpired", "Account");
            }
            var emailConf = EmailConfirmationById(userId);
            if (emailConf == true)
            {
                return RedirectToAction("ConfirmationLinkUsed", "Account");
            }
            if (date != null)
            {
                if (date.AddMinutes(1) < DateTime.Now)
                {
                    return RedirectToAction("ConfirmationLinkExpired", "Account");
                }
                else
                {
                    var result = await UserManager.ConfirmEmailAsync(userId, code);
                    return View(result.Succeeded ? "ConfirmEmail" : "Error");
                }
            }
            else
            {
                return View("Error");
            }

        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendConfirmationMail()
        {
            string res = null;
            //string connection = GetConnectionString("DefaultConnection");
            string connection = GetConnectionString("IdentityConnection");
            using (SqlConnection myConnection = new SqlConnection(connection))
            using (SqlCommand cmd = new SqlCommand("SELECT Email AS Email FROM AspNetUsers WHERE UserName = @UserName", myConnection))
            {
                cmd.Parameters.AddWithValue("@UserName", EConfUser);
                myConnection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        // Read advances to the next row.
                        if (reader.Read())
                        {
                            // To avoid unexpected bugs access columns by name.
                            res = reader["Email"].ToString();
                            var user = await UserManager.FindByEmailAsync(res);
                            UpdateEmailLinkDate(EConfUser);
                            codeType = "EmailConfirmation";
                            await SendEmail("ConfirmEmail", "Account", user, res, "WelcomeEmail", "Confirm your account");
                        }
                        myConnection.Close();
                    }
                }
            }
            return RedirectToAction("ConfirmationEmailSent", "Account");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult EmailConfirmationFailed()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ConfirmationEmailSent()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ConfirmationLinkExpired()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ConfirmationLinkUsed()
        {
            return View();
        }


        #endregion ConfirmEmail

        #region SendEmail

        public async Task SendEmail(string actionName, string controllerName, ApplicationUser user, string email, string emailTemplate, string emailSubject)
        {
            string code = null;
            if (codeType == "EmailConfirmation")
            {
                code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            }
            else if (codeType == "ResetPassword")
            {
                code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
            }
            var callbackUrl = Url.Action(actionName, controllerName, new { userId = user.Id, date = DateTime.Now, code = code }, protocol: Request.Url.Scheme);
            var message = await EMailTemplate(emailTemplate);
            message = message.Replace("@ViewBag.Name", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user.FirstName));
            message = message.Replace("@ViewBag.Link", callbackUrl);
            await MessageServices.SendEmailAsync(email, emailSubject, message);
        }


        #endregion SendEmail


        #region VerifyCode

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        #endregion VerifyCode

        #region ForgotPassword

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        #endregion ForgotPassword

        #region ResetPassword

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        #endregion ResetPassword

        #region SendCode

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        #endregion SendCode

        #region LogOff

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        #endregion LogOff

        #region Helpers

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
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
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        public static async Task<string> EMailTemplate(string template)
        {
            var templateFilePath = HostingEnvironment.MapPath("~/Content/templates/") + template + ".cshtml";
            StreamReader objstreamreaderfile = new StreamReader(templateFilePath);
            var body = await objstreamreaderfile.ReadToEndAsync();
            objstreamreaderfile.Close();
            return body;
        }

        public static string GetConnectionString(string connection)
        {
            return ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        }

        public static string ReturnString(string str)
        {
            string strOut = null;
            using (SqlConnection myConnection = new SqlConnection(connection))
            using (SqlCommand cmd = new SqlCommand(command, myConnection))
            {
                cmd.Parameters.AddWithValue(parameterName, str);
                myConnection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            if (methodName == "FindEmail")
                            {
                                strOut = reader["Email"].ToString();
                            }
                            else if (methodName == "FindUserName" || methodName == "FindUserNameById")
                            {
                                strOut = reader["UserName"].ToString();
                            }
                            else if (methodName == "FindUserId")
                            {
                                strOut = reader["UserId"].ToString();
                            }
                        }
                        myConnection.Close();
                    }
                    return strOut;
                }
            }
        }
        public static string FindEmail(string email)
        {
            command = "SELECT Email AS Email FROM AspNetUsers WHERE Email = @Email";
            parameterName = "@Email";
            methodName = "FindEmail";
            return ReturnString(email);
        }
        public string FindUserName(string username)
        {
            command = "SELECT UserName AS UserName FROM AspNetUsers WHERE UserName = @UserName";
            parameterName = "@UserName";
            methodName = "FindUserName";
            return ReturnString(username);
        }
        public string FindUserNameById(string userid)
        {
            command = "SELECT UserName AS UserName FROM AspNetUsers WHERE Id = @Id";
            parameterName = "@Id";
            methodName = "FindUserNameById";
            return ReturnString(userid);
        }
        public string FindUserId(string userprokey)
        {
            command = "SELECT UserId AS UserId FROM AspNetUserLogins WHERE ProviderKey = @ProviderKey";
            parameterName = "@ProviderKey";
            methodName = "FindUserId";
            return ReturnString(userprokey);
        }


        public bool ReturnBool(string str)
        {
            bool econfOut = false;
            string res = null;
            using (SqlConnection myConnection = new SqlConnection(connection))
            using (SqlCommand cmd = new SqlCommand(command, myConnection))
            {
                cmd.Parameters.AddWithValue(parameterName, str);
                myConnection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            res = reader["EmailConfirmed"].ToString();
                            if (res == "False")
                            {
                                econfOut = false;
                            }
                            else
                            {
                                econfOut = true;
                            }
                        }
                        myConnection.Close();
                    }
                    return econfOut;
                }
            }
        }
        public bool EmailConfirmation(string username)
        {
            command = "SELECT EmailConfirmed AS EmailConfirmed FROM AspNetUsers WHERE UserName = @UserName";
            parameterName = "@UserName";
            return ReturnBool(username);
        }
        public bool EmailConfirmationById(string userid)
        {
            command = "SELECT EmailConfirmed AS EmailConfirmed FROM AspNetUsers WHERE Id = @Id";
            parameterName = "@Id";
            return ReturnBool(userid);
        }

        public static int UpdateDatabase(string username)
        {
            using (SqlConnection myConnection = new SqlConnection(connection))
            using (SqlCommand cmd = new SqlCommand(command, myConnection))
            {
                cmd.Parameters.AddWithValue(parameterName, username);
                myConnection.Open();
                return cmd.ExecuteNonQuery();
            }
        }
        //public static int UpdateEmailLinkDate(string username)
        //{
        //    command = "UPDATE AspNetUsers SET EmailLinkDate = '" + DateTime.Now + "' WHERE UserName = @UserName";
        //    parameterName = "@UserName";
        //    return UpdateDatabase(username);
        //}

        public static int UpdateEmailLinkDate(string username)
        {
            command = "UPDATE AspNetUsers SET EmailLinkDate = GETDATE() WHERE UserName = @UserName";
            parameterName = "@UserName";
            return UpdateDatabase(username);
        }

        //public static int UpdateLastLoginDate(string username)
        //{
        //    command = "UPDATE AspNetUsers SET LastLoginDate = '" + DateTime.Now + "' WHERE UserName = @UserName";
        //    parameterName = "@UserName";
        //    return UpdateDatabase(username);
        //}

        public static int UpdateLastLoginDate(string username)
        {
            command = "UPDATE AspNetUsers SET LastLoginDate = GETDATE() WHERE UserName = @UserName";
            parameterName = "@UserName";
            return UpdateDatabase(username);
        }

        public static IEnumerable<SelectListItem> GetCountries()
        {
            RegionInfo country = new RegionInfo(new CultureInfo("en-US", false).LCID);
            List<SelectListItem> countryNames = new List<SelectListItem>();
            string cult = CultureInfo.CurrentCulture.EnglishName;
            string count = cult.Substring(cult.IndexOf('(') + 1,
                             cult.LastIndexOf(')') - cult.IndexOf('(') - 1);
            //To get the Country Names from the CultureInfo installed in windows
            foreach (CultureInfo cul in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                country = new RegionInfo(new CultureInfo(cul.Name, false).LCID);
                countryNames.Add(new SelectListItem()
                {
                    Text = country.DisplayName,
                    Value = country.DisplayName,
                    Selected = count == country.EnglishName
                });
            }
            //Assigning all Country names to IEnumerable
            IEnumerable<SelectListItem> nameAdded =
                countryNames.GroupBy(x => x.Text).Select(
                    x => x.FirstOrDefault()).ToList<SelectListItem>()
                    .OrderBy(x => x.Text);
            return nameAdded;
        }

        public static IEnumerable<SelectListItem> GetSex()
        {
            List<SelectListItem> sexNames = new List<SelectListItem>();
            List<Sex> sexes = new List<Sex>() { Sex.M, Sex.K};

            foreach (var sex in sexes)
            {
                sexNames.Add(new SelectListItem()
                {
                    Text = sex.ToString(),
                    Value = sex.ToString(),
                });

            }

            IEnumerable<SelectListItem> nameAdded =
                sexNames.GroupBy(x => x.Text).Select(
                  x => x.FirstOrDefault()).ToList<SelectListItem>()
                 .OrderBy(x => x.Text);

            return nameAdded;
        }


        #endregion

        #region Token
        public string GetAuthorizationTokenValue(string role)
        {
            var context = new ApplicationDbContext();
            var token = context.Tokens.Where(x => x.Role == role).FirstOrDefault();
            return token.Code;
        }
        #endregion

    }
}