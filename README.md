<!--# TestingApp
Prosta aplikacja w ASP.NET Core MVC która wyświetala dane pobrane z bazy -->

# Tworzenie aplikacji ASP.NET Core z bazą danych SQL
<br>

## Krok 1: Utwórz nowy projekt ASP.NET Core

Otwórz Visual Studio i utwórz nowy projekt ASP.NET Core z modelem aplikacji "Web Application".
<br><br>

## Krok 2: Zainstaluj niezbędne pakiety NuGet

Przejdź do konsoli NuGet Package Manager i zainstaluj następujące pakiety:

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
```
<br><br>
## Krok 3: Utwórz model danych

Utwórz nowy folder "Models" w swoim projekcie i dodaj klasę modelu danych, np. Person.cs:

```csharp

public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
```
<br><br>
## Krok 4: Utwórz kontekst bazy danych

Utwórz nowy folder "Data" i dodaj klasę kontekstu bazy danych, np. AppDbContext.cs:

```csharp

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Person> People { get; set; }
}
```
<br><br>
## Krok 5: Konfiguracja bazy danych

W pliku appsettings.json dodaj connection string do bazy danych Azure Data Studio:

```json

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=YourDatabaseName;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}
```
<br><br>
## Krok 6: Połączenie z bazą danych
Połącz się ze swoją bazą danych na przykład Azure Data Studio oraz dodaj dane to tabeli People

```sql
USE TestingAppDataBase;

INSERT INTO People (FirstName, LastName) VALUES ('John', 'Doe');
INSERT INTO People (FirstName, LastName) VALUES ('Jane', 'Smith');
INSERT INTO People (FirstName, LastName) VALUES ('Bob', 'Johnson');
-- Dodaj więcej wierszy według potrzeb
```
<br><br>
## Krok 7: Konfiguracja Startup.cs

W pliku Startup.cs skonfiguruj usługi i dodaj DbContext:

```csharp

public void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

    services.AddControllersWithViews();
}
```
<br><br>
## Krok 8: Migracje i aktualizacja bazy danych

Uruchom następujące polecenia w konsoli Package Manager:

```bash

add-migration Init
update-database
```
<br><br>
## Krok 9: Dodaj kontroler

Utwórz kontroler, np. PeopleController.cs, który będzie obsługiwał operacje CRUD dla osób:

```csharp

public class PeopleController : Controller
{
    private readonly AppDbContext _context;

    public PeopleController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var people = _context.People.ToList();
        return View(people);
    }
}
```
<br><br>
## Krok 10: Utwórz widok

Utwórz widok Index.cshtml w folderze Views/People i wyświetl dane w tabeli:

```html

@model List<Person>

<table class="table">
    <thead>
        <tr>
            <th>ID</th>
            <th>First Name</th>
            <th>Last Name</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var person in Model)
        {
            <tr>
                <td>@person.Id</td>
                <td>@person.FirstName</td>
                <td>@person.LastName</td>
            </tr>
        }
    </tbody>
</table>
```
<br><br>
## Krok 11: Uruchom aplikację

Uruchom aplikację i przejdź do strony /People w przeglądarce, aby zobaczyć dane pobrane z bazy danych i wyświetlone w tabeli.

<br>

<img src="https://raw.githubusercontent.com/iuno-san/Database-related-application/master/TestAppImg.png">
