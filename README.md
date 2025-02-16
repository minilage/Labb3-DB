# QuizApp

En WPF-applikation i C# som låter användaren hantera och spela quiz. Applikationen använder MongoDB för att 
spara quiz-data, så att man kan skapa, läsa, uppdatera och ta bort quiz-paket (Question Packs) samt spela quiz med tidsgräns.

---

## Funktionalitet

- **Quiz-paket:**  
  - Skapa nya quiz-paket, redigera dem och ta bort dem.
  - Varje paket innehåller en uppsättning frågor med svarsalternativ, där varje fråga har ett 
	korrekt svar, flera felaktiga svar, en kategori, svårighetsgrad och en tidsgräns per fråga.

- **Spel:**  
  - Spelaren kan spela ett quiz där tidtagning per fråga hanteras.
  - Under spelets gång visas frågetid, frågetext och svarsalternativ.  
  - Efter quizet visas en resultatdialog med antalet rätta svar.

- **Fullscreen:**  
  - I spelvyn (PlayerView) kan du växla till fullscreen-läge vilket tar bort fönstrets titelrad 
	och anpassar layouten för en bättre spelupplevelse.
  - Fullscreen-funktionen är endast aktiv i spelvyn.

- **Kategori-hantering:**  
  - Varje quiz-paket kan tilldelas en kategori, vald från en dropdown-meny.
  - Kategorierna lagras i MongoDB och hämtas automatiskt när applikationen startar.

---

## Teknologier

- **C# / .NET (WPF):**  
  Applikationen är byggd med Windows Presentation Foundation (WPF) i C#.

- **MongoDB:**  
  MongoDB används som databas via MongoDB.Driver för att lagra quiz-data.

- **Asynkrona anrop:**  
  Alla databasanrop sker asynkront med async/await för att säkerställa att UI:t förblir responsivt.

- **MVVM:**  
  Applikationen följer MVVM-mönstret för att separera UI från logik.

---

## Installation och Körning

### Förutsättningar

- .NET SDK (t.ex. .NET 6)
- MongoDB installerat och igång på localhost (standardport)
- Visual Studio (eller annan IDE med stöd för WPF)

### Steg

1. **Klona repot:**

2.Öppna lösningen: Öppna projektet i Visual Studio.

3.Bygg projektet: Kompilera lösningen. Applikationen kommer att skapa databasen 
(med ditt för- och efternamn) om den inte redan finns.

4.Kör applikationen: Starta applikationen med F5. Databasen skapas automatiskt och 
eventuella demodata laddas in vid första körningen.


### Användargränssnitt
Navigering:
Använd menyn högst upp i applikationen för att:

- Skapa, redigera och ta bort quiz-paket.
- Växla mellan redigeringsvyn (ConfigurationView) och spelvyn (PlayerView).
- Aktivera fullscreen-läge i spelvyn (Fullscreen-knappen är endast aktiv i PlayerView).

Spel:
I spelvyn visas:

- En timer med återstående tid per fråga.
- Frågetext och svarsalternativ med visuell feedback (rätt eller fel).
- Efter avslutat quiz visas en resultatdialog med spelarens antal rätta svar.

Asynkrona Anrop
Alla databasanrop sker asynkront med hjälp av async/await, vilket garanterar att applikationen 
förblir responsiv även under långa operationer mot databasen.

Kända Begränsningar
- Fullscreen-läget är endast aktivt i spelvyn.
- MongoDB måste vara igång på localhost för att applikationen ska kunna ansluta och spara data.

- Kontakt
Om du har frågor eller behöver support, kontakta mig via e-post: tina.lagesson@gmail.com

Denna applikation är utvecklad som en nybörjarimplementering av en quiz-app med MongoDB och WPF. 
Den fokuserar på grundläggande CRUD-funktionalitet och en enkel men funktionell användarupplevelse.