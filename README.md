# BeautyNest

Projekt je razvijen za potrebe predmeta **RS1** i nije završen. Trenutna implementacija pokazuje osnovne vještine rukovanja sa **Repository pattern-om**, korištenje API-ja i pisanje backend-a. 

Aplikacija omogućava registraciju korisnika s različitim nivoima pristupa. Trenutno smo najviše fokusirani na funkcionalnosti klijenta i neprijavljenog korisnika. Među implementiranim funkcionalnostima su: uređivanje profila, dodavanje profilne slike, pregled i dodavanje omiljenih salona, odabir usluga te kreiranje rezervacija.

Backend sistema za rezervacije osmišljen je tako da optimalno rasporedi zaposlenike u okviru radnog vremena salona, čime se osigurava efikasno upravljanje vremenskim slotovima.

Projekat će se nastaviti razvijati s ciljem unaprjeđenja korisničkog iskustva i proširenja postojećih mogućnosti. Sljedeće funkcionalnosti koje planiramo implementirati uključuju: pregled radnog rasporeda zaposlenika, pregled rezervacija klijenata i upravljanje radnim rasporedom.


# Napomena

Prilikom testiranja registracije (potvrda naloga putem mejla), na backendu, u fajlu **appsettings.json**, zamijeniti "Username" i "Password" polja stvarnim podacima.

  "Email": {
   <br>  "SmtpServer": "smtp.office365.com",
   <br>  "Port": "587",
   <br>  "Username": "`placeholder@edu.fit.ba`",
   <br>  "Password": "`placeholder`",
   <br>  "From": "`placeholder@edu.fit.ba`"
  <br>}

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
**(Klijent)**
Korisničko ime: `fazlicamina02`  
Šifra: `Amina123!`

**(Uposlenik)**
Korisničko ime: `aldinh`  
Šifra: `Uposlenik123!`
