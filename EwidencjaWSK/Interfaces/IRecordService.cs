﻿using EwidencjaWSK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwidencjaWSK.Services
{
    interface IRecordService
    {
        Record FindById(int id);
        IEnumerable<Record> GetAll();
    }
}
