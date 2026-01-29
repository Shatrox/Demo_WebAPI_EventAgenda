# Web API - Event Agenda

## Types de projet
- Backend: `ASP.NET API`
- Frontend: `Client React` (Pour exemple)
- Database: ` MS SQL Server`

## Architecture logiciel
Pattern: Clean Architecture

### Domain
Les elements necessaire du projet
- Models (+ Validations)
- Exceptions
- Enums
- ...

### ApplicationCore
Traitement des `Use Cases` du projet.
- Implementation des regles metier sous forme de services
- Definition des dependances via des interfaces

### Presentation
Les projets qui sont utilisable par l'utilisateur final.
Exemple de projet:
- Web API (ASP.NET Core)
- App Console
- Application Desktop en WPF
- ...

### Infrastructure
Les projets qui permettent d'acceder aux ressources externes.
Exemple de acces:
- Base de donnees
- Mail
- Web API externes
- ...

