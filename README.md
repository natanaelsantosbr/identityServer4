# identityServer4
Identity Server 4 + API + MVC

Tutorial
https://www.youtube.com/watch?v=HJQ2-sJURvA&ab_channel=IdentityServer

Comando para criar o banco de dados
<br/><br/>
dotnet ef migrations add InitialIdentityServerMigration -c PersistedGrantDbContext
<br/><br/>
dotnet ef migrations add InitialIdentityServerMigration -c ConfigurationDbContext
<br/><br/>
dotnet ef database update -c PersistedGrantDbContext
<br/><br/>
dotnet ef database update -c ConfigurationDbContext
<br/>
Aproveitem!!!
