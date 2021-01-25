using Entity;
using Integration;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel;

namespace Repositories
{
    public class TestRepository : BaseRepository<string,SSD_DEPARTMENT>, ITestRepository
    {
        public void InvokeApi()
        {
            //测试数据
            List<WorkcenterApi> workcenters = new List<WorkcenterApi>();

            workcenters.Add(new WorkcenterApi("30", "工作230"));
            workcenters.Add(new WorkcenterApi("40", "工作40"));

            ESBApiRepository eSBApiRepository = new ESBApiRepository();

            var result= eSBApiRepository.ExcuteApi<object, WorkcenterApi>(workcenters, "MES", "wms/workcenter");

            Console.WriteLine(result);
        }
    }
}
