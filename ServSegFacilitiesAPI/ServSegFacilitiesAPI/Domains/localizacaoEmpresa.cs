using System;
using System.Collections.Generic;

namespace ServSegFacilitiesAPI.Domains;

public partial class localizacaoEmpresa
{
    public int localizacaoEmpresaId { get; set; }

    public int empresaId { get; set; }

    public string latitude { get; set; } = null!;

    public string longitude { get; set; } = null!;

    public decimal? precisao { get; set; }

    public virtual empresa empresa { get; set; } = null!;
}
