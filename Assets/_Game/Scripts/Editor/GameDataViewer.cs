#if UNITY_EDITOR
using Core.Crossword;
using Core.Events;
using UnityEditor;
using UnityEngine;

namespace Core.Editor
{
    public class GameDataViewer : EditorWindow
    {
        private const string GameDataViewerMetaPath = "Assets/_Game/Meta/Game Data Viewer.asset";
        private const string SaveGameData = "Save Game Data";
        private const string LoadGameData = "Load Game Data";
        private const string CreateCrossword = "Create Crossword";

        private static UnityEditor.Editor _gameDataViewerEditor;
        private static GameDataViewerMeta _gameDataViewerMeta;
        private static bool _metaFound;

        public static void ShowWindow()
        {
            UpdateWindow();

            if (!_metaFound)
                return;

            _gameDataViewerMeta.LoadGameData();
        }

        private static void UpdateWindow()
        {
            EditorWindow window = GetWindow(typeof(GameDataViewer));
            GUIContent titleContent = new GUIContent
            {
                text = "Game Data Viewer"
            };
            window.titleContent = titleContent;

            _gameDataViewerMeta = AssetDatabase.LoadAssetAtPath<GameDataViewerMeta>(GameDataViewerMetaPath);
            _metaFound = false;

            if (_gameDataViewerMeta == null)
                return;

            _metaFound = true;
            _gameDataViewerEditor = UnityEditor.Editor.CreateEditor(_gameDataViewerMeta);
        }

        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnScriptsReloaded()
        {
            if (!HasOpenInstances<GameDataViewer>())
                return;
            
            UpdateWindow();
        }

        private void OnGUI()
        {
            if (!_metaFound)
            {
                EditorGUILayout.LabelField("Error while loading menu. Please reopen 'Game Data Viewer'.");
                return;
            }

            if (_gameDataViewerEditor == null)
                UpdateWindow();

            _gameDataViewerEditor.OnInspectorGUI();
            
            GUIStyle labelStyle = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleCenter
            };

            GUILayout.Space(5);
            GUILayout.Label("----- Debug Buttons -----", labelStyle);

            if (GUILayout.Button(SaveGameData))
                _gameDataViewerMeta.SaveGameData();

            if (GUILayout.Button(LoadGameData))
                _gameDataViewerMeta.LoadGameData();
            
            GUILayout.Space(5);
            GUILayout.Label("----- Runtime Only -----", labelStyle);

            if (GUILayout.Button(CreateCrossword))
                DebugEvents.SendCreateCrossword();
        }
    }
}
#endif