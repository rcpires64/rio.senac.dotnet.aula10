-- #### ESTRUTURA
CREATE DATABASE SenacDB;
GO

USE SenacDB;
GO

CREATE LOGIN L_AULA_APP WITH PASSWORD=N'app123';
GO

CREATE USER L_AULA_APP FROM LOGIN L_AULA_APP;
GO
-- GRANT para DML
GRANT SELECT, INSERT, UPDATE, DELETE ON SenacDB.dbo.aluno TO L_AULA_APP;
GO

GRANT SELECT, INSERT, UPDATE, DELETE ON SenacDB.dbo.NotaAluno TO L_AULA_APP;
GO

GRANT SELECT ON SenacDB.dbo.view_aluno_notas TO L_AULA_APP;
GO

CREATE TABLE aluno(
	Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	Nome NVARCHAR(250) NOT NULL,
	Matricula NVARCHAR(100) NOT NULL UNIQUE,
	DataNascimento DATE NOT NULL
);
GO

CREATE TABLE NotaAluno(
	Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	AlunoId UNIQUEIDENTIFIER NOT NULL,
	Nota FLOAT NOT NULL,
	FOREIGN KEY(AlunoId) REFERENCES aluno(Id)
);
GO

CREATE VIEW view_aluno_notas AS 
SELECT a.Nome, a.Matricula, a.DataNascimento, na.Nota 
FROM aluno a
JOIN NotaAluno na ON a.Id = na.AlunoId;



INSERT INTO aluno(Nome, Matricula, DataNascimento) VALUES ('Viviane', '123456', '2010-09-28');

/*
public Guid Id { get; set; }
public int Matricula { get; set; }
public string Nome { get; set; } = string.Empty;
public IEnumerable<string> Conhecimentos { get; set; } = default!;
*/
/*CREATE TABLE Professor(

);*/


-- #### TESTES
select * from aluno;

select CURRENT_TIMESTAMP; -- Datetime
SELECT CONVERT (date, CURRENT_TIMESTAMP); -- DateOnly
SELECT CONVERT (time, CURRENT_TIMESTAMP); -- TimeOnly



select * from aluno;
select Nome, Matricula from aluno;


INSERT INTO NotaAluno(AlunoId, Nota) VALUES ('2b7c6035-23dc-4834-a66c-0798bd17453f', 10.0);
INSERT INTO NotaAluno(AlunoId, Nota) VALUES ('2b7c6035-23dc-4834-a66c-0798bd17453f', 6.7);
INSERT INTO NotaAluno(AlunoId, Nota) VALUES ('2b7c6035-23dc-4834-a66c-0798bd17453f', 7.2);

select * from NotaAluno where AlunoId = '2b7c6035-23dc-4834-a66c-0798bd17453f'



INSERT INTO NotaAluno(AlunoId, Nota) VALUES ("2B7C6035-23DC-4834-A66C-0798BD17453F", 10.0),
											("2B7C6035-23DC-4834-A66C-0798BD17453F", 9.0),
											("2B7C6035-23DC-4834-A66C-0798BD17453F", 8.9);


SELECT * FROM NotaAluno WHERE AlunoId = (SELECT Id FROM aluno WHERE Matricula = '123456')