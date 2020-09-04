using EwidencjaWSK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwidencjaWSK.ViewModel
{
    public class AdditionalDocsViewModel
    {
        public List<AdditionalDoc> AdditionalDocs { get; set; }
        public PaginationViewModel PaginationViewModel { get; set; }
    }
}
