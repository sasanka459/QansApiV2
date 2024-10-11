using System;
using System.Collections.Generic;

namespace QansDAL.Entities;

public partial class Chapter
{
    public int ChapterId { get; set; }

    public int? TopicId { get; set; }

    public string ChapterName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual Topic? Topic { get; set; }
}
