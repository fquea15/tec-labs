using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Server.Models;

[Table("order_")]
public partial class Order
{
    [Key]
    [Column("order_id")]
    public int OrderId { get; set; }

    [Column("customer_id")]
    public int CustomerId { get; set; }

    [Column("shipping_address_id")]
    public int ShippingAddressId { get; set; }

    [Column("billing_address_id")]
    public int BillingAddressId { get; set; }

    [Column("total")]
    [Precision(10, 2)]
    public decimal Total { get; set; }

    [Column("status")]
    [StringLength(50)]
    public string? Status { get; set; }

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime? CreatedAt { get; set; }

    [ForeignKey("BillingAddressId")]
    [InverseProperty("OrderBillingAddress")]
    public virtual Address BillingAddress { get; set; } = null!;

    [ForeignKey("CustomerId")]
    [InverseProperty("Order")]
    public virtual Customer Customer { get; set; } = null!;

    [InverseProperty("Order")]
    public virtual ICollection<OrderDetail> OrderDetail { get; set; } = new List<OrderDetail>();

    [InverseProperty("Order")]
    public virtual ICollection<Payment> Payment { get; set; } = new List<Payment>();

    [ForeignKey("ShippingAddressId")]
    [InverseProperty("OrderShippingAddress")]
    public virtual Address ShippingAddress { get; set; } = null!;
}
