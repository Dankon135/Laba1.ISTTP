using System;
using System.Collections.Generic;

namespace LibraryDomain.Model;

public partial class Position: Entity
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Personnel> Personnel { get; set; } = new List<Personnel>();
}
