Funcionalidade: Adicionar
	Permitir o cadastro de um novo aplicativo

Cenário: Cadastrar um aplicativo com sucesso
	Dado que um aplicativo será cadastrado
	Quando todos os dados do aplicativo estão corretos 
	Então retorna Ok com as informações do aplicativo

Cenário: Cadastrar um aplicativo com erro
	Dado que um aplicativo será cadastrado
	Quando alguma informação do aplicativo está inválida
	Então retorna BadRequest com validações do aplicativo