using EwidencjaWSK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwidencjaWSK.ViewModel
{
    public class RecordViewModel
    {
        public Record Record{ get; set; }
        public List<Supplier> Suppliers { get; set; }
        public List<AdditionalDoc> AdditionalDocs { get; set; }
        public List<Part> Parts { get; set; }
    }
}
