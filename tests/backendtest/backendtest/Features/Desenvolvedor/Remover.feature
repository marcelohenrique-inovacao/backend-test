﻿Funcionalidade: Remover
	Excluir um desenvolvedor existente

Cenário: Excluir um desenvolvedor com sucesso
	Dado que um desenvolvedor será excluído
	Quando todos os dados do desenvolvedor estão corretos
	Então retorna Ok com as informações do desenvolvedor


Cenário: Excluir um desenvolvedor com erro
	Dado que um desenvolvedor será excluído
	Quando alguma informação do desenvolvedor está inválida
	Então retorna BadRequest com validações