using EwidencjaWSK.Data;
using EwidencjaWSK.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwidencjaWSK.Services
{
    public class SupplierService
    {
        private readonly ApplicationDbContext _context;
        private readonly string NameOfCreatedField = "Created";

        public SupplierService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Supplier FindById(int id)
        {
            return BaseQuery().FirstOrDefault(part => part.SupplierId == id);
        }

        private IEnumerable<Supplier> BaseQuery()
        {
            return _context.Suppliers.OrderBy(b => EF.Property<DateTime>(b, NameOfCreatedField));
        }

        public IEnumerable<Supplier> GetAll()
        {
            return BaseQuery();
        }
    }
}
