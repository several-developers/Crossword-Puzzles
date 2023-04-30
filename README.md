## Project tips
- It is recommended to start the game from the "**Bootstrap**" scene (or just click on the top menu "**Crossword Puzzle -> Run Game**"").
- The game works correctly when launched from any scene.
- Some Zenject installers are in the "**ProjectContext**" prefab (in the "**Resources**").
- The crossword can be edited at runtime using the "**Configs Editor**" or with a "**CrosswordConfig.json**" file.
After editing the crossword, you need to click "**Save Configs**" in the "**Configs Editor**" and then "**Re-create Crossword**".
- **Grid Helper** just a visualization for a crossword matrix.

## How to edit crossword
### Method one - from the custom editor.
1. Open tab "**Crossword Puzzle**".
2. Click "**Configs Editor**".
3. Click "**Load Configs**".
4. Here in "**Crossword Config -> Words Data**" you can create or edit words for the crossword.
5. To save the changes, click "**Save Configs**".

![configseditor](https://user-images.githubusercontent.com/66551502/235356281-06dad06f-0c10-4344-a8e3-559a5775b379.jpg)

---

### Method two - from the json file.
1. Open "**CrosswordConfig.json**" in the "**Resources/Configs**" folder.
2. Add your own words there:
```
"wordsData": [
  // Words
]
```

Example:
```
"wordsData": [
  {
    "answer": "Gamedev",
    "direction": "across",
    "column": 1,
    "row": 0
  }
]
```

---

### Rules
- **asnwer** - the answer itself (max 10 letters);
- **direction** - can only be "**across**" or "**down**";
- **column** - column position (0-9);
- **row** - row position (0-9);
