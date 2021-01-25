using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public class WorkcenterApi
    {
        public WorkcenterApi(string no,string name)
        {
            work_center_no = no;
            work_center_name = name;
        }
        public string work_center_no { get; set; }
        public string work_center_name { get; set; }
    }
}
