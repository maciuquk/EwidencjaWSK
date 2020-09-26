using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EwidencjaWSK.ViewModel
{
    public class AuditDateViewModel
    {
        [Display(Name ="Od:")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime From { get; set; }

        [Display(Name ="Do:")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime To { get; set; }

        [Display(Name ="Użytkownik")]
        public List<string> User { get; set; }

        [Display(Name ="Wszyscy użytkownicy")]
        public bool AllUsers { get; set; }

        [Display(Name ="Eksport do pliku")]
        public bool ExportToFile { get; set; }
    }
}
