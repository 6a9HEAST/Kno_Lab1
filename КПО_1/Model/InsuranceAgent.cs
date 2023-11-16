using System;
using System.Collections.Generic;

namespace КПО_1;

public partial class InsuranceAgent
{
    public short InsuranceAgentId { get; set; }

    public string FullName { get; set; } = null!;

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}
