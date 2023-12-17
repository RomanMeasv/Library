# NextIT Library Assignment - Roman Masár

Táto aplikácia má slúžiť ako prototyp pre firmu NextIT na posúdenie mojich programovacích znalostí a schopností. Kód obsahuje niekolko osobných komentárov rozdelených podla významu (napr. NOTICE má slúžiť ako komentár ktorým som chcel poukázať na nasledujúci kód)

## Frontend

Na frontend som použil Angular.
Štruktúru som sa snažil rozdeliť podľa školských a ostatných projektoch na ktorých som pracoval. Na styling som použil Tailwind.

- Dátum vypožičania je limitovaný na prítomnosť
- Webová aplikácia obsahuje formulár ktorý slúži na vytváranie, upravovanie a mazanie knihy
- Kniha sa dá taktiež vypožičať cez formulár (vyplnením "lending" časti)
- Všetky uložené knihy sú zobrazené pod formulárom

## Backend

Na backend som použil C# s .NET. Vytvoril som Rest API ktoré slúži na základné CRUD operácie na objekte knihy. Štruktúru som zvolil clean (onion) keďže tú nás učili v škole a mám v nej najviac skúseností. Na vytvorenie som použil niekolko packageov (napr. AutoMapper, Moq, XUnit, ...). Celé som to programoval cez Visual Studio 2022.

- API obsahuje jeden aktívny controller ktorý zachytáva requesty na adrese /book
- BaseController je abstrakt od ktorého by som odvíjal ďalšie
- Testy sa nachádzajú v zložke Tests
- Knihy sú uložené v XML súbore (Infrastructure/Library.xml)
- Na prácu s XML súborom som použil XmlDocument
- Aplikácia sa dá ďalej rozvíjať (napr. keby je potrebné prejsť z XML súboru na databázu tak stačí keď sa vytvorí nová classa ktorá implementuje IBookRepository)
- Logging je implementovaný ako service ktorý sa aktivuje každých 30s

### Chyby ktoré by som opravil

- Keď sa nevyplní celá lending časť tak sa kniha vytvorí ale nie správne
- Na ukladanie objektu knihy by som použil serializáciu
- Update knihy cez frontend nie je naimplementovaný
- Knihy nie sú roztriedené podľa ich statusu (vypožičané/volné)
- Logging systém vypisuje že ukladá ale v skutočnosti nezapisuje nikam
- "Token" ktorý sa vymieňa by nemal byť v url parametroch
- Keďže som nepoužil serializáciu tak v BookRepository sa nachádza kopec !
- Frontend nemá naimplementovaný login formulár ani autentikáciu i keď backend je z väčšej časti na to pripravený
