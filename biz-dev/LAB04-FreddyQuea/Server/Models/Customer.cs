using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Server.Models;

[Table("customer")]
[Index("Email", Name = "customer_email_key", IsUnique = true)]
public partial class Customer
{
    [Key]
    [Column("customer_id")]
    public int CustomerId { get; set; }

    [Column("full_name")]
    [StringLength(100)]
    public string FullName { get; set; } = null!;

    [Column("email")]
    [StringLength(100)]
    public string Email { get; set; } = null!;

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime? CreatedAt { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<Address> Address { get; set; } = new List<Address>();

    [InverseProperty("Customer")]
    public virtual ICollection<Order> Order { get; set; } = new List<Order>();
}
