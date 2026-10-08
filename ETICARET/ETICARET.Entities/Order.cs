using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ETICARET.Entities
{
    public class Order : BaseEntity
    {
        [MaxLength(30)]
        public required string OrderNumber { get; set; }
        [MaxLength(450)]
        public required string UserId { get; set; }
        [MaxLength(50)]
        public required string FirstName { get; set; }
        [MaxLength(50)]
        public required string LastName { get; set; }
        [MaxLength(150)]
        public required string Email { get; set; }
        [MaxLength(20)]
        public required string Phone { get; set; }
        [MaxLength(50)]
        public required string City { get; set; }
        [MaxLength(500)]
        public required string Address { get; set; }
        [MaxLength(500)]
        public string? Note { get; set; }
        public PaymentType PaymentType { get; set; }
        public OrderState State { get; set; }

        [MaxLength(100)]
        public string? PaymentId { get; set; }
        [MaxLength(100)]
        public string? ConversationId { get; set; }
        public List<OrderItem> Items { get; set; } = [];
        public decimal TotalPrice => Items.Sum(i => i.Price*i.Quantity);

        public string FullName => $"{FirstName} {LastName}";


    }
}
