# Web API Framework dla Soneta enova365

Framework do integracji API z systemem Soneta enova365.

## Wymagania uruchomieniowe
- Plik `appsettings.json` w katalogu uruchomieniowym pliku `.exe`.
- Opcjonalny folder `dll` na dodatkowe biblioteki enova.
- Plik `Lista baz danych.xml` w `%appdata%\Soneta`.
- Uruchamiaj tylko plik `.exe` (nie z IDE). Debuguj poprzez Podłącz do procesu.

## Funkcjonalności
- **Autoryzacja**: JWT lub brak; dotyczy wszystkich endpointów.
- **Sesja enova**: Dostępna w logice kontrolerów API.

## Uruchomienie
Przykładowa aplikacja w folderze `WebApplication1`. Zwróć uwagę na typ projektu w `.csproj`.

W pliku startowym:
```csharp
EnovaBootLoader.Init();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEnovaApi(builder.Configuration);

var app = builder.Build();

app.UseEnovaApi();
```

## Token generator
Aby go użyć przekopiowujemy plik .exe w to samo miejsce gdzie najduje się baza danych (to samo miejsce skąd jest odpalany plik .exe)
