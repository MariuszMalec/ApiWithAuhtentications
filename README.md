# Projekty w solucji

1. ApiWithAuhtenticationApiKey - API projekt
- middleware
- controllers
- ApiKey in appsettings
2. ApiWithAuhtenticationBearer - API projekt (nie dziala!)
- models
- Services
3. WebAppiNbaPlayers - API projekt (osobny projekt)
- controllers
- models
- Services
- BasicAuthenticationHandler - basic authorize - username, password
- UsersStorage
4. WebAppiUsers - API project.
- DataBase SqlLite UsersDb.db users
- context
- controllers
- migration
- models
- repository
- Services
- Middleware
- ApiKey in appsettings
5. WepAppAccessToApi - MVC project do strzelania po Api
- controllers
- Register Login Areas

## Start projektu
1. prawy przycisk na solucji "Set Startup Projects"
2. wybieramy Api i MVC project
3. Sciagamy Api WebAppiUsers. Zewnetrzny projekt
4. Start
