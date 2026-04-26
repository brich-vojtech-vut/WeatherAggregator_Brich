Projekt č.2 
Zvolil jsem weather apku, která stahuje data ze 2 různůch Free API (OpenMeteo a WeatherAPI) 
Plus jsem přidal "DummyAPI" která je něco jako moje vlastní stanice, akorát jen generuje náhodnou teplotu od 5 do 25°C.
OpenMetro funguje Free bez jakékoliv registrace.
Ale do WeatherAPI jsem se registroval se školním mailem, kde po registraci dostanete Free klíč, který je třeba implementovat do kódu. (což jsem udělal)
Máme tedy celkem 3 hodnoty teploty, které kód porovnává, tvoří průměr a Nejvyšší/Nejnižší teplotu.
Po spuštění se kód zeptá na zadání názvu města.
U měst v Česku jsem neměl problém, ale města v cizině doporučuji zadávat v Angličtině (OpenMeteo zvládalo i češtinu, ale WeatherAPI moc ne)
Pokud někde nastane chyba mělo by to být ošetřeno. (A to Chybovými stavy a Vyjímkami)
Ostatní podmínky jsou snad taky všechny splněny. Jako třeba získávání dat z API asynchronně.
UI jsem nechal v Konzoli, protože mi to pro výpis pár řádků přišlo dostačující.  
