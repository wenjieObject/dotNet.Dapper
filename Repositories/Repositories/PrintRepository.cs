using Entity;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public class PrintRepository : BaseRepository<string, PRINT_DATA_N>, IPrintRepository
    {
    }
}
