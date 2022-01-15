using NewsManagement.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.ViewModels.Catalog.Newss
{
    public class NewsVm
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string Img { get; set; }

        public string Video { get; set; }

        public bool News_Hot { get; set; }

        public bool Status { get; set; }

        public string Keyword { get; set; }

        public string CateName { get; set; }

        public int EventId { get; set; }

        public int CityId { get; set; }
       
        public int TopicId { get; set; }

        public int CategoryId { get; set; }

        public int SaveId { get; set; }

        public DateTime Date { get; set; }

        public int View { get; set; }

    }
}
