# Baldurs Gate 3 Loca Helper

Einfaches Werkzeug, um die für die Lokalisierung benötigten Dateien
schnell und einfach zu bearbeiten.

Die Bearbeitung der XML-Dateien erfolgt im Localization-Ordner des jeweiligen Moduls unter Angabe eines Verweises auf die Original-XML-Datei.
Diese dient zur Kontrolle der bereits übersetzten Texte.

Die Arbeitsgrundlage ist ein Ordner - Workspace genannt - in dem eine Sammlung verschiedener Mods bearbeitet werden kann.

## Screenshot

Hier ein parr Screenshot, um die Arbeitsweise des Programmes zu dokumentieren.
![screen1.png](images/screen1.png)

![screen2.png](images/screen2.png)

Beim ersten Start der App oder im Menü Punkt "Settings" **MUSS** dieser eingestellt werden: 
![workspace_folder.png](images/workspace_folder.png)

Die Struktur des Ordner **MUSS** folgenden Aufbau haben:

```
<ModRootFolder>
    <ModFolder>
        <Work>
            <Default Mod Structure>
```

Hier ein Beispiel meines Wokspace Ordners:

![workspace_structure.png](images/workspace_structure.png)

