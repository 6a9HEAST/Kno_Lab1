using System;
using System.Collections.Generic;

namespace КПО_1.Model;

public partial class Owner
{
    public short OwnerId { get; set; }

    public string FullName { get; set; } = null!;

    public short DrivingExperienceYears { get; set; }

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
