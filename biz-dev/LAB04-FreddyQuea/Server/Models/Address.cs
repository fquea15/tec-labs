using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Server.Models;

[Table("address")]
public partial class Address
{
    [Key]
    [Column("address_id")]
    public int AddressId { get; set; }

    [Column("customer_id")]
    public int CustomerId { get; set; }

    [Column("address_line")]
    [StringLength(255)]
    public string AddressLine { get; set; } = null!;

    [Column("city")]
    [StringLength(100)]
    public string? City { get; set; }

    [Column("state")]
    [StringLength(100)]
    public string? State { get; set; }

    [Column("country")]
    [StringLength(100)]
    public string? Country { get; set; }

    [Column("zip_code")]
    [StringLength(20)]
    public string? ZipCode { get; set; }

    [Column("type")]
    [StringLength(50)]
    public string? Type { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Address")]
    public virtual Customer Customer { get; set; } = null!;

    [InverseProperty("BillingAddress")]
    public virtual ICollection<Order> OrderBillingAddress { get; set; } = new List<Order>();

    [InverseProperty("ShippingAddress")]
    public virtual ICollection<Order> OrderShippingAddress { get; set; } = new List<Order>();
}
