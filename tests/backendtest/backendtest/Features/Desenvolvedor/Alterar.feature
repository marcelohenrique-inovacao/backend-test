Funcionalidade: Alterar
	Alterar o cadastro de um desenvolvedor existente

Cenário: Alterar um desenvolvedor com sucesso
	Dado que o cadastro de um desenvolvedor será alterado
	Quando todos os dados do desenvolvedor estão corretos 
	Então retorna Ok com as informações do desenvolvedor

Cenário: Alterar um desenvolvedor com erro
	Dado que o cadastro de um desenvolvedor será alterado
	Quando alguma informação do desenvolvedor está inválida
	Então retorna BadRequest com validações do desenvolvedor 