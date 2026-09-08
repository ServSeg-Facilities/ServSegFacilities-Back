CREATE DATABASE ServSeg_Facilities;
GO
USE ServSeg_Facilities
GO

CREATE TABLE cargo (
  cargoId INT PRIMARY KEY IDENTITY(1, 1),
  nomeCargo VARCHAR(50) NOT NULL
)
GO

CREATE TABLE empresa (
  empresaId INT PRIMARY KEY IDENTITY(1, 1),
  cnpj VARCHAR(18) UNIQUE NOT NULL,
  razaoSocial VARCHAR(150) NOT NULL,
  nomeFantasia VARCHAR(100),
  telefone VARCHAR(20),
  email VARCHAR(150),
  cep VARCHAR(9) NOT NULL,
  logradouro VARCHAR(150) NOT NULL,
  numero VARCHAR(20) NOT NULL,
  complemento VARCHAR(100),
  bairro VARCHAR(100) NOT NULL,
  cidade VARCHAR(100) NOT NULL,
  estado CHAR(2) NOT NULL
)
GO

CREATE TABLE usuario (
  usuarioId INT PRIMARY KEY IDENTITY(1, 1),
  nome VARCHAR(100) NOT NULL,
  email VARCHAR(150) UNIQUE NOT NULL,
  cargoId INT NOT NULL,
  empresaId INT NOT NULL,
  senha VARBINARY(255) NOT NULL DEFAULT 0x,

  CONSTRAINT FK_usuario_cargo_cargoId FOREIGN KEY (cargoId) REFERENCES cargo(cargoId),
  CONSTRAINT FK_usuario_empresa_empresaId FOREIGN KEY (empresaId) REFERENCES empresa(empresaId)
)
GO

CREATE TABLE localizacaoEmpresa (
  localizacaoEmpresaId INT PRIMARY KEY IDENTITY(1, 1),
  empresaId INT NOT NULL,
  latitude VARCHAR(15) NOT NULL,
  longitude VARCHAR(15) NOT NULL,
  precisao decimal(5,2),

  CONSTRAINT FK_localizacaoEmpresa_empresa_empresaId FOREIGN KEY (empresaId) REFERENCES empresa(empresaId)
)
GO

CREATE TABLE tipoRegistro (
  tipoRegistroId INT PRIMARY KEY IDENTITY(1, 1),
  nomeTipoRegistro VARCHAR(30) NOT NULL
)
GO

CREATE TABLE registroPonto (
  registroPontoId INT PRIMARY KEY IDENTITY(1, 1),
  usuarioId INT NOT NULL,
  latitude FLOAT NOT NULL,
  longitude FLOAT NOT NULL,
  dataHoraPonto datetime NOT NULL DEFAULT getdate(),
  fotoPonto VARBINARY(MAX),
  statusRegistroPonto BIT NOT NULL,
  tipoRegistroId INT NOT NULL,
  precisao FLOAT NOT NULL DEFAULT 0,

  CONSTRAINT FK_RegistroPonto_usuario_usuarioId FOREIGN KEY (usuarioId) REFERENCES usuario(usuarioId),
  CONSTRAINT FK_RegistroPonto_tipoRegistro_tipoRegistroId FOREIGN KEY (tipoRegistroId) REFERENCES tipoRegistro(tipoRegistroId)
)
GO

CREATE TABLE historicoRegistroPonto (
  historicoId INT PRIMARY KEY IDENTITY(1, 1),
  registroPontoEntradaId INT NOT NULL,
  registroPontoSaidaId INT NULL,

  constraint FK_historicoRegistroPonto_registroPonto_registroPontoEntradaId FOREIGN KEY (registroPontoEntradaId) REFERENCES registroPonto(registroPontoId),
  constraint FK_historicoRegistroPonto_registroPonto_registroPontoSaidaId FOREIGN KEY (registroPontoSaidaId) REFERENCES registroPonto(registroPontoId)
);
GO


CREATE TRIGGER TR_registroPonto_Historico
ON registroPonto
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    /*
        ENTRADA
        Cria um histórico para a entrada,
        desde que ainda não exista um histórico
        para aquele registro.
    */
    INSERT INTO historicoRegistroPonto (
        registroPontoEntradaId
    )
    SELECT
        i.registroPontoId
    FROM inserted i
    INNER JOIN tipoRegistro t
        ON t.tipoRegistroId = i.tipoRegistroId
    WHERE t.nomeTipoRegistro = 'Entrada'
      AND NOT EXISTS (
          SELECT 1
          FROM historicoRegistroPonto h
          WHERE h.registroPontoEntradaId = i.registroPontoId
      );


    /*
        SAÍDA
        Procura a entrada do mesmo usuário,
        no mesmo dia, que ainda não possui saída.
    */
    UPDATE h
    SET h.registroPontoSaidaId = i.registroPontoId
    FROM historicoRegistroPonto h
    INNER JOIN registroPonto entrada
        ON entrada.registroPontoId = h.registroPontoEntradaId
    INNER JOIN inserted i
        ON i.usuarioId = entrada.usuarioId
    INNER JOIN tipoRegistro t
        ON t.tipoRegistroId = i.tipoRegistroId
    WHERE t.nomeTipoRegistro = 'Saída'
      AND h.registroPontoSaidaId IS NULL
      AND CAST(entrada.dataHoraPonto AS DATE) =
          CAST(i.dataHoraPonto AS DATE)
      AND entrada.dataHoraPonto < i.dataHoraPonto;
END;
GO