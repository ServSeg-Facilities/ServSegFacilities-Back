using System;
using System.Collections.Generic;

namespace ServSegFacilitiesAPI.Domains;

public partial class usuario
{
    public int usuarioId { get; set; }

    public string nome { get; set; } = null!;

    public string email { get; set; } = null!;

    public int cargoId { get; set; }

    public int empresaId { get; set; }

    public byte[]? senha { get; set; }

    public virtual cargo cargo { get; set; } = null!;

    public virtual empresa empresa { get; set; } = null!;

    public virtual ICollection<registroPonto> registroPonto { get; set; } = new List<registroPonto>();
}
