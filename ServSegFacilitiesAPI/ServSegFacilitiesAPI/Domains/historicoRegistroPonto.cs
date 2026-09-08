using System;
using System.Collections.Generic;

namespace ServSegFacilitiesAPI.Domains;

public partial class historicoRegistroPonto
{
    public int historicoId { get; set; }

    public int registroPontoEntradaId { get; set; }

    public int? registroPontoSaidaId { get; set; }

    public virtual registroPonto registroPontoEntrada { get; set; } = null!;

    public virtual registroPonto? registroPontoSaida { get; set; }
}
