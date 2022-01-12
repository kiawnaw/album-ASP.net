using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AlbumsApp.Models
{
    public class Studio
    {
        public int StudioId { get; set; }
        [Required(ErrorMessage = "Studio name is required.")]
        [StringLength(64)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Website name is required.")]
        [Url]
        public string Url { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "City name is required.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Zipcode is required.")]
        [RegularExpression(@"(^\d{5}$)|(^\d{9}$)|(^\d{5}-\d{4}$)", ErrorMessage = "zipcode is not valid")]
        public string ZipCode { get; set; }
    }
}
