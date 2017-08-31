using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cwagnerShoppingApp.Models.CodeFirst
{
    public class Item
    {
        public int Id { get; set; }
        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }
        [Display(Name = "Updated Date")]
        public DateTime? UpdatedDate { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        [Display(Name = "Image")]
        public string MediaURL { get; set; }
        [AllowHtml]
        public string Description { get; set; }
    }
}