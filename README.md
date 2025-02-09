### Dokumentacja API

#### 1. **Opis**
Mikroserwis generuje dozwolone akcje dla karty użytkownika na podstawie identyfikatora użytkownika i numeru karty. API zwraca listę dozwolonych akcji, które mogą być wykonane na danej karcie, zależnie od przypisanych uprawnień.

#### 2. **Endpointy**

##### a. **GET /cards/permissions/{userId}/{cardNumber}**

Zwraca dozwolone akcje dla karty użytkownika.

###### **Parametry zapytania:**
- **userId** (required): Identyfikator użytkownika.
- **cardNumber** (required): Numer karty, dla której mają zostać wygenerowane dozwolone akcje.

###### **Przykład zapytania:**
```http
GET /cards/permissions/User1/Card1
```

###### **Przykładowa odpowiedź (200 OK):**
```json
{
  "actions": [
    "ACTION1",
    "ACTION2"
  ]
}
```

###### **Przykładowa odpowiedź (404 Not Found):**
```json
{
  "message": "Card Card1 for user User1 not found."
}
```

#### 3. **Konfiguracja uprawnień karty `CardPermissions.json`**

W pliku `CardPermissions.json` zdefiniowane są uprawnienia dla różnego rodzaju typów kart. Konfiguracja może być rozszeżana o kolejne uprawenia i/lub mogą być modyfikowane istniejące w zależności od zmieniających się wymagań.

##### **Przykład pliku `CardPermissions.json`:**
```json
{
  "CardPermissions": [
    {
      "Name": "ACTION1",
      "CardTypes": ["Prepaid", "Debit", "Credit"],
      "CardStatuses": ["Active"]
    },
    {
      "Name": "ACTION2",
      "CardTypes": ["Prepaid", "Debit", "Credit"],
      "CardStatuses": ["Inactive"],
	  "IsPinSet": false
    }
  ]
}
```

##### **Opis struktury pliku `CardPermissions.json`:**

- **CardPermissions:** Jest to główny element zawierający listę wszystkich dostępnych uprawnień.
- **Name:** Nazwa akcji, która reprezentuje konkretne uprawnienie, np. "ACTION1". To będzie nazwa akcji, którą system będzie sprawdzał, aby zdecydować, czy użytkownik może wykonać daną akcję na karcie. 
- **CardTypes:** Lista typów kart, do których przypisane jest dane uprawnienie. Typy kart to `Prepaid`, `Debit` i `Credit`, które są używane w systemie. Akcja będzie dostępna tylko dla kart, które pasują do jednego z tych typów.
- **CardStatuses:** Lista statusów kart, do których przypisane jest dane uprawnienie. Statusy kart obejmują różne stany karty, takie jak `Ordered`, `Inactive`, `Active`, `Restricted`, `Blocked`, `Expired`, `Closed`. Uprawnienie będzie dostępne tylko dla kart, które mają jeden z tych statusów.
- **IsPinSet:** Określa, czy karta wymaga ustawionego PIN-u dla danego uprawnienia. Jest to opcjonalny parametr, który może być użyty do ograniczenia dostępnych akcji na kartach, które mają ustawiony lub nie ustawiony PIN.
  - **Wartość `true`:** Oznacza, że akcja jest dostępna tylko dla kart, które mają ustawiony PIN.
  - **Wartość `false`:** Oznacza, że akcja jest dostępna tylko dla kart, które **nie** mają ustawionego PIN-u.
  - **Wartość `null`:** Oznacza, że ustawienie PIN-u nie ma wpływu na dostępność tej akcji.

---

### Podsumowanie

Dokumentacja zawiera szczegóły dotyczące endpointów API, parametrów, a także przykładów odpowiedzi, które aplikacja faktycznie może zwrócić. Rozszerzanie pliku `CardPermissions.json` jest opisane w sposób, który umożliwia łatwe dodawanie nowych typów kart, akcji i uprawnień.