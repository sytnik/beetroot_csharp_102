using System;
using System.Collections.Generic;

namespace Lesson36;

public partial class OrderDetail
{
    public int? Id { get; set; }

    public string ShippingAddress { get; set; }

    public int? OrderId { get; set; }
}
