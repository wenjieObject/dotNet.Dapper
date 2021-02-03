using Entity;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public class PrintOutRepository : BaseRepository<string, PRINT_DATA_O>, IPrintOutRepository
    {
    }
}
