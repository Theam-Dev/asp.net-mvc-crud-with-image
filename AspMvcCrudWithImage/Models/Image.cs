using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspMvcCrudWithImage.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String Title { get; set; }
        [Required]
        public String ImagePath { get; set; }

    }
}