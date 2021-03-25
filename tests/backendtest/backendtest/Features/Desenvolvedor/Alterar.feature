Funcionalidade: Alterar
	Alterar o cadastro de um desenvolvedor existente

Cenário: Alterar um desenvolvedor com sucesso
	Dado que todos os dados do desenvolvedor estão corretos 
	Quando eu tento alterar
	Então retorna Ok

Cenário: Alterar um desenvolvedor com erro
	Dado que alguma informação do desenvolvedor está inválida
	Quando eu tento alterar
	Então retorna BadRequest