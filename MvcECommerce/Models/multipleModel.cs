using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcECommerce.Models;

namespace MvcECommerce.Models
{
    public class multipleModel
    {
        ECommerce_2019_DbEntities2 db = new ECommerce_2019_DbEntities2();
        public List<Categories> GetCategories()
        {
            List<Categories> categories = db.Categories.ToList();
            return categories;
        }
        public List<Brands> GetBrands()
        {
            List<Brands> brands = db.Brands.ToList();
            return brands;
        }
        public List<Products> FilterProducts(int? Id)
        {
            if (Id == null)
            {
                List<Products> products = db.Products.ToList();
                return products;
            }
            else
            {
                List<Products> products = db.Products.Where(x => x.CategoryId == Id
            || x.BrandId == Id).ToList();
                return products;
            }
            
        }
        public Products GetId(int Id)
        {
                return db.Products.Find(Id);
        }
        public List<Images> FilterImages(int id)
        {
                List<Images> images = db.Images.Where(x => x.ImageId == id).ToList();
                return images;
        }
        public Users GetUserId(int? UserId)
        {
            if (UserId != null)
            {
                return db.Users.Find(UserId);
            }
            else
            {
                return db.Users.Find(3);
            }
            
        }

    }
}