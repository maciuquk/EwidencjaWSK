using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EwidencjaWSK.Data;
using EwidencjaWSK.Models;
using EwidencjaWSK.Services;
using EwidencjaWSK.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EwidencjaWSK.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
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
                newRecord.Value = "1000";
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

        public IActionResult Audit()
        {
            var auditDates = new AuditDateViewModel();
            auditDates.From = DateTime.Now;
            auditDates.To = DateTime.Now;
            return View(auditDates);
        }

        public IActionResult Changes(AuditDateViewModel auditDates)
        {
            if (auditDates.From == null)
            {
                auditDates.From = DateTime.Now;
            }

            if (auditDates.To == null)
            {
                auditDates.To = DateTime.Now;
            }

            auditDates.To = auditDates.To.AddHours(23);
            auditDates.To = auditDates.To.AddMinutes(59);
            auditDates.To = auditDates.To.AddSeconds(59);


            var audit = _context.Audits.Where(n=>n.DateTime >= auditDates.From && n.DateTime <= auditDates.To && n.TableName !="Role" && n.TableName != "Użytkownicy").ToList();
            var changes = new List<AuditViewModel>();

            int id = 0;
            foreach (var item in audit)
            {
                if (item.OldValues == null && item.NewValues == null)
                {
                }
                else
                {
                    string whatChanged = ChangesComparisonService.Changes(item.OldValues, item.NewValues);

                    id++;
                    changes.Add(new AuditViewModel { Id = id, Changes = whatChanged, Date = item.DateTime, BeforeChanges = item.OldValues, AfterChanges = item.NewValues, Table = item.TableName, ChangedBy = item.ChangedBy });
                }
                
            }

            return View(changes);
        }

        public async Task<IActionResult> Users()
        {
            List<UserRoleViewModel> userRole = new List<UserRoleViewModel>();
            
            foreach (var us in _userManager.Users)
            {
                IdentityUser user = await _userManager.FindByEmailAsync(us.Email);
                var role = await _userManager.GetRolesAsync(user);
                userRole.Add(new UserRoleViewModel { ApplicationUser = user, Role = role });
                
            }
            return View(userRole);
        }

        public async Task<IActionResult> MakeAdmin(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            await _userManager.RemoveFromRoleAsync(user, "User");
            await _userManager.AddToRoleAsync(user, "Admin");

            return RedirectToAction("Users");
        }

        public async Task<IActionResult> MakeUser(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            await _userManager.RemoveFromRoleAsync(user, "Admin");
            await _userManager.AddToRoleAsync(user, "User");

            return RedirectToAction("Users");
        }

        public async Task<IActionResult> Delete (string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            await _userManager.DeleteAsync(user);

            return RedirectToAction("Users");
        }
    }


}
