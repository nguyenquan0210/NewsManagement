using NewsManagement.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.ViewModels.Catalog
{
    public class CatalogCreateRequest
    {

        [Required]
        public string Name { get; set; }

        public int SortOrder { get; set; }

        public bool Hot { get; set; }

    }
}
