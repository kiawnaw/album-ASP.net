using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AlbumsApp.Models
{
    public class Album
    {
        public int AlbumId { get; set; }

        [Required(ErrorMessage = "Album name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Year Produced is required.")]
        [Range(1800, int.MaxValue, ErrorMessage = "Year must be an integer greater than 1800.")]
        public int? YearProduced { get; set; }

        [Required(ErrorMessage = "Rating is required.")]
        [Range(0.0, 10.0, ErrorMessage = "Rating must be a number between 0 and 10.")]
        public double? Rating { get; set; }

        [Required(ErrorMessage = "Please enter the recording studio")]
        [Display(Name = "Studio")]
        public int StudioId { get; set; }

        public Studio Studio { get; set; }
    }
}
