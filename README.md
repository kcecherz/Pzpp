# Pzpp
Temat projektu - Gra w stylu: "Jaka to melodia?"

1. Wymagania funkcjonalne:
   a) Zarządzanie biblioteką muzyczną
       - Aplikacja skanuje folder w poszukiwaniu plików MP3.
   b) Mechanika quizu
       - Losowanie pytania, odtworzenie utworu, weryfikacja odpowiedzi.
   c) System punktacji
       - Przyznawanie punktów na podstawie odpowiedzi (czas im szybciej lepiej), dodatkowe punkty za streak
   d)Panel wyników
       - Wyświetlanie podsumowania po zakończeniu rundy, zapisowanie Highscores Personal Best itp.
2. Wymagania niefuncjonalne:
   a) Responsywność interfejsu
   b) Obsługa błędów
     - odrzucanie uszkodzenych plików
   c) Modularność
     - Logika gry oddzielona od warstwy fizycznej (np. według wzoru MVVM)
     - Intuicyjne sterowanie (np. sterowanie klawiszami itp.)
