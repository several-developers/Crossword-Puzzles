#if UNITY_EDITOR
using Core.Crossword;
using UnityEditor;
using UnityEngine;

namespace Core.Editor
{
    public class GameDataViewer : EditorWindow
    {
        private const string GameMenuItem = "🕹 Crossword Puzzle/";
        private const string GameDataViewerMenuItem = GameMenuItem + "⚙ Game Data Viewer";
        private const string GameDataViewerMetaPath = "Assets/_Game/Meta/Game Data Viewer.asset";
        private const string SaveGameData = "Save Game Data";
        private const string LoadGameData = "Load Game Data";

        private static UnityEditor.Editor _gameDataViewerEditor;
        private static GameDataViewerMeta _gameDataViewerMeta;

        [MenuItem(GameDataViewerMenuItem)]
        public static void ShowWindow() => UpdateWindow();

        private static void UpdateWindow()
        {
            EditorWindow window = GetWindow(typeof(GameDataViewer));
            GUIContent titleContent = new GUIContent
            {
                text = "Game Data Viewer"
            };
            window.titleContent = titleContent;

            _gameDataViewerMeta = AssetDatabase.LoadAssetAtPath<GameDataViewerMeta>(GameDataViewerMetaPath);
            _gameDataViewerEditor = UnityEditor.Editor.CreateEditor(_gameDataViewerMeta);
        }

        private void OnGUI()
        {
            if (_gameDataViewerEditor == null)
                UpdateWindow();
            
            _gameDataViewerEditor.OnInspectorGUI();

            if (GUILayout.Button(SaveGameData))
                _gameDataViewerMeta.SaveGameData();

            if (GUILayout.Button(LoadGameData))
                _gameDataViewerMeta.LoadGameData();
        }
    }
}
#endif