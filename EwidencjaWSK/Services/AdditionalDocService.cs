using EwidencjaWSK.Data;
using EwidencjaWSK.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwidencjaWSK.Services
{
    public class AdditionalDocService : IAdditionalDocService
    {
        private readonly ApplicationDbContext _context;
        private readonly string NameOfCreatedField = "Created";

        public AdditionalDocService(ApplicationDbContext context)
        {
            _context = context;
        }

        public AdditionalDoc FindById(int id)
        {
            return BaseQuery().FirstOrDefault(additionalDoc => additionalDoc.AdditionalDocId == id);
        }

        private IEnumerable<AdditionalDoc> BaseQuery()
        {
            return _context.AdditionalDocs.OrderBy(b => EF.Property<DateTime>(b, NameOfCreatedField));
        }

        public IEnumerable<AdditionalDoc> GetAll()
        {
            return BaseQuery();
        }
    }
}
