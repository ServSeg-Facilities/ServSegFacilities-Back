USE ServSeg_Facilities;
GO


/* =========================================================
   1. CARGOS
   ========================================================= */

INSERT INTO cargo (nomeCargo)
VALUES
    ('Administrador'),
    ('Gerente'),
    ('Supervisor'),
    ('Funcionario'),
    ('Tecnico');
GO


/* =========================================================
   2. TIPOS DE REGISTRO
   ========================================================= */

INSERT INTO tipoRegistro (nomeTipoRegistro)
VALUES
    ('Entrada'),
    ('Saída'),
    ('Inicio Intervalo'),
    ('Fim Intervalo');
GO


/* =========================================================
   3. EMPRESAS
   ========================================================= */

INSERT INTO empresa
(
    cnpj,
    razaoSocial,
    nomeFantasia,
    telefone,
    email,
    cep,
    logradouro,
    numero,
    complemento,
    bairro,
    cidade,
    estado
)
VALUES

(
    '45543915060834',
    'CARREFOUR COMERCIO E INDUSTRIA LTDA',
    'Carrefour Express Amazonas',
    '(11) 95042-3066',
    'carrefourexpress@email.com',
    '09520-070',
    'Rua Amazonas',
    '383',
    NULL,
    'Centro',
    'Sao Caetano do Sul',
    'SP'
),

(
    '26563652028642',
    'REDE INTEGRADA DE LOJAS DE CONVENIENCIA E PROXIMIDADE S.A.',
    'OXXO Monte Alegre',
    '(19) 3423-8000',
    'oxxomontealegre@email.com',
    '09531-110',
    'Rua Monte Alegre',
    '163',
    'Loja A',
    'Santo Antonio',
    'Sao Caetano do Sul',
    'SP'
),

(
    '26563652032674',
    'REDE INTEGRADA DE LOJAS DE CONVENIENCIA E PROXIMIDADE S.A.',
    'OXXO Serafim Estacao',
    '(19) 3423-8000',
    'oxxoserafim@email.com',
    '09510-220',
    'Rua Serafim Constantino',
    '30',
    'Loja',
    'Centro',
    'Sao Caetano do Sul',
    'SP'
),

(
    '26563652038109',
    'REDE INTEGRADA DE LOJAS DE CONVENIENCIA E PROXIMIDADE S.A.',
    'OXXO Goias Cafe',
    '(19) 3423-8000',
    'oxxogoias@email.com',
    '09521-300',
    'Avenida Goias',
    '742',
    NULL,
    'Santo Antonio',
    'Sao Caetano do Sul',
    'SP'
),

(
    '61412110042907',
    'DROGARIA SAO PAULO S.A.',
    'Drogaria Sao Paulo',
    '(11) 4226-0213',
    'drogariasaopaulo@email.com',
    '09510-111',
    'Rua Manoel Coelho',
    '716',
    NULL,
    'Centro',
    'Sao Caetano do Sul',
    'SP'
);
GO


/* =========================================================
   4. LOCALIZAÇÕES DAS EMPRESAS
   ========================================================= */

INSERT INTO localizacaoEmpresa
(
    empresaId,
    latitude,
    longitude,
    precisao
)
VALUES

(
    1,
    '-23.618900',
    '-46.572500',
    300.00
),

(
    2,
    '-23.611700',
    '-46.567900',
    300.00
),

(
    3,
    '-23.615300',
    '-46.571600',
    300.00
),

(
    4,
    '-23.611300',
    '-46.571500',
    300.00
),

(
    5,
    '-23.614500',
    '-46.570500',
    300.00
);
GO


/* =========================================================
   5. USUÁRIOS
   Senha de todos: 123456
   ========================================================= */

INSERT INTO usuario
(
    nome,
    email,
    cargoId,
    empresaId,
    senha
)
VALUES

(
    'Administrador Sistema',
    'admin@email.com',
    1,
    1,
    HASHBYTES('SHA2_256', '123456')
),

(
    'Carlos Henrique',
    'carlos@email.com',
    2,
    1,
    HASHBYTES('SHA2_256', '123456')
),

(
    'Marcos Oliveira',
    'marcos@email.com',
    3,
    1,
    HASHBYTES('SHA2_256', '123456')
),

(
    'Joao Silva',
    'joao@email.com',
    4,
    1,
    HASHBYTES('SHA2_256', '123456')
),

(
    'Pedro Santos',
    'pedro@email.com',
    4,
    2,
    HASHBYTES('SHA2_256', '123456')
),

(
    'Lucas Almeida',
    'lucas@email.com',
    4,
    3,
    HASHBYTES('SHA2_256', '123456')
),

(
    'Rafael Costa',
    'rafael@email.com',
    3,
    4,
    HASHBYTES('SHA2_256', '123456')
),

(
    'Gabriel Souza',
    'gabriel@email.com',
    4,
    5,
    HASHBYTES('SHA2_256', '123456')
);
GO


/* =========================================================
   6. ENTRADAS
   =========================================================

   dataHoraPonto NÃO é informado.

   O banco utilizará:
       DEFAULT GETDATE()

   Portanto todos os registros abaixo receberão
   automaticamente a data/hora atual.

   Cada usuário possui apenas UMA entrada.
   ========================================================= */

INSERT INTO registroPonto
(
    usuarioId,
    latitude,
    longitude,
    statusRegistroPonto,
    tipoRegistroId
)
VALUES

-- Administrador - Carrefour
(
    1,
    -23.618850,
    -46.572450,
    1,
    1
),

-- Carlos - Carrefour
(
    2,
    -23.618870,
    -46.572470,
    1,
    1
),

-- Marcos - Carrefour
(
    3,
    -23.618880,
    -46.572480,
    1,
    1
),

-- Joao - Carrefour
(
    4,
    -23.618890,
    -46.572490,
    1,
    1
),

-- Pedro - OXXO Monte Alegre
(
    5,
    -23.611650,
    -46.567850,
    1,
    1
),

-- Lucas - OXXO Serafim
(
    6,
    -23.615250,
    -46.571550,
    1,
    1
),

-- Rafael - OXXO Goias
(
    7,
    -23.611250,
    -46.571450,
    1,
    1
),

-- Gabriel - Drogaria
(
    8,
    -23.614450,
    -46.570450,
    1,
    1
);
GO


/* =========================================================
   7. SAÍDAS
   =========================================================

   Novamente, dataHoraPonto NÃO é informado.

   O banco utilizará GETDATE().

   Como este INSERT acontece depois do INSERT das entradas,
   a trigger encontrará a entrada correspondente de cada
   usuário e preencherá registroPontoSaidaId.
   ========================================================= */

INSERT INTO registroPonto
(
    usuarioId,
    latitude,
    longitude,
    statusRegistroPonto,
    tipoRegistroId
)
VALUES

-- Administrador - Carrefour
(
    1,
    -23.618850,
    -46.572450,
    1,
    2
),

-- Carlos - Carrefour
(
    2,
    -23.618870,
    -46.572470,
    1,
    2
),

-- Marcos - Carrefour
(
    3,
    -23.618880,
    -46.572480,
    1,
    2
),

-- Joao - Carrefour
(
    4,
    -23.618890,
    -46.572490,
    1,
    2
),

-- Pedro - OXXO Monte Alegre
(
    5,
    -23.611650,
    -46.567850,
    1,
    2
),

-- Lucas - OXXO Serafim
(
    6,
    -23.615250,
    -46.571550,
    1,
    2
),

-- Rafael - OXXO Goias
(
    7,
    -23.611250,
    -46.571450,
    1,
    2
),

-- Gabriel - Drogaria
(
    8,
    -23.614450,
    -46.570450,
    1,
    2
);
GO


/* =========================================================
   8. CONFERÊNCIA DOS REGISTROS
   ========================================================= */

SELECT
    rp.registroPontoId,
    u.nome AS usuario,
    e.nomeFantasia AS empresa,
    t.nomeTipoRegistro AS tipoRegistro,
    rp.dataHoraPonto,
    rp.latitude,
    rp.longitude,
    rp.statusRegistroPonto
FROM registroPonto rp
INNER JOIN usuario u
    ON u.usuarioId = rp.usuarioId
INNER JOIN empresa e
    ON e.empresaId = u.empresaId
INNER JOIN tipoRegistro t
    ON t.tipoRegistroId = rp.tipoRegistroId
ORDER BY
    rp.usuarioId,
    rp.dataHoraPonto;
GO


/* =========================================================
   9. CONFERÊNCIA DO HISTÓRICO
   ========================================================= */

SELECT
    h.historicoId,

    entrada.registroPontoId AS registroEntradaId,
    entrada.dataHoraPonto AS dataHoraEntrada,

    saida.registroPontoId AS registroSaidaId,
    saida.dataHoraPonto AS dataHoraSaida,

    u.nome AS usuario,
    e.nomeFantasia AS empresa

FROM historicoRegistroPonto h

INNER JOIN registroPonto entrada
    ON entrada.registroPontoId = h.registroPontoEntradaId

LEFT JOIN registroPonto saida
    ON saida.registroPontoId = h.registroPontoSaidaId

INNER JOIN usuario u
    ON u.usuarioId = entrada.usuarioId

INNER JOIN empresa e
    ON e.empresaId = u.empresaId

ORDER BY
    entrada.dataHoraPonto;
GO