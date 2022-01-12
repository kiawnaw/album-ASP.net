using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AlbumsApp.Models;
using Microsoft.AspNetCore.Identity;

namespace AlbumsApp.Controllers
{
    public class StudioController : Controller
    {
        
        public StudioController(AlbumsDbContext albumContext, UserManager<User> userMngr, SignInManager<User> signInMngr)
        {
            signInManager = signInMngr;
            userManager = userMngr;
            _albumsDbContext = albumContext;
        }

        public IActionResult List()
        {
            List<Studio> studios = _albumsDbContext.Studios.OrderBy(s => s.Name).ToList();
            return View(studios);
        }

        [HttpGet]
        public IActionResult Add()
        {
            if (!signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login", "Account");
            }
            return View(new Studio());
        }

        [HttpPost]
        public IActionResult Add(Studio studio)
        {
            if (ModelState.IsValid)
            {
                _albumsDbContext.Studios.Add(studio);
                _albumsDbContext.SaveChanges();

                return RedirectToAction("List", "Album");
            }

            return View(studio);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login", "Account");
            }
            Studio studio = _albumsDbContext.Studios.Where(a => a.StudioId == id).FirstOrDefault();
            return View(studio);
        }

        [HttpPost]
        public IActionResult Edit(Studio studio)
        {
            if (ModelState.IsValid)
            {
                _albumsDbContext.Studios.Update(studio);
                _albumsDbContext.SaveChanges();

                return RedirectToAction("List", "Album");
            }
            return View(studio);
        }

        private AlbumsDbContext _albumsDbContext;
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
    }
}
