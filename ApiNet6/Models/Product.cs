using System;
using System.Collections.Generic;

namespace ApiNet6.Models;

public partial class Product
{
    public int IdProduct { get; set; }

    public string? Barcode { get; set; }

    public string? Description { get; set; }

    public string? Brand { get; set; }

    public int? IdCategory { get; set; }

    public decimal? Price { get; set; }

    public virtual Category? oCategory { get; set; }
}
