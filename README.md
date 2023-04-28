## How to edit crossword
### Method one - from the custom editor.
1. Open tab **Crossword Puzzle**.
2. Click **Game Data Viewer**.
3. Click **Load Game Data**.
4. Here in **Game Data -> Crossword Data -> Words Data** you can create or edit words for the crossword.

![SW9aBoDE](https://user-images.githubusercontent.com/66551502/235088509-02a76ebb-7f4b-47cb-87f9-bf36d574e6f1.jpg)

---

### Method two - from the json file.
1. Open **GameConfig.json** in the **Resources** folder.
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
