#if UNITY_EDITOR
using Core.Crossword;
using Core.Events;
using UnityEditor;
using UnityEngine;

namespace Core.Editor
{
    public class ConfigsEditor : EditorWindow
    {
        private const string ConfigsEditorMetaPath = "Assets/_Game/Meta/Configs Editor.asset";
        private const string SaveConfigs = "Save Configs";
        private const string LoadConfigs = "Load Configs";
        private const string RecreateCrossword = "Re-create Crossword";

        private static UnityEditor.Editor _configsEditor;
        private static ConfigsEditorMeta _configsEditorMeta;
        private static bool _metaFound;

        public static void ShowWindow()
        {
            UpdateWindow();

            if (!_metaFound)
                return;

            _configsEditorMeta.LoadConfigs();
        }

        private static void UpdateWindow()
        {
            EditorWindow window = GetWindow(typeof(ConfigsEditor));
            GUIContent titleContent = new GUIContent
            {
                text = "Configs Editor"
            };
            window.titleContent = titleContent;

            _configsEditorMeta = AssetDatabase.LoadAssetAtPath<ConfigsEditorMeta>(ConfigsEditorMetaPath);
            _metaFound = false;

            if (_configsEditorMeta == null)
                return;

            _metaFound = true;
            _configsEditor = UnityEditor.Editor.CreateEditor(_configsEditorMeta);
        }

        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnScriptsReloaded()
        {
            if (!HasOpenInstances<ConfigsEditor>())
                return;
            
            UpdateWindow();
        }

        private void OnGUI()
        {
            if (!_metaFound)
            {
                EditorGUILayout.LabelField("Error while loading menu. Please reopen 'Configs Editor'.");
                return;
            }

            if (_configsEditor == null)
                UpdateWindow();

            _configsEditor.OnInspectorGUI();
            
            GUIStyle labelStyle = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleCenter
            };

            GUILayout.Space(5);
            GUILayout.Label("----- Config Buttons -----", labelStyle);

            if (GUILayout.Button(SaveConfigs))
                _configsEditorMeta.SaveConfigs();

            if (GUILayout.Button(LoadConfigs))
                _configsEditorMeta.LoadConfigs();
            
            GUILayout.Space(5);
            GUILayout.Label("----- Runtime Only -----", labelStyle);

            if (GUILayout.Button(RecreateCrossword))
                DebugEvents.SendCreateCrossword();
        }
    }
}
#endif