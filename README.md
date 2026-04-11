# 🏎️ F1Zone – Formula–1 Statisztikai és Stratégiai Portál

## 📌 Projektmunka Tagok
- **Bártfai Levente**
- **Besze László**

---

## 🛠️ Felhasznált technológiák

A projekt modern Microsoft technológiai stackre épül, a reszponzív webdesign elveit követve.

- **Frontend & Backend:** C# .NET 8 (Blazor Web App)
- **Adatbázis:** Microsoft SQL Server
- **UI:** HTML5, CSS3
- **Verziókövetés:** Git & GitHub

---

## ⚙️ Főbb funkciók és modulok

A rendszer szerepkör-alapú jogosultságkezeléssel rendelkezik:

### 👤 Felhasználói funkciók
- 🔐 Biztonságos bejelentkezés (JWT token alapú azonosítás)
- 📰 Hírek olvasása
- ⭐ Versenyzők kedvencek közé helyezése
- 📊 Stratégia oldal használata

---

### 🛡️ Adminisztrátori funkciók
- 🏁 Főoldal módosítása:
  - "Driver of the Day"
  - "Legutóbbi futam"
- 👨‍✈️ Pilóták módosítása
- ➕ Új pilóta hozzáadása

---

## 💻 Futtatás helyi környezetben (Lokálisan)

A projekt futtatásához kövesd az alábbi lépéseket:

1. **Repository klónozása**
   ```bash
   git clone https://github.com/beszelaszlohihi/F1Zone
-A projekt megnyitása Visual Studio 2022 környezetben.
-Az adatbázis létrehozása a mellékelt SQL szkript (vagy Entity Framework Migrations) segítségével az SSMS-ben.
-Az appsettings.json fájlban a DefaultConnection adatbázis-kapcsolati karakterlánc (Connection String) beállítása a helyi SQL szerverhez.
-A projekt elindítása (F5 / IIS Express / Kestrel).
