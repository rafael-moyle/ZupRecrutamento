CREATE TABLE funcionario
( 
	email_corporativo nvarchar(255) NOT NULL,
	email_pessoal nvarchar(255),
	id int IDENTITY(1,1) NOT NULL,
	lider_id int,
	nome nvarchar(255) NOT NULL,
	numero_chapa nvarchar(255) NOT NULL,
	senha nvarchar(255),
	sobrenome nvarchar(255),
	telefone nvarchar(255)
)

go

ALTER TABLE funcionario
	ADD CONSTRAINT pk_funcionario PRIMARY KEY CLUSTERED (id ASC)

go


ALTER TABLE funcionario
	ADD CONSTRAINT fk_funcionario__lider FOREIGN KEY (lider_id) REFERENCES funcionario(id)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go


ALTER TABLE funcionario
	ADD CONSTRAINT uk_funcionario__numero_chapa UNIQUE (numero_chapa)

go
