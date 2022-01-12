using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using AlbumsApp.Models;

namespace AlbumsApp.Components
{
    public class TopRatedAlbums : ViewComponent
    {
        private const int _NumAlbums = 5;
        public TopRatedAlbums(AlbumsDbContext albumDbContext)
        {
            _albumDbContext = albumDbContext;
        }

        public IViewComponentResult Invoke(int numAlbums = 0)
        {
           
            if (numAlbums <= 0)
            {
                numAlbums = _NumAlbums;
            }
            var albums = _albumDbContext.Albums.Where(a => a.Rating >= 8.0)
                .OrderByDescending(a => a.Rating)
                .ToList();
                //.Take(numAlbums).ToList();
            
            var viewModel = new TopRatedAlbumsViewModel() {
                Albums = albums,
                NumberOfAlbumsToDisplay = Math.Min(albums.Count, numAlbums)
            };

            return View(viewModel);
        }

        private AlbumsDbContext _albumDbContext;
    }
}
