using System;
using System.Collections.Generic;
using System.Linq;

using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

using ImageSharingWithSecurity.DAL;
using ImageSharingWithSecurity.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ImageSharingWithSecurity.Controllers
{
    public class ImagesController : BaseController
    {
        private readonly IWebHostEnvironment hostingEnvironment;

        private readonly ILogger<ImagesController> logger;

        // Dependency injection
        public ImagesController(UserManager<ApplicationUser> userManager,
                                ApplicationDbContext db,
                                IWebHostEnvironment environment,
                                ILogger<ImagesController> logger)
            : base(userManager, db)
        {
            this.hostingEnvironment = environment;
            this.logger = logger;
        }

        protected void mkDirectories()
        {
            var dataDir = Path.Combine(hostingEnvironment.WebRootPath,
               "data", "images");
            if (!Directory.Exists(dataDir))
            {
                Directory.CreateDirectory(dataDir);
            }
        }

        protected string imageDataFile(int id)
        {
            return Path.Combine(
               hostingEnvironment.WebRootPath,
               "data", "images", "img-" + id + ".jpg");
        }

        public static string imageContextPath(int id)
        {
            return "data/images/img-" + id + ".jpg";
        }


        [HttpGet]
        [Authorize]
        public ActionResult Upload()
        {
            CheckAda();

            ViewBag.Message = "";
            ImageView imageView = new ImageView();
            imageView.Tags = new SelectList(db.Tags, "Id", "Name", 1);
            return View(imageView);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Upload(ImageView imageView)
        {
            CheckAda();

            await TryUpdateModelAsync(imageView);

            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Please correct the errors in the form!";
                ViewBag.Tags = new SelectList(db.Tags, "Id", "Name", 1);
                return View(imageView);
            }

            ApplicationUser user = await GetLoggedInUser();

            if (imageView.ImageFile == null || imageView.ImageFile.Length <= 0)
            {
                ViewBag.Message = "No image file specified!";
                imageView.Tags = new SelectList(db.Tags, "Id", "Name", 1);
                return View(imageView);
            }

            Image image = new Image();
            image.Caption = imageView.Caption;
            image.Description = imageView.Description;
            image.DateTaken = (DateTime)imageView.DateTaken;
            image.User = user;
            image.TagId = imageView.TagId;
            image.Approved = false;
            image.Tag = await db.Tags.FirstAsync(t => t.Id == imageView.TagId);

            await db.Images.AddAsync(image);
            await db.SaveChangesAsync();

            mkDirectories();

            await imageView.ImageFile.CopyToAsync(new FileStream(imageDataFile(image.Id), FileMode.Create));

            return RedirectToAction("Details", new { Id = image.Id });
        }

        [HttpGet]
        [Authorize]
        public ActionResult Query()
        {
            CheckAda();

            ViewBag.Message = "";
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Details(int Id)
        {
            CheckAda();

            Image image = db.Images.Find(Id);
            if (image == null)
            {
                return RedirectToAction("Error", "Home", new { ErrId = "Details:" + Id });
            }

            ImageView imageView = new ImageView();
            imageView.Id = image.Id;
            imageView.Caption = image.Caption;
            imageView.Description = image.Description;
            imageView.DateTaken = image.DateTaken;
            /*
             * Eager loading of related entities
             */
            var imageEntry = db.Entry(image);
            imageEntry.Reference(i => i.Tag).Load();
            imageEntry.Reference(i => i.User).Load();
            imageView.TagName = image.Tag.Name;
            imageView.Username = image.User.UserName;
            return View(imageView);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Edit(int Id)
        {
            CheckAda();
            ApplicationUser user = await GetLoggedInUser();

            Image image = db.Images.Find(Id);
            if (image == null)
            {
                return RedirectToAction("Error", "Home", new { ErrId = "EditNotFound" });
            }

            db.Entry(image).Reference(im => im.User).Load();  // Eager load of user
            if (!image.User.UserName.Equals(user.UserName))
            {
                return RedirectToAction("Error", "Home", new { ErrId = "EditNotAuth" });
            }

            ViewBag.Message = "";

            ImageView imageView = new ImageView();
            imageView.Tags = new SelectList(db.Tags, "Id", "Name", image.TagId);
            imageView.Id = image.Id;
            imageView.TagId = image.TagId;
            imageView.Caption = image.Caption;
            imageView.Description = image.Description;
            imageView.DateTaken = image.DateTaken;

            return View("Edit", imageView);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DoEdit(int Id, ImageView imageView)
        {
            CheckAda();
            ApplicationUser user = await GetLoggedInUser();

            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Please correct the errors on the page";
                imageView.Id = Id;
                imageView.Tags = new SelectList(db.Tags, "Id", "Name", imageView.TagId);
                return View("Edit", imageView);
            }

            logger.LogDebug("Saving changes to image " + Id);
            Image image = db.Images.Find(Id);
            if (image == null)
            {
                return RedirectToAction("Error", "Home", new { ErrId = "EditNotFound" });
            }

            db.Entry(image).Reference(im => im.User).Load();  // Explicit load of user
            if (!image.User.UserName.Equals(user.UserName))
            {
                return RedirectToAction("Error", "Home", new { ErrId = "EditNotAuth" });
            }

            image.TagId = imageView.TagId;
            image.Caption = imageView.Caption;
            image.Description = imageView.Description;
            image.DateTaken = imageView.DateTaken;
            db.Entry(image).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details", new { Id = Id });
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Delete(int Id)
        {
            CheckAda();
            ApplicationUser user = await GetLoggedInUser();

            Image image = db.Images.Find(Id);
            if (image == null)
            {
                return RedirectToAction("Error", "Home", new { ErrId = "Delete" });
            }

            db.Entry(image).Reference(im => im.User).Load();  // Explicit load of user
            if (!image.User.UserName.Equals(user.UserName))
            {
                return RedirectToAction("Error", "Home", new { ErrId = "DeleteNotAuth" });
            }

            ImageView imageView = new ImageView();
            imageView.Id = image.Id;
            imageView.Caption = image.Caption;
            imageView.Description = image.Description;
            imageView.DateTaken = image.DateTaken;
            /*
             * Eager loading of related entities
             */
            db.Entry(image).Reference(i => i.Tag).Load();
            imageView.TagName = image.Tag.Name;
            imageView.Username = image.User.UserName;
            return View(imageView);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DoDelete(int Id)
        {
            CheckAda();
            ApplicationUser user = await GetLoggedInUser();

            Image image = db.Images.Find(Id);
            if (image == null)
            {
                return RedirectToAction("Error", "Home", new { ErrId = "DeleteNotFound" });
            }

            db.Entry(image).Reference(im => im.User).Load();  // Explicit load of user
            if (!image.User.UserName.Equals(user.UserName))
            {
                return RedirectToAction("Error", "Home", new { ErrId = "DeleteNotAuth" });
            }

            //db.Entry(imageEntity).State = EntityState.Deleted;
            db.Images.Remove(image);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> ListAll()
        {
            CheckAda();
            ApplicationUser user = await GetLoggedInUser();

            IList<Image> images = ApprovedImages()
                .Include(im => im.User)
                .Include(im => im.Tag)
                .ToList();
            ViewBag.UserName = user.UserName;
            return View(images);
        }

        [HttpGet]
        [Authorize]
        public IActionResult ListByUser()
        {
            CheckAda();

            var viewModel = new ListByUserModel();
            viewModel.Users = new SelectList(userManager.Users.ToList(), "Id", "UserName", 0);
            return View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> DoListByUser(ListByUserModel userView)
        {
            CheckAda();

            if (ModelState.IsValid)
            {
                var selectedUserImages = ApprovedImages()
                    .Include(i => i.User)
                    .Include(i => i.Tag)
                    .Where(i => i.User.Id == userView.Id)
                    .ToList();

                var currentUser = await GetLoggedInUser();
                ViewBag.UserName = currentUser.UserName;
                return View("ListAll", selectedUserImages);
            }

            ViewBag.Message = "Error with selection";
            return View(userView);
        }

        [HttpGet]
        [Authorize]
        public ActionResult ListByTag()
        {
            CheckAda();

            ListByTagModel tagView = new ListByTagModel();
            tagView.Tags = new SelectList(db.Tags, "Id", "Name", 1);
            return View(tagView);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> DoListByTag(ListByTagModel tagView)
        {
            CheckAda();
            ApplicationUser user = await GetLoggedInUser();

            Tag tag = db.Tags.Find(tagView.Id);
            if (tag == null)
            {
                return RedirectToAction("Error", "Home", new { ErrId = "ListByTag" });
            }

            ViewBag.UserName = user.UserName;
            /*
             * Eager loading of related entities
             */
            var images = db.Entry(tag).Collection(t => t.Images).Query().Where(im => im.Approved).Include(im => im.User).ToList();
            return View("ListAll", tag.Images);
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Approve()
        {
            CheckAda();

            ViewBag.message = "";
            var userRoles = await userManager.GetRolesAsync(await GetLoggedInUser());
            bool isApprover = false;

            foreach (var role in userRoles)
            {
                if (role == "Admin" || role == "Approver")
                {
                    isApprover = true;
                    break;
                }
            }

            if (!isApprover)
            {
                ViewBag.isApprover = false;
                ViewBag.Message = "You are not allowed to approve images";
                return View(new ApproveModel { Images = new List<SelectListItem>() });
            }
            else
            {
                ViewBag.isApprover = true;
            }

            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var im in db.Images)
            {
                SelectListItem item = new SelectListItem { Text = im.Caption, Value = im.Id.ToString(), Selected = im.Approved };
                items.Add(item);
            }


            ApproveModel model = new ApproveModel { Images = items };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Approver,Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(ApproveModel model)
        {
            CheckAda();

            foreach (var item in model.Images.ToList())
            {
                Image image = db.Images.Find(int.Parse(item.Value));

                if (item.Selected)
                {
                    image.Approved = true;
                    model.Images.Remove(item);
                }
                else
                {
                    item.Text = image.Caption;
                }
            }

            await db.SaveChangesAsync();

            ViewBag.message = "Images approved!";

            return View(model);
        }
    }

}
