# APBD_proj_1
# University Equipment Rental

## Opis projektu
Aplikacja konsolowa w C# do obsługi uczelnianej wypożyczalni sprzętu.  
System umożliwia dodawanie użytkowników i sprzętu, wypożyczanie, zwroty, sprawdzanie dostępności oraz generowanie raportu.

## Struktura projektu
- `Models` – klasy domenowe
- `Services` – logika biznesowa
- `Common.cs` – reguły, generator ID, wyjątek, kontekst danych, kalkulator kar
- `Program.cs` – scenariusz demonstracyjny

## Decyzje projektowe
Zastosowano abstrakcyjne klasy `Equipment` i `User`, ponieważ typy sprzętu i użytkowników mają część wspólną oraz część specyficzną.  
Logika biznesowa została przeniesiona do klas serwisowych, a `Program.cs` odpowiada tylko za uruchomienie scenariusza.  
Limity wypożyczeń i kary znajdują się w `RentalPolicy`, a identyfikatory są generowane przez `IdGenerator`.

## Kohezja, coupling i odpowiedzialności klas
- `UserService` zarządza użytkownikami
- `EquipmentService` zarządza sprzętem
- `RentalService` obsługuje wypożyczenia i zwroty
- `ReportService` generuje raport

Podział ten ogranicza sprzężenie i porządkuje odpowiedzialności klas.

## SOLID
- **SRP** – każda klasa ma jedną główną odpowiedzialność
- **OCP** – można dodać nowy typ sprzętu bez przebudowy całego systemu
- **LSP** – klasy pochodne są używane przez klasy bazowe
- **ISP** – zastosowano mały interfejs `IPenaltyCalculator`
- **DIP** – `RentalService` korzysta z abstrakcji kalkulatora kar

## Reguły biznesowe
- student: maksymalnie 2 aktywne wypożyczenia
- pracownik: maksymalnie 5 aktywnych wypożyczeń
- sprzętu niedostępnego nie można wypożyczyć
- opóźniony zwrot powoduje naliczenie kary 10 PLN za dzień

## Uruchomienie
Uruchomić projekt w IntelliJ Rider jako aplikację konsolową.
