using System;
using System.Collections.Generic;

namespace КПО_1;

public partial class Model
{
    public int ModelId { get; set; }
    
    public string? Name { get; set; }

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
