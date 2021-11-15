using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using ImageSharingWithCloudStorage.DAL;
using ImageSharingWithCloudStorage.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace ImageSharingWithCloudStorage.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        protected SignInManager<ApplicationUser> signInManager;

        private readonly ILogger<AccountController> logger;

        protected IImageStorage images;

        // Dependency injection of DB context and user/signin managers
        public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 ApplicationDbContext db,
                                 IImageStorage images,
                                 ILogger<AccountController> logger)
            : base(userManager, db)
        {
            this.signInManager = signInManager;
            this.logger = logger;
            this.images = images;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            CheckAda();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            CheckAda();

            if (ModelState.IsValid)
            {
                logger.LogDebug("Registering user: " + model.Email);
                IdentityResult result = null;
                // TODO register the user from the model, and log them in

                ApplicationUser user = null;

                if (result.Succeeded)
                {
                    logger.LogDebug("...registration succeeded.");
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home", new { UserName = model.Email });
                }
                else
                {
                    logger.LogDebug("...registration failed.");
                    ModelState.AddModelError(string.Empty, "Registration failed");
                }

            }

            // If we got this far, something failed, redisplay form
            return View(model);

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            CheckAda();
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl)
        {
            CheckAda();
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // TODO log in the user from the model (make sure they are still active)
            var user = await db.Users.Where(u => u.Email == model.UserName).FirstOrDefaultAsync();

            var activeUsers = ActiveUsers();
            bool found = false;

            foreach (var u in activeUsers)
            {
                if (u.Email == user.Email)
                {
                    found = true;
                    break;
                }
            }
            if (found == true)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    SaveADACookie(db.Users.Where(u => u.Email == user.Email).Select(u => u.ADA).First());
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    // incorrect credentials
                    ViewBag.Message = "Incorrect username or password";
                    return View(model);
                }
            }

            ViewBag.Message = "User is not an active user";

            return View(model);
        }

        //
        // GET: /Account/Password

        [HttpGet]
        public ActionResult Password(PasswordMessageId? message)
        {
            CheckAda();
            ViewBag.StatusMessage =
                 message == PasswordMessageId.ChangePasswordSuccess ? "Your password has been changed."
                 : message == PasswordMessageId.SetPasswordSuccess ? "Your password has been set."
                 : message == PasswordMessageId.RemoveLoginSuccess ? "The external login was removed."
                 : "";
            ViewBag.ReturnUrl = Url.Action("Password");
            return View();
        }

        //
        // POST: /Account/Password

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Password(LocalPasswordModel model)
        {
            CheckAda();

            ViewBag.ReturnUrl = Url.Action("Password");
            if (ModelState.IsValid)
            {
                // TODO change the password
                ApplicationUser user = await GetLoggedInUser();
                IdentityResult idResult = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);


                if (idResult.Succeeded)
                {
                    return RedirectToAction("Password", new { Message = PasswordMessageId.ChangePasswordSuccess });
                }
                else
                {
                    ModelState.AddModelError("", "The new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Manage()
        {
            CheckAda();

            List<SelectListItem> users = new List<SelectListItem>();
            foreach (var u in db.Users)
            {
                SelectListItem item = new SelectListItem { Text = u.UserName, Value = u.Id, Selected = u.Active };
                users.Add(item);
            }

            ViewBag.message = "";
            ManageModel model = new ManageModel { Users = users };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manage(ManageModel model)
        {
            CheckAda();

            foreach (var userItem in model.Users)
            {
                ApplicationUser user = await userManager.FindByIdAsync(userItem.Value);

                // Need to reset user name in view model before returning to user, it is not posted back
                userItem.Text = user.UserName;

                if (user.Active && !userItem.Selected)
                {
                    var images = db.Entry(user).Collection(u => u.Images).Query().ToList();
                    foreach (Image image in images)
                    {
                        await this.images.DeleteFileAsync(image.Id);
                        db.Images.Remove(image);
                    }
                    user.Active = false;
                }
                else if (!user.Active && userItem.Selected)
                {
                    /*
                     * Reactivate a user
                     */
                    user.Active = true;
                }
            }
            await db.SaveChangesAsync();

            ViewBag.message = "Users successfully deactivated/reactivated";

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logoff()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AccessDenied(string returnUrl)
        {
            CheckAda();
            ViewBag.Message = "You are not authorized to access this page";
            return View();
        }

        protected void SaveADACookie(bool value)
        {
            // TODO save the value in a cookie field key
            String ADA;
            if (value)
            {
                ADA = "true";
            }
            else
            {
                ADA = "false";
            }

            var options = new CookieOptions()
            {
                IsEssential = true,
                Expires = DateTime.Now.AddMonths(3),
                SameSite = SameSiteMode.Lax
            };

            Response.Cookies.Append("ADA", ADA, options);
            ViewBag.isAda = value;
        }

        public enum PasswordMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

    }
}
