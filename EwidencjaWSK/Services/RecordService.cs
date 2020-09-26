using EwidencjaWSK.Data;
using EwidencjaWSK.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwidencjaWSK.Services
{
    public class RecordService : IRecordService
    {
        private readonly ApplicationDbContext _context;
        private readonly string NameOfCreatedField = "Created";

        public RecordService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Record FindById(int id)
        {
            return BaseQuery().FirstOrDefault(record => record.RecordId == id);
        }

        private IEnumerable<Record> BaseQuery()
        {
            return _context.Records.OrderBy(b => EF.Property<DateTime>(b, NameOfCreatedField));
        }

        public IEnumerable<Record> GetAll()
        {
            return BaseQuery();
        }
    }
}
