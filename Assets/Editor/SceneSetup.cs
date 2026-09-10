using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using Garganta.Core;
using Garganta.Flow;
using Garganta.Grid;
using Garganta.Combat;
using Garganta.Story;
using Garganta.UI;

namespace Garganta.Editor
{
    public static class SceneSetup
    {
        [MenuItem("Garganta/Setup TestBattle Scene")]
        public static void Setup()
        {
            var scene = EditorSceneManager.GetActiveScene();

            Camera cam = Object.FindAnyObjectByType<Camera>();
            if (cam == null)
            {
                var camGo = new GameObject("Main Camera");
                cam = camGo.AddComponent<Camera>();
            }
            cam.orthographic = true;
            cam.orthographicSize = 7f;
            cam.backgroundColor = new Color(0.10f, 0.10f, 0.18f); // Deep Black (ART_BRIEF)
            cam.transform.position = new Vector3(6, 4, -10);
            if (cam.GetComponent<IsometricCamera>() == null) cam.gameObject.AddComponent<IsometricCamera>();
            if (cam.GetComponent<CameraShake>() == null) cam.gameObject.AddComponent<CameraShake>();

            Ensure("Battle", typeof(GameManager), typeof(GridManager), typeof(GridVisualizer),
                typeof(TurnManager), typeof(CombatManager), typeof(ObjectPool));
            Ensure("UI", typeof(UIManager), typeof(TurnOrderUI), typeof(UnitInfoUI),
                typeof(ActionMenuUI), typeof(CombatUI), typeof(GameOverUI));
            Ensure("Flow", typeof(GameFlow), typeof(TitleUI), typeof(WorldMapUI),
                typeof(BaseUI), typeof(DialogueUI), typeof(TutorialManager), typeof(EndingsUI));

            EditorSceneManager.MarkSceneDirty(scene);
            Debug.Log("Garganta: TestBattle setup done. Save to Assets/Scenes/TestBattle.unity then Press Play.");
        }

        // Headless: Unity -batchmode -executeMethod Garganta.Editor.SceneSetup.SetupAndSave -quit
        public static void SetupAndSave()
        {
            Setup();
            var scene = EditorSceneManager.GetActiveScene();
            bool ok = EditorSceneManager.SaveScene(scene, "Assets/Scenes/TestBattle.unity");
            Debug.Log("Garganta: TestBattle saved to Assets/Scenes/TestBattle.unity: " + ok);
        }

        static void Ensure(string name, params System.Type[] comps)
        {
            var go = GameObject.Find(name) ?? new GameObject(name);
            foreach (var c in comps)
                if (go.GetComponent(c) == null) go.AddComponent(c);
        }
    }
}
