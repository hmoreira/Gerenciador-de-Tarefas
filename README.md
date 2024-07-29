# Gerenciador de Tarefas

 Avaliação foi feita usando Visual Studio 2022, .NET 6 SDK, Docker, SQL Server 2022 e SQL Server Management Studio (SSMS).

 Pra teste da API :

1. Na raiz do projeto executar <b>docker-composer up</b> pra criar os containers da API e do SQL Server no Docker;
2. Em seguida executar o script <b>TaskManager.sql</b> (localizado na raiz do projeto) no SSMS pra criacao da base de dados do projeto e do usuario master;
3. Pra login do usuário master usar o endpoint <b>/api/user/login</b> com username <b>master</b> e senha <b>e10VFOqJKiB1XTh</b>;
4. Após o login utilizar o token pra autenticacao dos endpoints restantes;
5. Pra execução dos testes unitários executar <b>dotnet test</b> dentro do pasta <b>TaskManager.UnitTests</b>;
6. Não foram feitos testes integrados mas com refactoring da API é possivel criar um projeto de teste pra isso.

# Pontos de Melhoria

- Não permitir remoção do usuario master;
- Não permitir a exclusão de usuários com tarefas associadas;
- Inclusão do projeto de testes no Docker Compose.
 
