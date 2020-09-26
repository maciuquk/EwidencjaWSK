using EwidencjaWSK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwidencjaWSK.Services
{
    interface IAdditionalDocService
    {
        AdditionalDoc FindById(int id);
        IEnumerable<AdditionalDoc> GetAll();
    }
}
