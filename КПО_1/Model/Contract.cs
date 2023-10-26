using System;
using System.Collections.Generic;

namespace КПО_1.Model;

public partial class Contract
{
    public short ContractId { get; set; }

    public DateTime ConslusionDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal Price { get; set; }

    public decimal ValueOfInsuranceCases { get; set; }

    public short VehicleId { get; set; }

    public short InsuranceAgentId { get; set; }

    public virtual InsuranceAgent InsuranceAgent { get; set; } = null!;

    public virtual ICollection<InsuranceCase> InsuranceCases { get; set; } = new List<InsuranceCase>();

    public virtual Vehicle Vehicle { get; set; } = null!;
}
