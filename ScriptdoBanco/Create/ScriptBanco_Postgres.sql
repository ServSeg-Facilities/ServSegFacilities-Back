-- ============================================================
-- SCRIPT DE CRIACAO DO BANCO POSTGRESQL (SUPABASE)
-- SERVE-SEG FACILITIES
-- ============================================================

DROP TABLE IF EXISTS "historicoRegistroPonto" CASCADE;
DROP TABLE IF EXISTS "registroPonto" CASCADE;
DROP TABLE IF EXISTS "localizacaoEmpresa" CASCADE;
DROP TABLE IF EXISTS "usuario" CASCADE;
DROP TABLE IF EXISTS "tipoRegistro" CASCADE;
DROP TABLE IF EXISTS "empresa" CASCADE;
DROP TABLE IF EXISTS "cargo" CASCADE;

CREATE TABLE "cargo" (
  "cargoId" SERIAL PRIMARY KEY,
  "nomeCargo" VARCHAR(50) NOT NULL
);

CREATE TABLE "empresa" (
  "empresaId" SERIAL PRIMARY KEY,
  "cnpj" VARCHAR(18) UNIQUE NOT NULL,
  "razaoSocial" VARCHAR(150) NOT NULL,
  "nomeFantasia" VARCHAR(100),
  "telefone" VARCHAR(20),
  "email" VARCHAR(150),
  "cep" VARCHAR(9) NOT NULL,
  "logradouro" VARCHAR(150) NOT NULL,
  "numero" VARCHAR(20) NOT NULL,
  "complemento" VARCHAR(100),
  "bairro" VARCHAR(100) NOT NULL,
  "cidade" VARCHAR(100) NOT NULL,
  "estado" CHAR(2) NOT NULL
);

CREATE TABLE "usuario" (
  "usuarioId" SERIAL PRIMARY KEY,
  "nome" VARCHAR(100) NOT NULL,
  "email" VARCHAR(150) UNIQUE NOT NULL,
  "cargoId" INT NOT NULL,
  "empresaId" INT NOT NULL,
  "senha" BYTEA NOT NULL DEFAULT ''::bytea,

  CONSTRAINT "FK_usuario_cargo_cargoId" FOREIGN KEY ("cargoId") REFERENCES "cargo"("cargoId"),
  CONSTRAINT "FK_usuario_empresa_empresaId" FOREIGN KEY ("empresaId") REFERENCES "empresa"("empresaId")
);

CREATE TABLE "localizacaoEmpresa" (
  "localizacaoEmpresaId" SERIAL PRIMARY KEY,
  "empresaId" INT NOT NULL,
  "latitude" VARCHAR(15) NOT NULL,
  "longitude" VARCHAR(15) NOT NULL,
  "precisao" DECIMAL(5,2),

  CONSTRAINT "FK_localizacaoEmpresa_empresa_empresaId" FOREIGN KEY ("empresaId") REFERENCES "empresa"("empresaId")
);

CREATE TABLE "tipoRegistro" (
  "tipoRegistroId" SERIAL PRIMARY KEY,
  "nomeTipoRegistro" VARCHAR(30) NOT NULL
);

CREATE TABLE "registroPonto" (
  "registroPontoId" SERIAL PRIMARY KEY,
  "usuarioId" INT NOT NULL,
  "latitude" DOUBLE PRECISION NOT NULL,
  "longitude" DOUBLE PRECISION NOT NULL,
  "dataHoraPonto" TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
  "fotoPonto" BYTEA,
  "status" BOOLEAN NOT NULL DEFAULT TRUE,
  "tipoRegistroId" INT NOT NULL,
  "precisao" DOUBLE PRECISION NOT NULL DEFAULT 0,

  CONSTRAINT "FK_RegistroPonto_usuario_usuarioId" FOREIGN KEY ("usuarioId") REFERENCES "usuario"("usuarioId"),
  CONSTRAINT "FK_RegistroPonto_tipoRegistro_tipoRegistroId" FOREIGN KEY ("tipoRegistroId") REFERENCES "tipoRegistro"("tipoRegistroId")
);

CREATE TABLE "historicoRegistroPonto" (
  "historicoId" SERIAL PRIMARY KEY,
  "registroPontoEntradaId" INT NOT NULL,
  "registroPontoSaidaId" INT NULL,

  CONSTRAINT "FK_historicoRegistroPonto_registroPonto_registroPontoEntradaId" FOREIGN KEY ("registroPontoEntradaId") REFERENCES "registroPonto"("registroPontoId"),
  CONSTRAINT "FK_historicoRegistroPonto_registroPonto_registroPontoSaidaId" FOREIGN KEY ("registroPontoSaidaId") REFERENCES "registroPonto"("registroPontoId")
);

CREATE OR REPLACE FUNCTION fn_tr_registroPonto_Historico()
RETURNS TRIGGER AS $func$
DECLARE
    v_tipo_nome VARCHAR(30);
BEGIN
    SELECT "nomeTipoRegistro" INTO v_tipo_nome 
    FROM "tipoRegistro" 
    WHERE "tipoRegistroId" = NEW."tipoRegistroId";

    IF v_tipo_nome = 'Entrada' THEN
        IF NOT EXISTS (
            SELECT 1 FROM "historicoRegistroPonto"
            WHERE "registroPontoEntradaId" = NEW."registroPontoId"
        ) THEN
            INSERT INTO "historicoRegistroPonto" ("registroPontoEntradaId")
            VALUES (NEW."registroPontoId");
        END IF;
    END IF;

    IF v_tipo_nome = 'Saída' THEN
        UPDATE "historicoRegistroPonto" h
        SET "registroPontoSaidaId" = NEW."registroPontoId"
        FROM "registroPonto" entrada
        WHERE entrada."registroPontoId" = h."registroPontoEntradaId"
          AND entrada."usuarioId" = NEW."usuarioId"
          AND entrada."tipoRegistroId" = (SELECT "tipoRegistroId" FROM "tipoRegistro" WHERE "nomeTipoRegistro" = 'Entrada' LIMIT 1)
          AND h."registroPontoSaidaId" IS NULL
          AND DATE(entrada."dataHoraPonto") = DATE(NEW."dataHoraPonto")
          AND entrada."dataHoraPonto" < NEW."dataHoraPonto";
    END IF;

    RETURN NEW;
END;
$func$ LANGUAGE plpgsql;

DROP TRIGGER IF EXISTS "TR_registroPonto_Historico" ON "registroPonto";
CREATE TRIGGER "TR_registroPonto_Historico"
AFTER INSERT ON "registroPonto"
FOR EACH ROW
EXECUTE FUNCTION fn_tr_registroPonto_Historico();
