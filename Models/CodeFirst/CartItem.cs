using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cwagnerShoppingApp.Models.CodeFirst
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        [Display(Name = "Quantity")]
        public int Count { get; set; }
        [Display(Name = "Creation Date")]
        public DateTime? CreationDate { get; set; }
        public string CustomerId { get; set; }

        public virtual Item Item { get; set; }
        public virtual ApplicationUser Customer { get; set; }
    }
}