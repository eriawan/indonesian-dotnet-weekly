using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleAspNetAspire90.ApiService.DataContext
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public string ProductColor { get; set; } = "";
        public decimal ProductPrice { get; set; }
        public string? ProductDescription { get; set; }
    }
}
