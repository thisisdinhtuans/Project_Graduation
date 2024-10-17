using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Page
{
    public class RestaurantParams : PaginationParams
    {
        public string OrderBy { get; set; }
        public string SearchTerm { get; set; }
    }
}
