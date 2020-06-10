using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoviKunstUitleen.ViewModel;
using NoviKunstUitleen.Models;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net;
using System.IO;

namespace NoviKunstUitleen.Controllers
{
    [Authorize]
    public class ArtsController : Controller
    {
        private ApplicationDbContext _context;

        public ArtsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Arts
        public ActionResult Collection()
        {
            var art = _context.Arts.ToList();

            return View(art);
        }

        public ActionResult ArtsLendPeriod(string id)
        {
            if (Request.IsAjaxRequest())
            {
                int artId = Convert.ToInt32(id);
                var art = _context.Arts.SingleOrDefault(a => a.ArtsId == artId);
                if (art == null)
                {
                    return HttpNotFound();
                }
                return PartialView( art);
            }

            return PartialView("Error");

        }


        [Authorize(Roles = "Staffmember")]
        public ActionResult New()
        {
            return View();
        }

        [Authorize(Roles = "Staffmember")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save( Arts art)
        {
            //get profile photo.
            var upload = Request.Files["Image"];
            var fileName = Path.Combine(Server.MapPath("~/Image/ArtsCollection"), upload.FileName);
            if (upload.ContentLength > 0)
            {
                upload.SaveAs(fileName);
            }

            //if (!ModelState.IsValid)
            //{
            //    var viewModel = new NewArtsFormViewModel
            //    {
            //        Art = art
            //    };

            //    return View("New", viewModel);
            //}


            if (art.ArtsId == 0)
            {
                var inStock = art.NumbersInStock;
                var artValue = new Arts()
                {

                    Name = art.Name,
                    Image = "~/Image/ArtsCollection/" + upload.FileName,
                    NumbersInStock = art.NumbersInStock,
                    NumbersAvailable = art.NumbersInStock,
                    DateAdded = art.DateAdded,
                    Price = art.Price,
                    Creator = art.Creator


                };

                _context.Arts.Add(artValue);
            }
            else
            {
                var ArtinDB = _context.Arts.Single(a => a.ArtsId == art.ArtsId);
                ArtinDB.Name = art.Name;
                ArtinDB.Image = art.Image;
                ArtinDB.NumbersInStock = art.NumbersInStock;
                ArtinDB.Price = art.Price;
                ArtinDB.Creator = art.Creator;
                ArtinDB.DateAdded = art.DateAdded;
            }

            _context.SaveChanges();
            return RedirectToAction("Collection", "Arts");

        }

        [Authorize(Roles= "Staffmember")]
        public ActionResult Edit(int id)
        {
            var art = _context.Arts.SingleOrDefault(a => a.ArtsId == id);
            if(art == null)
            {
                return HttpNotFound();
            }
            var artViewModel = new NewArtsFormViewModel
            {
                Art = art
            };
            return View("ArtsForm",artViewModel);
        }

    }
}