using System;
using System.Collections.Generic;

namespace КПО_1;

public partial class Vehicle
{
    public short VehicleId { get; set; }

    public string StateImplementationNumber { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public short Mileage { get; set; }

    public short OwnerDrivingExperience { get; set; }

    public short OwnerId { get; set; }

    public int? ModelId { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    public virtual Model? Model { get; set; }

    public virtual Owner Owner { get; set; } = null!;
}
