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

Cenário: Vincular um desenvolvedor à um aplicativo que ele já está vinculado
	Dado que um desenvolvedor será vinculado à um aplicativo
	Quando ele já está vinculado a esse aplicativo
	Então retorna BadRequest com validações do vínculo

Cenário: Tornar um desenvolvedor responsável pelo Aplicativo com sucesso
	Dado que um desenvolvedor será o responsável pelo Aplicativo
	Quando não há ninguém responsável pelo Aplicativo
	E ele não é responsável por outro Aplicativo
	Então retorna Ok com as informações do vínculo

Cenário: Tornar um desenvolvedor responsável pelo Aplicativo mas ele já é responsável por outro
	Dado que um desenvolvedor será o responsável pelo Aplicativo
	Quando não há ninguém responsável pelo Aplicativo
	E ele é responsável por outro Aplicativo
	Então retorna BadRequest com validações do vínculo

Cenário: Tornar um desenvolvedor responsável pelo Aplicativo mas já existe responsável
	Dado que um desenvolvedor será o responsável pelo Aplicativo
	Quando já existe um responsável pelo Aplicativo 
	E ele não é responsável por outro Aplicativo
	Então retorna BadRequest com validações do vínculo