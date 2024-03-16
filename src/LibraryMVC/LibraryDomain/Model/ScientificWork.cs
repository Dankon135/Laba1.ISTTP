using System;
using System.Collections.Generic;

namespace LibraryDomain.Model;

public partial class ScientificWork
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int ResearcherId { get; set; }

    public string Client { get; set; } = null!;

    public string ClientAddress { get; set; } = null!;

    public string Subordination { get; set; } = null!;

    public string Field { get; set; } = null!;

    public int PersonnelId { get; set;  }

    public virtual Personnel Personnel { get; set; } = null!;

    public virtual ResearcherWork? ResearcherWork { get; set; }
}
