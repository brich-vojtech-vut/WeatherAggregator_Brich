Projekt č.2 
Zvolil jsem weather apku, která stahuje data ze 2 různůch Free API (OpenMeteo a WeatherAPI) 
Plus jsem přidal "DummyAPI" která je něco jako moje vlastní stanice, akorát jen generuje náhodnou teplotu od 5 do 25°C.
OpenMetro funguje Free bez jakékoliv registrace.
Ale do WeatherAPI jsem se registroval se školním mailem, kde po registraci dostanete Free klíč, který je třeba implementovat do kódu. (což jsem udělal)
U měst v Česku jsem neměl problém, ale města v cizině doporučuji zadávat v Angličtině (OpenMeteo zvládalo i češtinu, ale WeatherAPI moc ne).

Zdůvodnění architektury:
Namespaces:
Rozdělil jsem appku do tří logických částí (vrstev). Důvodem bylo hlavně to, abych oddělil data od logiky:  
Models: Tady jsou jen datové třídy, do kterých se ukládají data stažená z JSONu.
Providers: Tyto třídy se starají čistě jen o HTTP komunikaci a stahování dat.
Services: Tady je "mozek" aplikace (WeatherAggregationService). Ten jen dostane data a řeší výpočty (průměry, extrémy).

Interface a OOP:
Abych splnil principy OOP, vytvořil jsem interface IWeatherProvider. Všechna 3 API tento interface implementují. Dělal jsem to proto, abych mohl jednoduše přidat další zdroj počasí, aniž bych musel jakkoliv sahat do hlavní logiky programu.

Práce s výjimkami:
Rozhodl jsem se řešit výjimky primárně už na úrovni jednotlivých Providerů. Každé HTTP volání má svůj try-catch blok.  
Když má např. WeatherAPI výpadek nebo zadám špatný klíč, nechci, aby mi to shodilo celou aplikaci. Místo toho se jen vrátí objekt s informací IsSuccess = false a aplikace zkusí spočítat data alespoň ze zbylých dvou API.
V hlavním Program.cs pak mám jen jeden velký globální try-catch, který funguje jako poslední pojistka, aby konzole nespadla na nějakou úplně nečekanou chybu.

Asynchronní stahování:
Aplikace tahá data ze 3 míst. Aby uživatel nečekal, až se stáhne první, pak druhé a pak třetí, dal jsem všechny Tasky do listu a zavolal Task.WhenAll(). Tím se data stahují ze všech API současně (paralelně) a aplikace je mnohem rychlejší.

Konzolové UI mi přišlo nejčistčí a nejsnadnější k tomtuto typu aplikace.
