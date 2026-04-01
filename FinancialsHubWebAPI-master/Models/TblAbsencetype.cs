using System;
using System.Collections.Generic;

namespace FinancialsHubWebAPI.Models;

public partial class TblAbsencetype
{
    public int Id { get; set; }

    public int OrderNumber { get; set; }

    public string AbsenceType { get; set; } = null!;
}
