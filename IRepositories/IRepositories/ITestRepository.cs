using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRepositories
{
    public interface ITestRepository : IBaseRepository<string,SSD_DEPARTMENT>
    {
        public void InvokeApi();
    }
}
