# 🎵 PZPP

## 📌 Temat projektu
Gra w stylu: **„Jaka to melodia?”**

---

## 1. Wymagania funkcjonalne

### a) Zarządzanie biblioteką muzyczną
- Aplikacja skanuje wybrany folder w poszukiwaniu plików `.mp3`.
- Automatyczne dodawanie poprawnych plików do biblioteki.
- Pomijanie plików nieobsługiwanych lub uszkodzonych.

### b) Mechanika quizu
- Losowanie pytania.
- Odtworzenie fragmentu utworu.
- Wprowadzanie odpowiedzi przez użytkownika.
- Weryfikacja poprawności odpowiedzi.

### c) System punktacji
- Przyznawanie punktów na podstawie poprawności odpowiedzi.
- Dodatkowe punkty za szybką odpowiedź (im szybciej, tym więcej punktów).
- Bonusowe punkty za serię poprawnych odpowiedzi (*streak*).

### d) Panel wyników
- Wyświetlanie podsumowania po zakończeniu rundy.
- Zapisywanie najlepszych wyników (*High Scores*).
- Obsługa rekordu osobistego (*Personal Best*).

---

## 2. Wymagania niefunkcjonalne

### a) Responsywność interfejsu
- Płynne działanie aplikacji bez zauważalnych opóźnień.

### b) Obsługa błędów
- Odrzucanie uszkodzonych plików muzycznych.
- Informowanie użytkownika o błędach.

### c) Modularność
- Oddzielenie logiki gry od warstwy prezentacji (np. zgodnie ze wzorcem **MVVM**).
- Intuicyjne sterowanie (np. obsługa klawiatury).