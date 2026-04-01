using System;
using System.Collections.Generic;

namespace FinancialsHubWebAPI.Models;

public partial class CourseRoundInstructor
{
    public long Id { get; set; }

    public long CourseRoundId { get; set; }

    public long InstructorAccountId { get; set; }

    public DateOnly AssignedDate { get; set; }

    public virtual CourseRound CourseRound { get; set; } = null!;

    public virtual Account InstructorAccount { get; set; } = null!;
}
