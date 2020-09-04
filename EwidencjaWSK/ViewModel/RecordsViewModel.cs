using EwidencjaWSK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwidencjaWSK.ViewModel
{
    public class RecordsViewModel
    {
        public List<Record> Records{ get; set; }
        public List<Supplier> Suppliers{ get; set; }
        public List<Part> Parts{ get; set; }
        public List<AdditionalDoc> AdditionalDocs { get; set; }
        public int Year { get; set; }
        public PaginationViewModel PaginationViewModel { get; set; }


    }
}
