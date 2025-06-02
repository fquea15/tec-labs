﻿using System;
using System.Collections.Generic;

namespace LAB08_FreddyQuea.Models;

public partial class Client
{
    public int Clientid { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
