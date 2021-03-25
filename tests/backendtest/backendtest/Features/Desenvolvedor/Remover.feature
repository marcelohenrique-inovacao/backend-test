Funcionalidade: Remover
	Excluir um desenvolvedor existente

Cenário: Excluir um desenvolvedor com sucesso
	Dado que todos os dados do desenvolvedor estão corretos
	Quando eu tento excluir
	Então retorna Ok


Cenário: Excluir um desenvolvedor com erro
	Dado que alguma informação do desenvolvedor está inválida
	Quando eu tento excluir
	Então retorna BadRequest

Cenário: Excluir um desenvolvedor vinculado à um Aplicativo
	Dado que todos os dados do desenvolvedor estão corretos
	E que ele estiver vinculado à um Aplicativo
	Quando eu tento excluir
	Então retorna BadRequest