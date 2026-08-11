
CREATE DATABASE ServSeg_Facilities;
GO
USE ServSeg_Facilities
GO
CREATE TABLE usuario (
  usuarioId INT PRIMARY KEY IDENTITY(1, 1),
  nome VARCHAR(100) NOT NULL,
  senha VARCHAR(255) NOT NULL,
  email VARCHAR(150) UNIQUE NOT NULL,
  cargoId INT NOT NULL,
  empresaId INT NOT NULL
)
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

CREATE TABLE localizacaoEmpresa (
  localizacaoEmpresaId INT PRIMARY KEY IDENTITY(1, 1),
  empresaId INT NOT NULL,
  latitude VARCHAR(15) NOT NULL,
  longitude VARCHAR(15) NOT NULL,
  precisao decimal(5,2)
)
GO

CREATE TABLE registroPonto (
  registroPontoId INT PRIMARY KEY IDENTITY(1, 1),
  usuarioId INT NOT NULL,
  latitude VARCHAR(15),
  longitude VARCHAR(15),
  dataHoraPonto datetime NOT NULL DEFAULT getdate(),
  status BIT NOT NULL,
  tipoRegistroId INT NOT NULL
)
GO

CREATE TABLE tipoRegistro (
  tipoRegistroId INT PRIMARY KEY IDENTITY(1, 1),
  nomeTipoRegistro VARCHAR(30) NOT NULL
)
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Sigla da UF',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'empresa',
@level2type = N'Column', @level2name = 'estado';
GO

ALTER TABLE usuario ADD FOREIGN KEY (cargoId) REFERENCES cargo (cargoId)
GO

ALTER TABLE usuario ADD FOREIGN KEY (empresaId) REFERENCES empresa (empresaId)
GO

ALTER TABLE localizacaoEmpresa ADD FOREIGN KEY (empresaId) REFERENCES empresa (empresaId)
GO

ALTER TABLE registroPonto ADD FOREIGN KEY (usuarioId) REFERENCES usuario (usuarioId)
GO

ALTER TABLE registroPonto ADD FOREIGN KEY (tipoRegistroId) REFERENCES tipoRegistro (tipoRegistroId)
GO

