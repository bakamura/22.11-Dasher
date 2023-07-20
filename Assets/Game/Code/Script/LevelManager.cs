using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    [Header("Parameters")]

    [HideInInspector] public UnityEvent onLevelStart;

    [Header("Cache")]

    private static int _currentSceneId = 1;

    public void GoToScene(int sceneId) {
        StartCoroutine(GoToSceneRoutine(sceneId));
    }

    public void RestartScene() {
        onLevelStart.Invoke();
    }

    private IEnumerator GoToSceneRoutine(int sceneId) {
        if (sceneId == 0) sceneId = _currentSceneId + 1;

        // Animation

        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(_currentSceneId);

        yield return unloadOperation;

        _currentSceneId = sceneId;
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(_currentSceneId, LoadSceneMode.Additive);

        yield return loadOperation;

        // Animation
    }

}
