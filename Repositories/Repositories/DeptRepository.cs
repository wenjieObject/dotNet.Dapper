using Dapper;
using Entity;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Repositories
{
    public class DeptRepository : BaseRepository<string,SSD_DEPARTMENT>, IDeptRepository
    {

        public string GetSingle(string id)
        {
            var list = new List<SSD_DEPARTMENT>();
            using (IDbConnection connection = DapperFactory.CrateOracleConnection())
            {
                 
                 list = connection.Query<SSD_DEPARTMENT>("select * from SSD_DEPARTMENT").ToList();
            }

            foreach(var item in list)
            {
                Console.WriteLine(item.GUID);
            }
            return "学生张三";//造个假数据返回
        }
    }
}
