using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EwidencjaWSK.Data;
using EwidencjaWSK.Models;
using Microsoft.AspNetCore.Mvc;

namespace EwidencjaWSK.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ZasiejBaze()
        {
            for (int i = 1; i < 100; i++)
            {
                var newRecord = new Record();
                var newPart = new Part();
                var newAdditionalDoc = new AdditionalDoc();
                var newSupplier = new Supplier();

                var rnd = new Random();
                newSupplier.Name = "Nowy" + rnd.Next(1000).ToString();
                newSupplier.PostalCode = "11-111";
                newSupplier.Street = "Ulica";
                newSupplier.Number = "1";
                newSupplier.Country = "Niemcy";
                newSupplier.Name = "Dostawca" + rnd.Next(1000).ToString();
                _context.Suppliers.Add(newSupplier);
                _context.SaveChanges();

                newRecord.Number = "Kontrakt nr. " + rnd.Next(1000).ToString();
                newRecord.Date = new DateTime(2020,2,4);
                newRecord.Value = 1000;
                newRecord.SuplierId = 4;
                _context.Records.Add(newRecord);
                _context.SaveChanges();

                newAdditionalDoc.Number = "Dokument nr" + rnd.Next(1000).ToString();
                newAdditionalDoc.KindOfDoc = "WZ";
                _context.AdditionalDocs.Add(newAdditionalDoc);
                _context.SaveChanges();

                newPart.Name = "Część" + rnd.Next(1000).ToString();
                _context.Parts.Add(newPart);
                _context.SaveChanges();



            }


            return RedirectToAction("Index", "Records");
        }

        public IActionResult WyczyscBaze()
        {
            return null;
        }
    }


}
