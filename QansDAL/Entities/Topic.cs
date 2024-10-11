using System;
using System.Collections.Generic;

namespace QansDAL.Entities;

public partial class Topic
{
    public int TopicId { get; set; }

    public string TopicName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Chapter> Chapters { get; set; } = new List<Chapter>();
}
