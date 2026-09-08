using System;
using System.Collections.Generic;

namespace ServSegFacilitiesAPI.Domains;

public partial class registroPonto
{
    public int registroPontoId { get; set; }

    public int usuarioId { get; set; }

    public double latitude { get; set; }

    public double longitude { get; set; }

    public DateTime dataHoraPonto { get; set; }

    public byte[]? fotoPonto { get; set; }

    public bool statusRegistroPonto { get; set; }

    public int tipoRegistroId { get; set; }

    public double precisao { get; set; }

    public virtual ICollection<historicoRegistroPonto> historicoRegistroPontoregistroPontoEntrada { get; set; } = new List<historicoRegistroPonto>();

    public virtual ICollection<historicoRegistroPonto> historicoRegistroPontoregistroPontoSaida { get; set; } = new List<historicoRegistroPonto>();

    public virtual tipoRegistro tipoRegistro { get; set; } = null!;

    public virtual usuario usuario { get; set; } = null!;
}
