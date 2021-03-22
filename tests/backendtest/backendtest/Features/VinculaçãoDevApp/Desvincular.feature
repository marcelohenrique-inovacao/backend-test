Funcionalidade: Desvincular
	Permitir o desvínculo entre um desenvolvedor e um aplicativo

Cenário: Desvincular um desenvolvedor de um aplicativo com sucesso
	Dado que um desenvolvedor será desvinculado de um aplicativo
	Quando todos os dados do desvínculo estão corretos 
	Então retorna Ok com as informações do desvínculo

Cenário: Desvincular um desenvolvedor de um aplicativo com erro
	Dado que um desenvolvedor será desvinculado de um aplicativo
	Quando alguma informação do desvínculo está inválida
	Então retorna BadRequest com validações do desvínculo

Cenário: Remover a responsabilidade de um Desenvolvedor com sucesso
	Dado será removido sua responsabilidade de um aplicativo
	Quando todos os dados da remoção estão corretos
	Então retorna Ok com as informações do desvínculo

Cenário: Remover a responsabilidade de um Desenvolvedor com erro
	Dado será removido sua responsabilidade de um aplicativo
	Quando alguma informação do remoção está inválida
	Então retorna BadRequest com validações do desvínculo