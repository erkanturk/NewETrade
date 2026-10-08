using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ETICARET.Entities
{
    public class Category:BaseEntity
    {
        [MaxLength(100)]
        public required string Name { get; set; }

        public List<Product> Products { get; set; } = [];

    }
}
