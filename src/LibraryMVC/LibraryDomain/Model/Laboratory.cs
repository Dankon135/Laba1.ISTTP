﻿using System;
using System.Collections.Generic;

namespace LibraryDomain.Model;

public partial class Laboratory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int DepartamentId { get; set; }

    public virtual Departament Departament { get; set; } = null!;

    public virtual ICollection<Personnel> Personnel { get; set; } = new List<Personnel>();
}
