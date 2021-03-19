Funcionalidade: Alterar
	Alterar o cadastro de um aplicativo existente

Cenário: Alterar um aplicativo com sucesso
	Dado que o cadastro de um aplicativo será alterado
	Quando todos os dados estão do aplicativo corretos 
	Então retorna Ok com as informações do aplicativo

Cenário: Alterar um aplicativo com erro
	Dado que o cadastro de um aplicativo será alterado
	Quando alguma informação do aplicativo está inválida
	Então retorna BadRequest com validações do aplicativo