using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiNet6.Models;

public partial class Category
{
    public int IdCategory { get; set; }

    public string? Description { get; set; }

    [JsonIgnore]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
