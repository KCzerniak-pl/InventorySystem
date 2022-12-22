using System.ComponentModel.DataAnnotations;

namespace InventorySystemWebApi.Models
{
    public class PageQuery
    {
        public string? SearchPhrase { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public int PageNumber { get; set; }

        [Range(1, 50)]
        public int PageSize { get; set; }
    }
}
