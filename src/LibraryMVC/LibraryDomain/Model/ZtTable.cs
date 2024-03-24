using System;
using System.Collections.Generic;

namespace LibraryDomain.Model;

public partial class ZtTable
{
    public int Id { get; set; }

    public int PersonnelId { get; set; }

    public int ScientificWorkId { get; set; }

    public virtual Personnel Personnel { get; set; } = null!;

    public virtual ResearcherWork PersonnelNavigation { get; set; } = null!;
}
