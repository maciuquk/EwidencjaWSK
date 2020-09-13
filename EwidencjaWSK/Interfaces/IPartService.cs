using EwidencjaWSK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwidencjaWSK.Services
{
    interface IPartService
    {
        Part FindById(int id);
        IEnumerable<Part> GetAll();
    }
}
