using System;
using System.Collections.Generic;

namespace QansDAL.Entities;

public partial class Answer
{
    public int AnswerId { get; set; }

    public int? QuestionId { get; set; }

    public int? ColumnId { get; set; }

    public string MatchText { get; set; } = null!;

    public virtual Column? Column { get; set; }

    public virtual Question? Question { get; set; }
}
