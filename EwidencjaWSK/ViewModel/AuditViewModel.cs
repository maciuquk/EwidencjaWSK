using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EwidencjaWSK.ViewModel
{
    public class AuditViewModel
    {
        [Display(Name ="LP")]
        public int Id { get; set; }

        [Display(Name = "Data")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name ="Przed")]
        public string BeforeChanges { get; set; }
        
        [Display(Name ="Po")]
        public string AfterChanges { get; set; }

        [Display(Name = "Zmiana")]
        public string Changes { get; set; }

        [Display(Name ="Tabela")]
        public string Table { get; set; }

        [Display(Name ="Dokonał zmiany")]
        public string ChangedBy { get; set; }

    }


}
