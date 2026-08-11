using System;
using System.Collections.Generic;

namespace ServSegFacilitiesAPI.Domains;

public partial class registroPonto
{
    public int registroPontoId { get; set; }

    public int usuarioId { get; set; }

    public string? latitude { get; set; }

    public string? longitude { get; set; }

    public DateTime dataHoraPonto { get; set; }

    public bool status { get; set; }

    public int tipoRegistroId { get; set; }

    public virtual tipoRegistro tipoRegistro { get; set; } = null!;

    public virtual usuario usuario { get; set; } = null!;
}
