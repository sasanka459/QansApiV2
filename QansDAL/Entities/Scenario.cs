using System;
using System.Collections.Generic;

namespace QansDAL.Entities;

public partial class Scenario
{
    public int ScenarioId { get; set; }

    public string ScenarioText { get; set; } = null!;

    public string? ReferenceImageUrl { get; set; }

    public string? ReferenceLink { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
