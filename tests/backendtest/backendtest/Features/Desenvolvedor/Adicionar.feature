Funcionalidade: Adicionar
	Permitir o cadastro de um novo desenvolvedor

Cenário: Cadastrar um desenvolvedor com sucesso
	Dado que todos os dados do desenvolvedor estão corretos 
	Quando eu tento cadastrar
	Então retorna Ok

Cenário: Cadastrar um desenvolvedor com erro
	Dado que o CPF do desenvolvedor está inválido
	Quando eu tento cadastrar
	Então retorna BadRequest

Cenário: Cadastrar um desenvolvedor com email inválido
	Dado que o Email do desenvolvedor está inválido
	Quando eu tento cadastrar
	Então retorna BadRequest