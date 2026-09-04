using System;
using System.Collections.Generic;

namespace ServSegFacilitiesAPI.Domains;

public partial class tipoRegistro
{
    public int tipoRegistroId { get; set; }

    public string nomeTipoRegistro { get; set; } = null!;

    public virtual ICollection<registroPonto> registroPonto { get; set; } = new List<registroPonto>();
}
