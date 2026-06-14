# Wedding Planner API

## Etap I – Modele, DTO i podstawowe endpointy

### Opis projektu

Wedding Planner to aplikacja ASP.NET Core Web API wspierająca organizację wesela.

System umożliwia:

- zarządzanie weselami
- zarządzanie listą gości
- zarządzanie budżetem
- zarządzanie wydatkami

W Etapie I dane przechowywane są wyłącznie w pamięci aplikacji przy użyciu statycznej listy (`List<T>`).

---

## Technologie

- .NET 8
- ASP.NET Core Web API
- Swagger / OpenAPI
- C#

---

## Struktura projektu

```text
Controllers/
Models/
DTOs/
Data/
```

### Controllers

Obsługa żądań HTTP.

### Models

Modele domenowe:

- WeddingModel
- GuestModel
- BudgetModel
- ExpenseModel

### DTOs

Obiekty transferu danych:

- CreateWeddingDto
- CreateGuestDto
- CreateBudgetDto
- CreateExpenseDto

### Data

Klasa:

```text
WeddingData
```

przechowująca dane w pamięci aplikacji.

---

## Modele

### WeddingModel

```text
Id
BrideName
GroomName
Date
Venue
IsActive
Guests
Expenses
Budget
```

### GuestModel

```text
Id
FirstName
LastName
IsConfirmed
```

### ExpenseModel

```text
Id
Name
Cost
```

### BudgetModel

```text
TotalBudget
Spent
Remaining
```

---

## Endpointy

### Weddings

| Metoda | Endpoint |
|----------|----------|
| GET | /api/weddings |
| GET | /api/weddings/{id} |
| POST | /api/weddings |

### Guests

| Metoda | Endpoint |
|----------|----------|
| GET | /api/guests |
| GET | /api/guests/{id} |
| POST | /api/guests |

### Budget

| Metoda | Endpoint |
|----------|----------|
| GET | /api/budget/{weddingId} |
| POST | /api/budget |

### Services / Expenses

| Metoda | Endpoint |
|----------|----------|
| GET | /api/services |
| GET | /api/services/{id} |
| POST | /api/services |

---

## Uruchomienie projektu

```bash
git clone <repo-url>
cd WeddingPlannerApp
dotnet run
```

Swagger:

```text
https://localhost:xxxx/swagger
```

---

## Funkcjonalności Etapu I

- konfiguracja projektu Web API
- modele danych
- DTO
- dane przechowywane w List<T>
- endpointy GET
- endpointy POST
- testowanie w Swagger UI


## Etap II – Serwisy, Dependency Injection i pełny CRUD

### Opis

Etap II rozszerza funkcjonalność projektu o:

- warstwę serwisów
- Dependency Injection
- walidację danych
- reguły biznesowe
- obsługę błędów
- pełny CRUD

---

## Nowe elementy architektury

```text
Controllers/
Services/
DTOs/
Models/
Middleware/
Data/
```

---

## Warstwa serwisów

Każdy moduł posiada własny serwis odpowiedzialny za logikę biznesową.

### Przykładowe serwisy

```text
IWeddingService
WeddingService

IGuestService
GuestService

IBudgetService
BudgetService

IExpenseService
ExpenseService
```

---

## Dependency Injection

Serwisy rejestrowane są w Program.cs:

```csharp
builder.Services.AddScoped<IWeddingService, WeddingService>();
builder.Services.AddScoped<IGuestService, GuestService>();
builder.Services.AddScoped<IBudgetService, BudgetService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
```

---

## Walidacja

Do DTO dodano atrybuty walidacyjne:

```csharp
[Required]
[StringLength(100)]
[Range(1, 1000000)]
```

Przykładowe walidacje:

- wymagane pola
- długość tekstu
- dodatnie kwoty
- poprawne zakresy liczb

---

## Reguły biznesowe

Przykładowe reguły:

### Wedding

- data wesela nie może być z przeszłości

### Guest

- kontrola liczby gości

### Budget

- wydatki nie mogą przekroczyć budżetu

### Expense

- koszt musi być większy od 0

---

## Global Error Handling

Middleware przechwytuje wyjątki i zwraca odpowiednie kody HTTP.

| Wyjątek | HTTP |
|----------|----------|
| KeyNotFoundException | 404 |
| ApplicationException | 400 |
| Exception | 500 |

---

## Endpointy

### Weddings

| Metoda | Endpoint |
|----------|----------|
| GET | /api/weddings |
| GET | /api/weddings/{id} |
| POST | /api/weddings |
| PUT | /api/weddings/{id} |
| DELETE | /api/weddings/{id} |

### Guests

| Metoda | Endpoint |
|----------|----------|
| GET | /api/guests |
| GET | /api/guests/{id} |
| POST | /api/guests |
| PUT | /api/guests/{id} |
| DELETE | /api/guests/{id} |

### Budget

| Metoda | Endpoint |
|----------|----------|
| GET | /api/budget/{weddingId} |
| POST | /api/budget |
| PUT | /api/budget/{id} |
| DELETE | /api/budget/{id} |

### Services / Expenses

| Metoda | Endpoint |
|----------|----------|
| GET | /api/services |
| GET | /api/services/{id} |
| POST | /api/services |
| PUT | /api/services/{id} |
| DELETE | /api/services/{id} |

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

## Funkcjonalności Etapu II

- pełny CRUD
- serwisy
- Dependency Injection
- Middleware
- walidacja danych
- reguły biznesowe
- obsługa wyjątków