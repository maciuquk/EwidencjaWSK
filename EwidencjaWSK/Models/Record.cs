using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EwidencjaWSK.Models
{
    public class Record
    {
        [Key]
        public int RecordId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="Numer kontraktu")]
        public string Number { get; set; }

        [Display(Name = "Data")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Firma")]
        public virtual Supplier RecordSupplier { get; set; }

        [Display(Name = "Wartość")]
        public decimal Value{ get; set; }

        [Display(Name = "Waluta")]
        public string Currency { get; set; } // zrobić enuma do wyboru $ € PLN lub inne

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Rodzaj obrotu")]
        public string KindOfTransaction { get; set; } // uzbrojenie / dual use

        [Display(Name = "Czy sprawdzona lista odmów?")]
        public bool IsCheckDenyList { get; set; }

        [Display(Name = "Czy sprawdzona lista syngałów ostrzegawczych?")]
        public bool IsCheckWarningSignalList { get; set; }

        [Display(Name = "Czy przedmiot obrotu w bazie części?")]
        public bool IsInPartsBase { get; set; } // numer w bazie danych dostawcy

        [Display(Name = "Czy wymagane jest zewzwolenie ministerstwa?")]
        public bool CzyWymaganeZezwolenieMin { get; set; } // tak / nie
    }

    public class WarehouseDoc
    {
        public int Id { get; set; }

        [Display(Name = "Numer dokumentu magazynowego")]
        public string Number { get; set; }

        [Display(Name = "Data")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }

    public class AccountDoc
    {
        //wypełnia Ksiegowosc
        public int Id { get; set; }

        [Display(Name = "Numer dokumentu księgowego")]
        public string Number { get; set; }

        [Display(Name = "Data")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime Date{ get; set; }

        [Display(Name ="Nazwa dokumentu")]
        public string KindOfDoc { get; set; }

    }

    public class Supplier
    {
        public int Id { get; set; }

        [Display(Name = "Nazwa Kontrahenta")]
        public string Name{ get; set; }

        [Display(Name = "Kod pocztowy")]
        public string PostalCode{ get; set; }

        [Display(Name = "Ulica")]
        public string Street { get; set; }

        [Display(Name = "Nr")]
        public string Number { get; set; }

        [Display(Name = "Państwo")]
        public string Country { get; set; }

    }

    public class Part
    {
        [ForeignKey("Record")]
        public int PartId { get; set; }

        [Display(Name = "Nazwa części")]
        public string Name { get; set; }

        [Display(Name = "Czy jest uzbrojeniem?")]
        public bool IsInArmedList { get; set; }
        
        public virtual Record Record { get; set; }

    }



    // dodać audyt
}
