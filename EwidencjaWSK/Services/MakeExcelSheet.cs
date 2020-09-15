using EwidencjaWSK.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace EwidencjaWSK.Services
{
    public class ExportToExcel
    {

        public void MakeExcelSheet(List<Record> datatoExport, List<Supplier> suppliers)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Raport");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Lp";
                worksheet.Cell(currentRow, 2).Value = "Numer kontraktu";
                worksheet.Cell(currentRow, 3).Value = "Data";
                worksheet.Cell(currentRow, 4).Value = "Wartość";
                worksheet.Cell(currentRow, 5).Value = "Waluta";
                worksheet.Cell(currentRow, 6).Value = "Dostawca";

                foreach (var record in datatoExport)
                {
                    currentRow++;

                    worksheet.Cell(currentRow, 1).Value = currentRow - 1;
                    worksheet.Cell(currentRow, 2).Value = record.Number;
                    worksheet.Cell(currentRow, 3).Value = record.Date;
                    worksheet.Cell(currentRow, 4).Value = record.Value;
                    worksheet.Cell(currentRow, 5).Value = record.Currency;
                    worksheet.Cell(currentRow, 6).Value = from supplierName in suppliers
                                                          where supplierName.SupplierId == record.SuplierId
                                                          select supplierName.Name;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    //return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    //    "Raport.xlsx");
                }
            }
        }

    }
}
