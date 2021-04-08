# Como rodar a aplicação
1. Copiar os arquivos baixando ou clonando o repositório
2. Abrir o CMD e ir no diretório raiz da aplicação usando o comando "cd .../backend-test"
3. Digitar o comando "docker compose up"
4. Após a finalização abrir o banco de dados e rodar o arquivo criacao-banco-docker.sql para inserir alguns dados iniciais."Etapa será alterada para usar o startup da aplicação"
5. A aplicação responde no http://localhost:80 e https://localhost:5001.
6. A url do swagger é https://localhost:5001/swagger/index.html
 


# Tabelas
- Foi modificada a relação entre Desenvolvedor e Aplicativo para N : N.

---

# Regras de negócio

- Um aplicativo pode ter um desenvolvedor responsável, e N desenvolvedores.
- Um desenvolvedor não pode ser responsável por mais de um aplicativo.
- Um desenvolvedor pode participar no desenvolvimento de no máximo 3 aplicativos.
- Não pode excluir um Aplicativo se houver Desenvolvedor vinculado.

---