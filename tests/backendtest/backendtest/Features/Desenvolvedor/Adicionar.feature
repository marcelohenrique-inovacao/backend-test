Funcionalidade: Adicionar
	Permitir o cadastro de um novo desenvolvedor

Cenário: Cadastrar um desenvolvedor com sucesso
	Dado que um desenvolvedor será cadastrado
	Quando todos os dados do desenvolvedor estão corretos 
	Então retorna Ok com as informações do desenvolvedor

Cenário: Cadastrar um desenvolvedor com erro
	Dado que um desenvolvedor será cadastrado
	Quando alguma informação do desenvolvedor está inválida
	Então retorna BadRequest com validações do desenvolvedor