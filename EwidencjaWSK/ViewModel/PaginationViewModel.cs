using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwidencjaWSK.ViewModel
{
    public class PaginationViewModel
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages => 
            (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
    }
}
