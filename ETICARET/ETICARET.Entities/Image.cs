using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ETICARET.Entities
{
    public class Image:BaseEntity
    {
        [MaxLength(300)]
        public required string ImageUrl { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }

    }
}
