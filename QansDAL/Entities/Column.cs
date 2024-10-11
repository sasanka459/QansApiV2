using System;
using System.Collections.Generic;

namespace QansDAL.Entities;

public partial class Column
{
    public int ColumnId { get; set; }

    public int? QuestionId { get; set; }

    public string ColumnText { get; set; } = null!;

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual Question? Question { get; set; }
}
