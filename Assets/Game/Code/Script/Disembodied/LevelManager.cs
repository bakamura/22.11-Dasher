using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    [Header("Parameters")]

    [HideInInspector] public UnityEvent onLevelEnter = new UnityEvent(); // Needed only if PlayerInitialPos setter will use it
    [HideInInspector] public UnityEvent onLevelStart = new UnityEvent();

    [Header("Cache")]

    private static int _currentSceneId = 1;
    private bool _waitForAd = false;
    private HUD _hud;

    private void Start() {
        _hud = FindObjectOfType<HUD>();

        IronSourceHandler.instance.SubscribeToIronSourceEvent(IronSourceHandler.IronSourceEvent.InterstitialAdShowSucceededEvent, AdOpened);
        IronSourceHandler.instance.SubscribeToIronSourceEvent(IronSourceHandler.IronSourceEvent.InterstitialAdClosedEvent, AdClosed);

        SceneManager.LoadScene(SaveSystem.instance.progress.levelCurrent, LoadSceneMode.Additive);
    }

    public void GoToScene(int sceneId) {
        StartCoroutine(GoToSceneRoutine(sceneId));
    }

    private IEnumerator GoToSceneRoutine(int sceneId) {
        if (sceneId == 0) sceneId = _currentSceneId + 1;

        _hud.PauseBtn(false);
        
        // Animation

        // After animation ended only

        IronSourceHandler.instance.InterstitialShow();

        AsyncOperation loadOperation = SceneManager.UnloadSceneAsync(_currentSceneId);

        yield return loadOperation;

        _currentSceneId = sceneId;
        loadOperation = SceneManager.LoadSceneAsync(_currentSceneId, LoadSceneMode.Additive);

        yield return loadOperation;

        while (_waitForAd) {
            yield return null;
        }

        // Animation

        // After animation ended only

        _hud.PauseBtn(true);

        onLevelEnter?.Invoke();
        onLevelStart?.Invoke();
    }

    public void RestartScene() {
        onLevelStart.Invoke();
        // Show Ad here ?
    }

    private void AdOpened(IronSourceAdInfo adInfo) {
        _waitForAd = true;
    }

    private void AdClosed(IronSourceAdInfo adInfo) {
        _waitForAd = false;
    }

}
