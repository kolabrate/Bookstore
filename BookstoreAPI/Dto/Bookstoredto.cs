using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BookstoreAPI.Dto
{
    public class Bookdto
    {

        public int? Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public decimal Cost { get; set; }

        [Required]
        public string Genre { get; set; }

    }
}