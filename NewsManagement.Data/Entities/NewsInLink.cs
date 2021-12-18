using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Data.Entities
{
    public class NewsInLink
    {
        public int NewsId { get; set; }

        public News News { get; set; }

        public int AccountId { get; set; }

        public Account Account { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        public int TopicId { get; set; }

        public Topic Topic { get; set; }

        public int EventId { get; set; }

        public Eventss Event { get; set; }
    }
}
