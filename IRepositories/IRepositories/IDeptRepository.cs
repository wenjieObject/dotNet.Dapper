using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRepositories
{
    public interface IDeptRepository : IBaseRepository<string,SSD_DEPARTMENT>
    {
        string GetSingle(string id);
    }
}
