﻿using System.ComponentModel.DataAnnotations.Schema;

namespace _00_LoginPage.Models
{
    public class ProductViewModel : BaseEntity
    {
        public string Description { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))] /* Bir kategorinin bir den fazla productu olabilir */ 
        public virtual CategoryViewModel Category { get; set; }
    }
}
