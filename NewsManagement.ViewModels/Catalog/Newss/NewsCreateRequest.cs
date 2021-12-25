using Microsoft.AspNetCore.Http;
using NewsManagement.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.ViewModels.Catalog.Newss
{
    public class NewsCreateRequest
    {
        [Required]
        public string Title { get; set; }


        [Required]
        public string Description { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Content { get; set; }

        public string Url { get; set; }

        public string Video { get; set; }

        [Required]
        public Status News_Hot { get; set; }

        [Required]
        public string Keyword { get; set; }


        [Required]
        public int EventId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public int CityId { get; set; }

        [Required]
        public int TopicId { get; set; }

        [Required]
        public IFormFile ThumbnailImage { get; set; }

    }
}
