using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace AspMvcCrudWithImage.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }
        public DbSet<Image> Images{ get; set; }
    }
}