using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectECommerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        public string Author { get; set; }
        [Required]
        [Range(1,1000)]
        public  double ListPrice { get; set; }
        [Required]
        [Range(1,1000)]
        public double Price { get; set; }
        [Required]
        [Range(1,1000)]
        public double Price50 { get; set; }
        [Required]
        [Range(1,1000)]
        public double Price100 { get; set;}
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }
        [Display(Name ="Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [Display(Name ="CoverType")]
        public int CoverTypeId { get; set; }
        public CoverType CoverType { get; set; }

    }
}
