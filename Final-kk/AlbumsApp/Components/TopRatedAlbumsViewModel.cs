using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AlbumsApp.Models;

namespace AlbumsApp.Components
{
    public class TopRatedAlbumsViewModel
    {
        public List<Album> Albums { get; set; }

        public int NumberOfAlbumsToDisplay { get; set; }
    }
}
