# BeautyNest

Projekt je razvijen za potrebe predmeta **RS1** i nije završen. Trenutna implementacija pokazuje osnovne funkcionalnosti kao što su rukovanje sa **Repository pattern-om**, korištenje API-ja i pisanje backend-a. Iako aplikacija trenutno ne sadrži formu za registraciju korisnika, korisnici se mogu registrovati putem API-ja. Na frontend-u, za funkcionalnosti koje još nisu implementirane, koriste se **placeholder-i**. Aplikacija trenutno nema mnogo funkcionalnosti, ali će biti nadograđena u budućim fazama razvoja, s ciljem implementacije svih predviđenih opcija za klijente, vlasnike i uposlenike salona ljepote.

# Getting Started

**Backend (ASP.NET Core):**

Pokrenuti BeautyNest.sln u Visual Studiu.

Pokrenuti sljedeće komande u Package Manager Console za migracije:
<br>`Update-Database -Context "ApplicationDbContext"`
<br>`Update-Database -Context "AuthDbContext"`

Pokrenuti aplikaciju pomoću IIS Express ili Kestrel.

**Frontend (Angular):**

Instalirati Node.js s nodejs.org.

Instalirati potrebne pakete:
<br>`npm install`  

Pokrenuti razvojni server:
<br>`ng serve`

Aplikacija će biti dostupna na http://localhost:4200.

**Pristupni podaci za default korisnika:**  
Korisničko ime: `fazlicamina02`  
Šifra: `Amina123!`
