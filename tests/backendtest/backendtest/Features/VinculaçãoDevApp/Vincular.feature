Funcionalidade: Vincular
	Permitir o vínculo entre um desenvolvedor e um aplicativo

Cenário: Vincular um desenvolvedor à um aplicativo com sucesso
	Dado que um desenvolvedor será vinculado à um aplicativo
	Quando todos os dados do vínculo estão corretos 
	Então retorna Ok com as informações do vínculo

Cenário: Vincular um desenvolvedor à um aplicativo com erro
	Dado que um desenvolvedor será vinculado à um aplicativo
	Quando alguma informação do vínculo está inválida
	Então retorna BadRequest com validações do vínculo