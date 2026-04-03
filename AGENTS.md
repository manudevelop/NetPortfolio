# AGENTS.md — Portfolio de Desarrollador .NET

## Visión General del Proyecto

Construir un portfolio web moderno, rápido y visualmente distintivo para un desarrollador .NET. El sitio debe transmitir **precisión técnica y profesionalismo**, con una identidad visual que evoque la cultura .NET (sistemas tipados, arquitectura limpia, rendimiento).

Stack base: **Blazor WebAssembly** + **ASP.NET Core** (API backend opcional), desplegado en Azure Static Web Apps o GitHub Pages.

---

## Comandos de Build

```bash
# Compilar toda la solución
dotnet build

# Compilar un proyecto específico
dotnet build src/Portfolio.Client/Portfolio.Client.csproj

# Ejecutar en desarrollo
dotnet run --project src/Portfolio.Client

# Publicar para producción
dotnet publish src/Portfolio.Client -c Release -o ./publish

# Limpiar artefactos de build
dotnet clean
```

---

## Comandos de Test

```bash
# Ejecutar todos los tests
dotnet test

# Tests con salida verbosa
dotnet test --verbosity normal

# Ejecutar un solo test por nombre
dotnet test --filter "FullyQualifiedName~NombreDelTest"

# Ejecutar tests en un proyecto específico
dotnet test tests/Portfolio.Tests/Portfolio.Tests.csproj

# Tests con coverage
dotnet test --collect:"XPlat Code Coverage"
```

---

## Linting y Calidad de Código

```bash
# Formatear código
dotnet format --include "**/*.cs"

# Verificar violations de estilo
dotnet format --verify-no-changes --diagnostics

# Verificar analyzer violations
dotnet build --no-restore | grep -E "warning|error"
```

---

## Convenciones de Código

### Convenciones de Nombres

| Elemento | Convención | Ejemplo |
|----------|------------|---------|
| Clases/Records | PascalCase | `Project`, `PortfolioDataService` |
| Interfaces | PascalCase con prefijo `I` | `IPortfolioDataService` |
| Métodos | PascalCase | `GetProjectsAsync()` |
| Propiedades | PascalCase | `public string Name { get; set; }` |
| Campos privados | `_camelCase` | `private IEnumerable<Project> _projects` |
| Parámetros | camelCase | `string projectTitle` |
| Componentes Razor | PascalCase | `<ProjectCard />` |

### Imports

- Usar directivas `using` implícitas (proyectos SDK-style)
- Cualificar explícitamente tipos de terceros o ambiguos
- Agrupar imports: System → Proyecto → Terceros

### Tipos

- Usar `record` para DTOs y modelos de datos
- Usar `class` para servicios y lógica de negocio
- Usar `DateOnly`/`TimeOnly` para fechas/horas
- Usar `string?` para strings nullables (no `String`)
- Usar `IEnumerable<T>` para colecciones solo lectura
- Usar `IList<T>` o `List<T>` cuando se necesita mutación

### Errores

- Usar try-catch con excepciones específicas
- Loggear errores con nivel apropiado (`ILogger`)
- Retornar `IResult` o `ActionResult` tipados desde endpoints API
- Nunca tragar excepciones silenciosamente

### Formato

- 4 espacios para indentación (sin tabs)
- Máx. 120 caracteres por línea
- Una expresión por línea en cadenas de métodos
- Llave de apertura en la misma línea

---

## Arquitectura del Proyecto

```
portfolio/
├── AGENTS.md                  ← Este archivo           
├── content/
│   ├── CV2026.md              ← CV actualizado a 2026
│   └── foto.jpeg              ← Fotografía 
├── src/
│   ├── Portfolio.Client/      ← Blazor WASM (frontend)
│   │   ├── Pages/
│   │   ├── Components/
│   │   ├── Models/
│   │   ├── Services/
│   │   └── wwwroot/
│   └── Portfolio.Api/         ← Opcional: backend ASP.NET Core
├── tests/
│   └── Portfolio.Tests/
└── .github/
    └── workflows/
        └── deploy.yml
```

---

## Instrucciones para el Agente

### Comportamiento General

- **Usa C# idiomático**: records, pattern matching, nullable reference types habilitados.
- **Sigue Clean Architecture** en la separación de responsabilidades.
- **No uses librerías de terceros** sin antes verificar si .NET o Blazor ya lo resuelven nativamente.
- **Cada componente Razor debe tener una responsabilidad única.**
- **Los datos del portfolio** se leen desde `wwwroot/data/portfolio.json` mediante `HttpClient`. No hardcodees contenido en los componentes.
- Si una tarea requiere backend, créalo en `Portfolio.Api` con endpoints REST mínimos y documentados con Swagger.

### Componentes Razor

- Mantener bloques `@code` bajo 30 líneas — extraer a clase parcial `.razor.cs`
- Usar `[Inject]` para inyección de servicios
- Usar `@key` en bucles `foreach` para diffing óptimo
- Preferir `<style scoped>` sobre CSS global

```csharp
// ✅ Correcto: code-behind pattern
public partial class ProjectsPage : ComponentBase
{
    [Inject] IPortfolioDataService DataService { get; set; } = default!;
    private IEnumerable<Project> _projects = [];

    protected override async Task OnInitializedAsync()
        => _projects = await DataService.GetProjectsAsync();
}
```

---

## Especificaciones de Cada Sección

### Home

- **Hero section**: Nombre, título profesional, descripción breve (máx. 2 líneas), botón CTA a proyectos.
- **Indicador de tecnologías clave**: Lista visual de .NET, C#, Azure, SQL Server, etc.
- **Animación de entrada**: El texto del hero aparece con efecto de typewriter usando JS interop o CSS.

### About

- Foto/avatar (placeholder SVG si no hay imagen real).
- Párrafo de presentación en primera persona.
- **Sección de Skills** agrupados por categoría: Backend, Frontend, Cloud, DevOps, Bases de datos.

### Projects

- Grid de tarjetas responsivo (CSS Grid, sin frameworks CSS externos).
- Cada tarjeta muestra: título, descripción, stack tecnológico, links a GitHub/demo.
- **Filtro por tecnología**: implementar con estado local en Blazor (`_selectedFilter`).

### Experience

- Timeline vertical con línea conectora.
- Cada ítem: empresa, cargo, fechas, descripción, logros (lista de bullets).
- Diseño alternado izquierda/derecha en pantallas grandes.

### Contact

- Formulario con campos: Nombre, Email, Mensaje.
- Validación con `DataAnnotations` y `EditForm` de Blazor.
- Links a LinkedIn, GitHub, correo electrónico.

---

## Guía de Estilos (Design System)

### Paleta de Colores

```css
:root {
  /* Tema oscuro (predeterminado) */
  --color-bg:         #0d1117;
  --color-surface:    #161b22;
  --color-border:     #30363d;
  --color-accent:     #7c3aed;   /* Violeta .NET */
  --color-accent-alt: #06b6d4;   /* Cian Azure */
  --color-text:       #e6edf3;
  --color-text-muted: #8b949e;

  /* Tema claro */
  --color-bg-light:      #ffffff;
  --color-surface-light: #f6f8fa;
  --color-text-light:    #1c2128;
}
```

### Tipografía

- **Display/Headings**: `'JetBrains Mono'` (monospaced, evoca código .NET)
- **Body**: `'Geist'` o `'IBM Plex Sans'`

### Espaciado

Usar escala de 4px: `4, 8, 12, 16, 24, 32, 48, 64, 96px`.

### Responsive Breakpoints

```css
/* sm:  >= 640px  */
/* md:  >= 768px  */
/* lg:  >= 1024px */
/* xl:  >= 1280px */
```

---

## Datos de Ejemplo (portfolio.json)

```json
{
  "personal": {
    "name": "Tu Nombre",
    "title": "Senior .NET Developer",
    "tagline": "Construyendo sistemas robustos con C#",
    "email": "tu@email.com",
    "github": "https://github.com/tuusuario",
    "linkedin": "https://linkedin.com/in/tuusuario",
    "location": "España / Remote"
  },
  "skills": [ ... ],
  "projects": [ ... ],
  "experience": [ ... ]
}
```

Poblar con al menos: **5 skills por categoría**, **4 proyectos** (2 featured), **3 experiencias laborales**.

---

## Reglas de Calidad de Código

### C# / Blazor

- Habilitar `<Nullable>enable</Nullable>` en todos los proyectos.
- Usar `required` en propiedades de modelos.
- Componentes Razor con lógica compleja: code-behind en `.razor.cs`.
- Evitar `@code` blocks de más de 30 líneas.

### CSS

- Usar **CSS custom properties** para colores y tamaños.
- CSS scoped para estilos de componentes.
- `app.css` solo para estilos globales y variables.

### Performance

- Usar `@key` en listas para optimizar diffing de Blazor.
- Imágenes en `.webp` con lazy loading.
- Minimizar JS interop — solo cuando sea imprescindible.

---

## CI/CD — GitHub Actions

```yaml
name: Deploy Portfolio
on:
  push:
    branches: [main]
jobs:
  build-and-deploy:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      - name: Setup .NET
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '8.x'
      - name: Publish Blazor WASM
        run: dotnet publish src/Portfolio.Client -c Release -o ./publish
      - name: Deploy to GitHub Pages
        uses: peaceiris/actions-gh-pages@v4
        with:
          github_token: ${{ secrets.GITHUB_TOKEN }}
          publish_dir: ./publish/wwwroot
```

---

## Dependencias

- .NET 8.0
- Blazor WebAssembly
- xUnit (testing)

---

## Notas Finales

- **Pregunta antes de asumir** el nombre, email y URLs reales. Usa placeholders `TODO:` si no se proporcionan.
- **No instales NuGet packages** sin justificación explícita.
- El objetivo: un portfolio que un reclutador técnico de empresas .NET vea y piense: *"Esta persona sabe lo que hace."*