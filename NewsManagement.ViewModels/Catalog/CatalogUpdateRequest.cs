using System.ComponentModel.DataAnnotations;

namespace NewsManagement.ViewModels.Catalog
{
    public class CatalogUpdateRequest
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int SortOrder { get; set; }

        public bool Status { get; set; }

        public bool Hot { get; set; }
    }
}
