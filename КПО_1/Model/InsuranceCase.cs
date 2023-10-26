using System;
using System.Collections.Generic;

namespace КПО_1.Model;

public partial class InsuranceCase
{
    public short InsuranceCaseId { get; set; }

    public string CaseType { get; set; } = null!;

    public DateTime Date { get; set; }

    public short ContractId { get; set; }

    public short Value { get; set; }

    public virtual Contract Contract { get; set; } = null!;
}
