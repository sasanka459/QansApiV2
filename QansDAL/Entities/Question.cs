using System;
using System.Collections.Generic;

namespace QansDAL.Entities;

public partial class Question
{
    public int QuestionId { get; set; }

    public int? ChapterId { get; set; }

    public int? ScenarioId { get; set; }

    public string QuestionText { get; set; } = null!;

    public string QuestionType { get; set; } = null!;

    public string? ReferenceImageUrl { get; set; }

    public string? ReferenceLink { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual Chapter? Chapter { get; set; }

    public virtual ICollection<Column> Columns { get; set; } = new List<Column>();

    public virtual ICollection<Option> Options { get; set; } = new List<Option>();

    public virtual Scenario? Scenario { get; set; }
}
