using System;
using System.Collections.Generic;

namespace LibraryDomain.Model;

public partial class ResearcherWork
{
    public int Id { get; set; }

    public int ResearcherId { get; set; }

    public int ScientificWorkId { get; set; }

    public string Contribution { get; set; } = null!;

    public byte[] CreatedAt { get; set; } = null!;

    public virtual ScientificWork IdNavigation { get; set; } = null!;
}
