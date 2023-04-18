<div id="top"></div>

![PDFimg](https://user-images.githubusercontent.com/79923317/232738734-ece6a26d-cbda-490d-8d89-c1f6447a2c5a.png)

# Inventory System (EN)
Inventory System is a program written in C# that provides an API for managing inventory. The application uses a SQL database to store information about items, their groups, locations, manufacturers, and sellers. Application users can add, remove, modify, and browse information about items in the warehouse.

## Technologies and libraries used
The application was written in ASP.NET Core MVC and uses the following technologies and libraries:
* **.NET 7**: allows for creating cross-platform applications, including web and API interfaces.
* **Entity Framework Core**: an ORM (Object-Relational Mapping) framework used for database management.
* **AutoMapper**: library for automatic object mapping between the business layer and the presentation layer (using DTO - Data Transfer Objects).
* **Swagger**: a tool for generating documentation and easy API testing.
* **JSON Web Token (JWT)**: an open standard that provides secure information exchange, enabling user authentication.
* **FluentValidation**: a library used for validating data sent by the user.

## Project structure
The project consists of three main parts:
1. **Database**: contains the definition of the database model and migrations for its creation and update..
2. **InventorySystemWebApi**: contains the web application code (business logic), including controllers, services, object mapping, authentication and authorization configuration, and error handling (using Middleware).
3. **InventorySystemWebApi.Tests.Integration**: contains tests for the web application.

## Running the application
To run the application, you need to:
1. Clone the application repository from GitHub to your local environment using the 'git clone' command or download the source code in ZIP format and extract it on your computer.
2. Open the project in Visual Studio.
3. Select the 'InventorySystemWebApi' project as the startup project.
4. Run the application.

After starting the application, you can test its functionality using the Swagger interface available at https://localhost:7117/swagger/index.html and http://localhost:5054/swagger/index.html.

<br />

# Inventory System (PL)
Inventory System to program napisany w języku C#, który dostarcza interfejs programistyczny (API) do zarządzania inwentaryzacją. Aplikacja wykorzystuje bazę danych SQL, w której przechowywane są informacje o przedmiotach, ich grupach, lokalizacjach, producentach i sprzedawcach. Użytkownicy aplikacji mają możliwość dodawania, usuwania, modyfikowania oraz przeglądania informacji o przedmiotach w magazynie.

## Użyte technologie i biblioteki
Aplikacja została napisana w ASP.NET Core MVC i korzysta z następujących technologii i bibliotek:
* **.NET 7**: umożliwia tworzenie aplikacji wieloplatformowych, w tym internetowych i internetowych interfejsów API.
* **Entity Framework Core**: framework ORM (Object-Relational Mapping) służący do obsługi bazy danych.
* **AutoMapper**: biblioteka do automatycznego mapowania obiektów między warstwą biznesową a warstwą prezentacji (wykorzystując DTO - Data Transfer Objects).
* **Swagger**: narzędzie do generowania dokumentacji oraz łatwego testowania API.
* **JSON Web Token (JWT)**: otwarty standard, który zapewnia bezpieczną wymianę informacji, umożliwiając autentykację użytkowników.
* **FluentValidation**: biblioteka służąca do walidacji danych przesyłanych przez użytkownika.

## Struktura projektu
Projekt składa się z trzech głównych części:
1. **Database**: zawiera definicję modelu bazy danych oraz migracje do jej tworzenia i aktualizacji.
2. **InventorySystemWebApi**: zawiera kod aplikacji webowej (logikę biznesową), w tym kontrolery, serwisy, mapowanie obiektów, konfigurację uwierzytelniania i autoryzacji oraz obsługę błędów (korzystając z Middleware).
3. **InventorySystemWebApi.Tests.Integration**: zawiera testy aplikacji webowej.

## Uruchamianie aplikacji
Żeby uruchomić aplikację, należy:
1. Sklonować repozytorium aplikacji z GitHub na swoje lokalne środowisko za pomocą komendy 'git clone' lub pobrać kod źródłowy w formacie ZIP i wypakować go na swoim komputerze.
2. Otworzyć projekt w programie Visual Studio.
3. Wybrać projekt 'InventorySystemWebApi' jako projekt startowy.
4. Uruchomić aplikację.

Po uruchomieniu aplikacji można przetestować jej działanie za pomocą interfejsu Swaggera, dostępnego pod adresem https://localhost:7117/swagger/index.html oraz http://localhost:5054/swagger/index.html.

<p align="right">(<a href="#top">back to top</a>)</p>
