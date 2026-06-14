# Wedding Planner API

## Opis projektu

Wedding Planner to aplikacja ASP.NET Core Web API wspierająca organizację wesela.

System umożliwia:
- zarządzanie weselami,
- zarządzanie listą gości,
- zarządzanie budżetem,
- zarządzanie wydatkami.

Projekt rozwijany był etapowo:
- **Etap I**: modele, DTO, podstawowe endpointy GET/POST, dane w pamięci (`List<T>`),
- **Etap II**: warstwa serwisów, Dependency Injection, walidacja, reguły biznesowe, pełny CRUD, middleware błędów,
- **Etap III**: persystencja danych w EF Core + SQLite, paginacja, Soft Delete, dokumentacja Swagger z XML.

---

## Funkcjonalności systemu

### Moduł wesel
- dodawanie nowego wesela,
- pobieranie listy wesel,
- pobieranie szczegółów wybranego wesela,
- aktualizacja danych wesela,
- usuwanie wesela z wykorzystaniem mechanizmu Soft Delete.

### Moduł listy gości
- dodawanie gości do wybranego wesela,
- pobieranie listy gości,
- pobieranie szczegółów pojedynczego gościa,
- aktualizacja danych gościa,
- usuwanie gościa,
- zabezpieczenie przed dodawaniem duplikatów gości.

### Moduł budżetu
- tworzenie budżetu dla wesela,
- pobieranie budżetu,
- aktualizacja budżetu,
- automatyczne wyliczanie wydanej i pozostałej kwoty.

### Moduł wydatków
- dodawanie wydatków,
- pobieranie listy wydatków,
- aktualizacja wydatków,
- usuwanie wydatków,
- kontrola przekroczenia budżetu.

---

## Technologie

- .NET 8
- ASP.NET Core Web API
- C#
- Entity Framework Core
- SQLite
- Swagger / OpenAPI
- LINQ
- Dependency Injection
- Middleware

---

## Struktura projektu

```text
Controllers/
DTOs/
Models/
Services/
Middleware/
Data/
Program.cs
appsettings.json
WeddingApp.csproj
```

---

## Modele

### WeddingModel
- `Id`
- `BrideName`
- `GroomName`
- `Date`
- `Venue`
- `IsActive`
- `IsDeleted`
- `Guests`
- `Expenses`
- `Budget`

### GuestModel
- `Id`
- `FirstName`
- `LastName`
- `IsConfirmed`
- `WeddingModelId`

### ExpenseModel
- `Id`
- `Name`
- `Cost`
- `WeddingModelId`

### BudgetModel
- `Id`
- `TotalBudget`
- `Spent`
- `Remaining`
- `WeddingModelId`

---

## DTOs

- `CreateWeddingDto`
- `UpdateWeddingDto`
- `CreateGuestDto`
- `UpdateGuestDto`
- `CreateBudgetDto`
- `UpdateBudgetDto`
- `CreateExpenseDto`
- `UpdateExpenseDto`

---

## Endpointy API

### Weddings

| Metoda | Endpoint |
|----------|----------|
| GET | /api/weddings?pageNumber=1&pageSize=5 |
| GET | /api/weddings/{id} |
| POST | /api/weddings |
| PUT | /api/weddings/{id} |
| DELETE | /api/weddings/{id} |

### Guests

| Metoda | Endpoint |
|----------|----------|
| GET | /api/guests/{id}/guests |
| GET | /api/guests/{id}/guests/{guestId} |
| POST | /api/guests/{id}/guests |
| PUT | /api/guests/{id}/guests/{guestId} |
| DELETE | /api/guests/{id}/guests/{guestId} |

### Budgets

| Metoda | Endpoint |
|----------|----------|
| POST | /api/budgets/{id}/budget |
| GET | /api/budgets/{id}/budget |
| PUT | /api/budgets/{id}/budget |

### Expenses

| Metoda | Endpoint |
|----------|----------|
| GET | /api/expenses/{id}/expenses |
| POST | /api/expenses/{id}/expenses |
| PUT | /api/expenses/{id}/expenses/{expenseId} |
| DELETE | /api/expenses/{id}/expenses/{expenseId} |

---

## Reguły biznesowe

### Wesele
- nie można utworzyć wesela z datą wcześniejszą niż dzień bieżący,
- nie można utworzyć duplikatu wesela (ta sama para + ten sam dzień),
- usunięcie wesela realizowane jest poprzez Soft Delete.

### Goście
- nie można dodać dwóch gości o tym samym imieniu i nazwisku do jednego wesela,
- nie można zaktualizować danych gościa tak, aby powstał duplikat.

### Budżet i wydatki
- nie można dodać wydatku przekraczającego budżet wesela,
- nie można zaktualizować wydatku tak, aby przekroczył budżet,
- po dodaniu, aktualizacji lub usunięciu wydatku budżet jest automatycznie przeliczany.

---

## Dodatkowe funkcjonalności

- obsługa wyjątków za pomocą własnego Middleware,
- dokumentacja API generowana przez Swagger,
- komentarze XML dla endpointów,
- paginacja wyników,
- walidacja danych wejściowych,
- Soft Delete.

---

## Testowanie

Projekt testowany przy użyciu Swagger UI.

Sprawdzono:
- GET
- POST
- PUT
- DELETE
- walidację DTO
- obsługę błędów
- reguły biznesowe

---

## Uruchomienie projektu

### Wymagania

- .NET 8 SDK
- JetBrains Rider lub Visual Studio Code

### Uruchomienie

1. Otwórz projekt w Riderze lub Visual Studio Code.
2. Przywróć pakiety NuGet:

```bash
dotnet restore
```

3. Uruchom aplikację:

```bash
dotnet run
```

4. Otwórz dokumentację Swagger:

```text
https://localhost:xxxx/swagger
```

lub adres wyświetlony w konsoli po uruchomieniu aplikacji.

---

## Dane

Aplikacja korzysta z bazy danych SQLite (`WeddingApp.db`), tworzonej automatycznie przy starcie aplikacji (`EnsureCreated`).

W projekcie może pojawić się także:
- `WeddingApp.db-wal`
- `WeddingApp.db-shm`

Są to standardowe pliki pomocnicze SQLite.

---

## Autor

Projekt wykonany w ramach przedmiotu Programowanie .NET.

