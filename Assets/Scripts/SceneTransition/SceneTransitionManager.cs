using UnityEngine;

public static class SceneTransitionManager
{
    private static string transitionPrefabPath = "Prefabs/TransitionPrefab"; // The path to the prefab within the Resources folder
    private static GameObject transitionPrefab;

    static SceneTransitionManager()
    {
        // Load the transition prefab from the Resources folder
        transitionPrefab = Resources.Load<GameObject>(transitionPrefabPath);

        if (transitionPrefab == null)
        {
            Debug.LogError("SceneTransitionManager: Transition prefab is not found at the specified path.");
        }
    }

    // Call this function to instantiate the transition prefab
    public static void TriggerSceneTransition(string nextSceneName, float transitionDelay)
    {
        if (transitionPrefab == null)
        {
            Debug.LogError("SceneTransitionManager: Transition prefab is not assigned.");
            return;
        }

        // Set the next scene name and transition delay on the prefab
        SceneTransitionHandler handler = transitionPrefab.GetComponent<SceneTransitionHandler>();
        handler.nextSceneName = nextSceneName;
        handler.transitionDelay = transitionDelay;

        // Instantiate the prefab
        GameObject.Instantiate(transitionPrefab);
    }
}
