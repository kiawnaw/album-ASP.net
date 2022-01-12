using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AlbumsApp.Models;

namespace AlbumsApp.Models.ViewModels
{
    public class AddOrEditAlbumViewModel : Album
    {
        public AddOrEditAlbumViewModel()
        {
        }

        public AddOrEditAlbumViewModel(Album album)
        {
            this.AlbumId = album.AlbumId;
            this.Name = album.Name;
            this.YearProduced = album.YearProduced;
            this.Rating = album.Rating;
            this.StudioId = album.StudioId;
            this.Studio = album.Studio;
        }

        public List<Studio> AllStudios { get; set; }
    }
}
