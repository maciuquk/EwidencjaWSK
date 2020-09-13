using EwidencjaWSK.Data;
using EwidencjaWSK.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EwidencjaWSK.Services
{
    public class PartService
    {
        private readonly ApplicationDbContext _context;
        private readonly string NameOfCreatedField = "Created";

        public PartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Part FindById(int id)
        {
            return BaseQuery().FirstOrDefault(part => part.PartId == id);
        }

        private IEnumerable<Part> BaseQuery()
        {
            return _context.Parts.OrderBy(b => EF.Property<DateTime>(b, NameOfCreatedField));
        }

        public IEnumerable<Part> GetAll()
        {
            return BaseQuery();
        }
    }
}
