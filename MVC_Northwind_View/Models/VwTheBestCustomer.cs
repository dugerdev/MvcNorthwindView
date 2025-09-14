using System;
using System.Collections.Generic;

namespace MVC_Northwind_View.Models;

public partial class VwTheBestCustomer
{
    public string CustomerId { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public short TotalQuantity { get; set; }
}
