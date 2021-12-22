using Microsoft.AspNetCore.Http;
using NewsManagement.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.ViewModels.Catalog.Newss
{
    public class NewsCreateRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string Url { get; set; }

        public string Video { get; set; }

        public Status News_Hot { get; set; }

        public string Keyword { get; set; }

        public int EventId { get; set; }


        public int AccountId { get; set; }

        public int CityId { get; set; }

        public int TopicId { get; set; }

        public IFormFile ThumbnailImage { get; set; }

    }
}
