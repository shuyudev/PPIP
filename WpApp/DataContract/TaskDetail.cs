using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract
{
    public class TaskDetail
    {
        public TaskType Type { get; set; }

        public TaskInfo TaskInfo { get; set; }
        public string Id { get; set; }
    }
}
