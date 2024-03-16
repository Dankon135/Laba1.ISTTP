using System;
using System.Collections.Generic;

namespace LibraryDomain.Model;

public partial class Department
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Laboratory> Laboratories { get; set; } = new List<Laboratory>();

    public virtual Personnel? Personnel { get; set; }
}
