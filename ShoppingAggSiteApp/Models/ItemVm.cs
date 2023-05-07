using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAggSiteApp.Models
{
    public class ItemVm
    {
        public int Id { get; set; }
        [Display(Name = "Store Id")]
        public int StoreId { get; set; }
        [Display(Name = "Quality Rating Id")]
        public int QualityRatingId { get; set; }
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }
        [Display(Name = "Image Url")]
        public string ItemImageUrl { get; set; }
        [Display(Name = "Current Price")]
        public decimal CurrentPrice { get; set; }
        public decimal Weight { get; set; }
    }

}
