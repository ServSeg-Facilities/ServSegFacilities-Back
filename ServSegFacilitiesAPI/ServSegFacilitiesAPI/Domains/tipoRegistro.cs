using System;
using System.Collections.Generic;

namespace ServSegFacilitiesAPI.Domains;

public partial class tipoRegistro
{
    public int tipoRegistroId { get; set; }

    public string nomeTipoRegistro { get; set; } = null!;

    public virtual ICollection<historicoRegistroPonto> historicoRegistroPonto { get; set; } = new List<historicoRegistroPonto>();

    public virtual ICollection<registroPonto> registroPonto { get; set; } = new List<registroPonto>();
}
