using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcECommerce.Models.ViewMoldels
{
    public class HomeViewModel
    {
        public List<Categories> Categories { get; set; }
        public List<Brands> Brands { get; set; }
        public List<Products> Products { get; set; }
        public List<WishList> WishLists { get; set; }
        public List<Users> Users { get; set; }
        public Products ProductId { get; set; }




    }
}