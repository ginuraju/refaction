using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Data.Models
{
    public class ProductOption : BaseEntity
    {
        public Guid ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public bool IsNew { get; set; }
    }
}
