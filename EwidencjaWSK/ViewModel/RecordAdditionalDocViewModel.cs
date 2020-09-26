using EwidencjaWSK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwidencjaWSK.ViewModel
{
    public class RecordAdditionalDocViewModel
    {
        public Record Record { get; set; }
        public List<AdditionalDoc> AdditionalDocs { get; set; }
    }
}
