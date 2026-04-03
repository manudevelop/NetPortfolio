# .NET Portfolio

Portfolio profesional de **Manuel David Ruiz Rubio** - Senior Software Engineer

## Tech Stack

- **.NET 8.0** - Blazor WebAssembly
- **Clean Architecture** - Separación de responsabilidades
- **CSS Custom Properties** - Theming oscuro/claro
- **GitHub Actions** - CI/CD

## Estructura

```
src/Portfolio.Client/
├── Pages/           # Home, About, Projects, Experience, Contact
├── Components/      # NavBar, Footer, ProjectCard, TimelineItem
├── Models/          # PortfolioData, Project, Skill, WorkExperience
├── Services/        # IPortfolioDataService
└── wwwroot/data/    # portfolio.json
```

## Commands

```bash
# Build
dotnet build

# Run dev con hot reload
dotnet watch --project src/Portfolio.Client

# Publish
dotnet publish src/Portfolio.Client -c Release -o ./publish

# Format code
dotnet format --include "**/*.cs"
```

## Deployment

Despliegue automático a GitHub Pages via GitHub Actions en cada push a `main`.

## Datos

Los datos del portfolio se cargan desde `wwwroot/data/portfolio.json` - editable sin recompilar.
