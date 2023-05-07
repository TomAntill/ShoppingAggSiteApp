using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAggSiteApp.Models
{
    public class StoreVm
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int LocationId { get; set; }
        public string StoreImageUrl { get; set; }
        public string StoreName { get; set; }
    }
}
