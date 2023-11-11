[color=#ff7700][size=5][b]QOL Localization Editor for Larian Games (currently Baldurs Gate 3 only)[/b][/size][/color]

Tool for localizing BG3 mods, easy and fast editing of localization XML files.

[b][size=5]The following features are currently available in the app:[/size][/b]
[list]
[size=3][b][/b][/size][*]Importing a mod pak file and converting it to a new translations mod
[*]Multi mod editing
[*]Editing localization with easy clipboard feature
[*]Quicksearch display in table and text fields
[*]Comparison of texts with the current and a previous original localization file
[*]Incorporating the changes from the original localization files
[*]Automatic saving of the translation file - and ONLY this(!) - into loca format
[*]Saving the translation mod in zip format (Nexusmod upload compatible)
[*]Localization (Currently: German, English, Chinese)
[/list]

[img]https://staticdelivery.nexusmods.com/mods/3474/images/3136/3136-1699722297-1655180582.png[/img]

Source-Code available at GitHub: [url=https://github.com/Tenvan/Bg3LocalHElper]Bg3LocalHelper
[/url]Releases can also downloaded here: [url=https://github.com/Tenvan/Bg3LocalHElper/releases]Bg3LocaHelper Releases[/url]

[color=#ff0000][size=5]Attention![/size][/color]
[size=3]From version 3.2, the application requires one off the follwowing Dotnet Runtimes:
- net 7.0
- net 6.0
- Framework 4.8.1
- Framework 4.7.2
which can be downloaded directly from Microsoft, here net 7.0: [url=https://dotnet.microsoft.com/en-us/download/dotnet/7.0]Dotnet 7.0 Downloads[/url][/size][size=3]
[/size]

NET7.0 is the main Version, but in optional Files you can find versions for the other runtimes.


[b][size=3]Short instructions:[/size][/b]
At 'Original Reference' select the original - mostly English - original file of the mod.
In 'Source XML File' the target file is selected in the translation mod.
Both files are then loaded for processing using 'Load Source XML'.

The text to be translated is automatically copied to the clipboard as soon as you navigate in the grid.
The translated text can simply be copied using 'Paste from Clipboard'. Manual marking and copying is not necessary.

With 'Save Source XML' the translation is then saved at the end - or in between.
When you save the Xml, it write automatical the loca-File in the same directory.
No more need for extra use of the ConverterApp.

[color=#00ff00][size=3][b]The loca-Code part was imported from Norbyte excelent LSLib[/b][/size][/color]:
[url=https://github.com/Norbyte/lslib]https://github.com/Norbyte/lslib[/url]
thx

[b][size=4]See my other Mods for translation samples:[/size]
[url=https://www.nexusmods.com/baldursgate3/mods/3298]5e Spells GER[/url]
[url=https://www.nexusmods.com/baldursgate3/mods/3395]Appearance Edit Enhanced GER[/url]
[url=https://www.nexusmods.com/baldursgate3/mods/3103]Artificer GER[/url]
[url=https://www.nexusmods.com/baldursgate3/mods/3402]Auto Loot Seller V3 GER[/url]
[url=https://www.nexusmods.com/baldursgate3/mods/3384]Bard Subclass - College of Glamour GER[/url]
[url=https://www.nexusmods.com/baldursgate3/mods/3305]Demon Hunter Class GER[/url]
[url=https://www.nexusmods.com/baldursgate3/mods/3897]Enemies Enhanced GER[/url]
[url=https://www.nexusmods.com/baldursgate3/mods/3178]Infernal Armor Nightmare GER[/url]
[url=https://www.nexusmods.com/baldursgate3/mods/3226]Infinity Weapons Melee GER[/url]
[url=https://www.nexusmods.com/baldursgate3/mods/3212]Infinity Weapons Ranged GER[/url]
[url=https://www.nexusmods.com/baldursgate3/mods/3276]Secret Scrolls 5e Spells GER[/url]
[url=https://www.nexusmods.com/baldursgate3/mods/3279]Secret Scrolls GER[/url]
[url=https://www.nexusmods.com/baldursgate3/mods/3228]Sussur Weapons Spawn GER[/url]
[url=https://www.nexusmods.com/baldursgate3/mods/3383]Thor - God of Thunder GER[/url]
[url=https://www.nexusmods.com/baldursgate3/mods/3385]Transmog Enhanced GER[/url]
[url=https://www.nexusmods.com/baldursgate3/mods/3347]Warlock Undead GER[/url]
[url=https://www.nexusmods.com/baldursgate3/mods/3318]WoWDeathKnightClassGER[/url]
[/b]

Sourcecodes for translated mods can you find here: [url=https://github.com/Tenvan/Bg3Mods]Bg3Mods[/url]
