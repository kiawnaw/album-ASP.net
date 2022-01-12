using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using AlbumsApp.Models;
using AlbumsApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace AlbumsApp.Controllers
{
    public class AlbumController : Controller
    {
      //Kiana Karamouz
      //Webtech final exam
      //8655104
      //Jack Lai

        //defining the variables needed
       
        private const string ckiCounter = "CkiCounter";
        private const string ssnCounter = "SsnCounter";
       

        public AlbumController(AlbumsDbContext albumContext, UserManager<User> userMngr, SignInManager<User> signInMngr)
        {
            signInManager = signInMngr;
            userManager = userMngr;
            _albumsDbContext = albumContext;
        }

        [Route("/")]
        public IActionResult List()
        {
            var albums = _albumsDbContext.Albums.Include(a => a.Studio).OrderByDescending(a => a.YearProduced).ToList();
            //Visits Count With COOKIE
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(ssnCounter)))
            {
                HttpContext.Session.SetString(ssnCounter, "1");
                
            }
            if (!Request.Cookies.ContainsKey(ckiCounter))
            {
                Response.Cookies.Append(ckiCounter, "1");
                
            }

            string currentSessionCounter = HttpContext.Session.GetString(ssnCounter);
            string currentCookieCounter = Request.Cookies[ckiCounter] ?? "0";
            int updatedSessionCounter = int.Parse(currentSessionCounter) + 1;
            int updatesCookieCounter = int.Parse(currentCookieCounter) + 1;

            HttpContext.Session.SetString(ssnCounter, updatedSessionCounter.ToString());
            HttpContext.Response.Cookies.Append(ckiCounter, updatesCookieCounter.ToString());

            ViewData["VisitorMSG"] = "You have visited this page a total of "
                                     +updatesCookieCounter.ToString() + ", and "
                                     + updatedSessionCounter.ToString() +
                                     " of those visits are for this current session";
            
            return View(albums);
        }

        public IActionResult ListByAlbums(int id)
        {
            var albums = _albumsDbContext.Albums.Include(a => a.Studio).Where(a  =>a.Studio.StudioId == id).OrderByDescending(a => a.YearProduced).ToList();
            return View(albums);
        }

        [HttpGet]
        public IActionResult Add()
        {
            if (!signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login", "Account");
            }
            var viewModel = new AddOrEditAlbumViewModel();
            viewModel.AllStudios = GetAllStudios();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Add(AddOrEditAlbumViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _albumsDbContext.Albums.Add(viewModel);
                _albumsDbContext.SaveChanges();

                return RedirectToAction("List", "Album");
            }
            else
            {
                ModelState.AddModelError("", "There were errors in the form - please fix them and try adding again.");
                viewModel.AllStudios = GetAllStudios();
                return View(viewModel);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login", "Account");
            }
            AddOrEditAlbumViewModel viewModel = new AddOrEditAlbumViewModel(_albumsDbContext.Albums.Include(a => a.Studio).Where(a => a.AlbumId == id).FirstOrDefault());
            viewModel.AllStudios = GetAllStudios();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(AddOrEditAlbumViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _albumsDbContext.Albums.Update(viewModel);
                _albumsDbContext.SaveChanges();

                return RedirectToAction("List", "Album");
            }
            else
            {
                ModelState.AddModelError("", "There were errors in the form - please fix them and try updating again.");
                viewModel.AllStudios = GetAllStudios();
                return View(viewModel);
            }
        }

        [HttpGet]
        public IActionResult DeleteConfirmation(int id)
        {
            if (!signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login", "Account");
            }
            Album album = _albumsDbContext.Albums.Include(a => a.Studio).Where(a => a.AlbumId == id).FirstOrDefault();
            return View(album);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Album album = _albumsDbContext.Albums.Include(a => a.Studio).Where(a => a.AlbumId == id).FirstOrDefault();
            _albumsDbContext.Albums.Remove(album);
            _albumsDbContext.SaveChanges();

            return RedirectToAction("List", "Album");
        }

        [HttpGet("/Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private List<Studio> GetAllStudios()
        {
            return _albumsDbContext.Studios.OrderBy(s => s.Name).ToList();
        }

        private AlbumsDbContext _albumsDbContext;
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
    }
}
