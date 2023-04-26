#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Core.Editor
{
    public class MenuToolkit : MonoBehaviour
    {
        private const string GameMenuItem = "🕹 Crossword Puzzle/";
        private const string ScenesMenuItem = GameMenuItem + "💾 Scenes/";
        private const string ScenesPath = "Assets/_Game/Scenes/";

        private const string BootstrapSceneMenuItem = ScenesMenuItem + "🚀 Bootstrap";
        private const string GameSceneMenuItem = ScenesMenuItem + "⚔ Game";
        private const string RunGameMenuItem = GameMenuItem + "🎮 Run Game";

        private const string BootstrapScenePath = ScenesPath + "Bootstrap.unity";
        private const string GameScenePath = ScenesPath + "Game.unity";

        [MenuItem(BootstrapSceneMenuItem)]
        private static void LoadBootstrapScene() =>
            OpenScene(BootstrapScenePath);

        [MenuItem(GameSceneMenuItem)]
        private static void LoadGameScene() =>
            OpenScene(GameScenePath);

        [MenuItem(RunGameMenuItem)]
        public static void RunGame()
        {
            if (EditorApplication.isPlaying)
                return;

            LoadBootstrapScene();
            EditorApplication.isPlaying = true;
        }

        private static void OpenScene(string path)
        {
            bool canOpenScene = !Application.isPlaying &&
                                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

            if (!canOpenScene)
                return;

            EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
        }
    }
}
#endif