using eShopSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Data.Entities
{
    
    public class News
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string Url { get; set; }

        public string Img { get; set; }

        public string Video { get; set; }

        public int Viewss { get; set; }

        public Status News_Hot { get; set; }

        public int Display_On { get; set; }

        public DateTime Date { get; set; }

        public Status Status { get; set; }

        public string Keyword { get; set; }
    }
}
