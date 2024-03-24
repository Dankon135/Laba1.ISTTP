using System;
using System.Collections.Generic;

namespace LibraryDomain.Model;

public partial class Personnel
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public int DepartamentId { get; set; }

    public int LaboratoryId { get; set; }

    public int PositionId { get; set; }

    public DateOnly PositionStart { get; set; }

    public DateOnly PositionEnd { get; set; }

    public int PersonnelId { get; set; }

    public virtual Departament Departament { get; set; } = null!;

    public virtual Laboratory Laboratory { get; set; } = null!;

    public virtual Position Position { get; set; } = null!;

    public virtual ICollection<ZtTable> ZtTables { get; set; } = new List<ZtTable>();
}
