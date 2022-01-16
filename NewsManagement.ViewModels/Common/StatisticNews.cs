using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.ViewModels.Common
{
    public class StatisticNews
    {
        public string date { get; set; }
        public string month { get; set; }
        public int count { get; set; }
        /*public string catename { get; set; }*/
        public int view { get; set; }
        public int rating { get; set; }
    }
}
