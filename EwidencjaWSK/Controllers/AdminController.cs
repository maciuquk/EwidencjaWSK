using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using EwidencjaWSK.Data;
using EwidencjaWSK.Models;
using EwidencjaWSK.Services;
using EwidencjaWSK.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
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
                    changes.Add(new AuditViewModel { Id = id, Changes = whatChanged, Date = item.DateTime, Table = item.TableName, ChangedBy = item.ChangedBy });
                }
                
            }

            if (auditDates.ExportToFile == false)
                return View(changes);
            else
            {
                #region excel

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Raport zmian");
                    var currentRow = 1;
                    worksheet.Cell(currentRow, 1).Value = "Lp";
                    worksheet.Cell(currentRow, 2).Value = "Data";
                    worksheet.Cell(currentRow, 3).Value = "Tabela";
                    worksheet.Cell(currentRow, 4).Value = "Użytkownik";
                    worksheet.Cell(currentRow, 5).Value = "Co zmienione";

                    foreach (var change in changes)
                    {
                        currentRow++;

                        worksheet.Cell(currentRow, 1).Value = currentRow - 1;
                        worksheet.Cell(currentRow, 2).Value = change.Date;
                        worksheet.Cell(currentRow, 3).Value = change.Table;
                        worksheet.Cell(currentRow, 4).Value = change.ChangedBy;
                        worksheet.Cell(currentRow, 5).Value = change.Changes;
                    }

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();

                        return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Raport.xlsx");
                    }
                }

                #endregion

                return RedirectToAction("Audit");
            }
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

        public async Task<IActionResult> UserConfirm()
        {
            List<IdentityUser> users = _userManager.Users.ToList();
            List<UserRoleViewModel> allUsers = new List<UserRoleViewModel>();
            return View(users);

        }

       [HttpPost]
        public async Task<IActionResult> UserAccept(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {

                    foreach (var user in _context.Users)
                {
                    if (user.Email == email)
                    {
                        user.EmailConfirmed = true;
                        
                    }
                }
                    }

            _context.SaveChanges();

            return RedirectToAction("UserConfirm");
        }

        [HttpPost]
        public async Task<IActionResult> UserBlocked(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                foreach (var user in _context.Users)
                {
                    if (user.Email == email)
                    {
                        user.EmailConfirmed = false;
                        
                    }
                }

            }

            _context.SaveChanges();

            return RedirectToAction("UserConfirm");
        }
    }


}
