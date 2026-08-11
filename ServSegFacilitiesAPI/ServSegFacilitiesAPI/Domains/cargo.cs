using System;
using System.Collections.Generic;

namespace ServSegFacilitiesAPI.Domains;

public partial class cargo
{
    public int cargoId { get; set; }

    public string nomeCargo { get; set; } = null!;

    public virtual ICollection<usuario> usuario { get; set; } = new List<usuario>();
}
