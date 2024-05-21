# DP_document_management

Spuštění

Aplikace je spustitelná pomocí souboru DocumentManagementApp.exe ve složce Release\net8.0-windows. Pro spuštění aplikace je zapotřebí mít nainstalován Microsoft .Net Framework ve verzi v4.0 nebo vyšší.

Výběr složky pro analýzu

Po prvotním spuštění aplikace je uživatel vyzván k zadání absolutní cesty ke složce, kde se dokumenty nachází. Pokud složka neexistuje, nebo je cesta zadána špatně, nebude uživateli zpřístupněna aplikace do doby, dokud nezadá validní cestu. Příkladem cesty může být „C:\Users\user\data“. Po zadání validní cesty je uživatel přesměrován na úvodní okno aplikace.
Pokud uživatel již dříve zadal složku pro analýzu, bude při následujícím spuštění aplikace rovnou přesměrován na úvodní okno aplikace. Jestliže se uživatel rozhodně změnit složku s dokumenty pro analýzu, budou všechna jeho dříve uložená data smazána, tzn. pokud již měl uložené šablony a k nim přiřazené dokumenty, o všechny tyto informace přijde. Na dokumenty ve složce nemá aplikace žádný vliv a jejich obsah se nikterak nemění.
 
Analýza nových dokumentu

Pokud složka obsahuje dokumenty, které ještě nebyly zanalyzovány, může uživatel vidět počet těchto souborů v tlačítku v úvodním okně a přejít na analýzu. Uživatel může překliklávat mezi jednotlivými soubory a vybrat si tak, jaký bude chtít analyzovat. Ukončit analýzu a vrátit se na úvodní okno může buď dokončením analýzu všech nových souborů, nebo po kliknutí na tlačítko „Back“ v levém horním rohu.
Uživatel si vybere soubor, který bude chtít zanalyzovat. Při první analýze musí vytvořit šablonu a následně vytvořit atributy, které dokument obsahuje. U vytváření šablony je nutné zadat její název. Při vytváření atributu je nutné zadat název atributu a jeho typ (text, číslo, datum). Každému atributu uživatel musí přiřadit hodnotu – nelze ponechat atribut prázdný. Hodnotu atributu uživatel doplní tak, že klikne do textového pole daného atributu a následně vybere z náhledu dokumentu oblast, v níž se nachází daný atribut. Pokud bude vybranou oblastí jeden bod, hodnota atributu bude prázdná.
Pokud již byl nějaký dokument zanalyzován pro danou šablonu, po vybrání šablony se uživateli zobrazí v levé části předvyplněný formulář obsahující atributy, které byly zadány v již zanalyzovaných dokumentech pro danou šablonu. V náhledu dokumentu se mu také zobrazí čtverce, kde se atributy nachází. Po kliknutí na jednotlivé atributy se aktualizuje také náhled a zobrazí pouze daný atribut.
Jestliže uživatel přepíše hodnotu atributu ručně (bez výběru z dokumentu) a byla již dříve vybrána oblast obsahující tento atribut, oblast atributu se nezmění a čtverec označující atribut zůstane v náhledu stále zobrazen. Je to z důvodu, že by uživatel měl vkládat pouze atributy, které se nachází v samotném dokumentu. Lze však přidat nějaké doplňující atributy a to tak, že jejich hodnotu hned po vytvoření atributu zadá ručně a nevybere oblast z náhledu dokumentu. Pokud je již tento atribut předvyplněn z minulých analýz, může atribut smazat a vytvořit znovu.

Analýza starých dokumentu

Pokud byl již nějaký soubor zanalyzován, lze jej odstranit či upravit provedenou analýzu při kliknutí na tlačítko „Analyze old documents“ a zadáním absolutní cesty k dokumentu. Příkladem cesty k dokumentu může být „C:\Users\user\data\faktura.jpg“. Pokud byla cesta zadána správně a soubor existuje a byl již zanalyzován, zobrazí se stejné okno jako při analýze nového dokumentu, avšak formulář s atributy již obsahuje hodnoty z dříve provedené analýzy tohoto souboru. Práce s atributy je obdobná jako při analýze nového dokumentu.
Pro odstranění analýzy vybraného souboru stačí pouze kliknout na tlačítko „Delete“ v pravém horním rohu okna a potvrdit odstranění.

Filtrování

V pravém horním rohu úvodního okna se nachází tlačítko, kterým se otevře seznam filtrů. Filtry jsou defaultně nastaveny na „Item – rgx – prázdná hodnota“. Tento filtr zobrazí všechny šablony a již zanalyzované dokumenty.
Filtrovat lze dvěma způsoby:
•	„Item – rgx – hodnota“
o	filtrují se dokumenty i šablony podle hodnoty (regulární výraz)
•	„Amount – > – hodnota“, nebo „Amount – < – hodnota“
o	filtrují se pouze soubory, které mají číselnou hodnotu větší, nebo menší, než je zadaná hodnota (desetinná číslo musí být ve tvaru 0.0, tzn. odděleno tečkou)
Jiné kombinace filtrů nebudou brány v potaz a budou přeskočeny.
