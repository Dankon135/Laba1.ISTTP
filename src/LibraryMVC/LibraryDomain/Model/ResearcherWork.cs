using System;
using System.Collections.Generic;

namespace LibraryDomain.Model;

public partial class ResearcherWork
{
    public int Id { get; set; }

    public int ScientificWorkId { get; set; }

    public string Contribution { get; set; } = null!;

    public DateOnly CreatedAt { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<ZtTable> ZtTables { get; set; } = new List<ZtTable>();
}
