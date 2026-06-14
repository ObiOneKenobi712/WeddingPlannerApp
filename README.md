# WeddingPlannerApp

## Opis projektu

WeddingPlannerApp to aplikacja internetowa wspomagająca organizację wesela. System umożliwia zarządzanie weselami, listą gości, budżetem oraz wydatkami związanymi z organizacją wydarzenia.

Projekt został wykonany w technologii **ASP.NET Core Web API** z wykorzystaniem języka **C#**, **Entity Framework Core** oraz bazy danych **SQLite**.

---

## Funkcjonalności systemu

### Moduł wesel

* dodawanie nowego wesela,
* pobieranie listy wesel,
* pobieranie szczegółów wybranego wesela,
* aktualizacja danych wesela,
* usuwanie wesela z wykorzystaniem mechanizmu Soft Delete.

### Moduł listy gości

* dodawanie gości do wybranego wesela,
* pobieranie listy gości,
* pobieranie szczegółów pojedynczego gościa,
* aktualizacja danych gościa,
* usuwanie gościa,
* zabezpieczenie przed dodawaniem duplikatów gości.

### Moduł budżetu

* tworzenie budżetu dla wesela,
* pobieranie budżetu,
* aktualizacja budżetu,
* automatyczne wyliczanie wydanej i pozostałej kwoty.

### Moduł wydatków

* dodawanie wydatków,
* pobieranie listy wydatków,
* aktualizacja wydatków,
* usuwanie wydatków,
* kontrola przekroczenia budżetu.

---

## Zastosowane technologie

* ASP.NET Core 8 Web API
* C#
* Entity Framework Core
* SQLite
* Swagger / OpenAPI
* Dependency Injection
* DTO (Data Transfer Objects)
* Middleware
* LINQ

---

## Reguły biznesowe

### Wesele

* nie można utworzyć wesela z datą wcześniejszą niż dzień bieżący,
* usunięcie wesela realizowane jest poprzez Soft Delete.

### Goście

* nie można dodać dwóch gości o tym samym imieniu i nazwisku do jednego wesela,
* nie można zaktualizować danych gościa tak, aby powstał duplikat.

### Budżet i wydatki

* nie można dodać wydatku przekraczającego budżet wesela,
* nie można zaktualizować wydatku tak, aby przekroczył budżet,
* po dodaniu, aktualizacji lub usunięciu wydatku budżet jest automatycznie przeliczany.

---

## Dodatkowe funkcjonalności

* obsługa wyjątków za pomocą własnego Middleware,
* dokumentacja API generowana przez Swagger,
* komentarze XML dla endpointów,
* paginacja wyników,
* walidacja danych wejściowych,
* Soft Delete.

---

## Uruchomienie projektu

### Wymagania

* .NET 8 SDK
* JetBrains Rider lub Visual Studio Code

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
WeddingApp.db
```

---

## Autor

Projekt wykonany w ramach przedmiotu Programowanie .NET.
