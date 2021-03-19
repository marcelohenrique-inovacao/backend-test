Funcionalidade: Remover
	Excluir um aplicativo existente

Cenário: Excluir um aplicativo com sucesso
	Dado que um aplicativo será excluído
	Quando todos os dados do aplicativo estão corretos 
	E não está vinculado à um desenvolvedor
	Então retorna Ok com as informações do aplicativo

Cenário: Excluir um aplicativo que está vinculado à um desenvolvedor
	Dado que um aplicativo será excluído
	Quando todos os dados do aplicativo estão corretos 
	E estiver vinculado à um desenvolvedor
	Então retorna BadRequest com validações do aplicativo

Cenário: Excluir um aplicativo com erro
	Dado que um aplicativo será excluído
	Quando alguma informação do aplicativo está inválida
	Então retorna BadRequest com validações do aplicativo