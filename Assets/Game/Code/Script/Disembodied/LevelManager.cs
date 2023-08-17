using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager> {

    [Header("Parameters")]

    [HideInInspector] public UnityEvent onLevelEnter = new UnityEvent(); // Needed only if PlayerInitialPos setter will use it
    [HideInInspector] public UnityEvent onLevelStart = new UnityEvent();
    [SerializeField] private int _retryUntilAd;
    private int _retryAmount = 0;

    [Header("Cache")]

    private bool _waitForAd = false;
    private HUD _hud;
    private LevelTransitionAnimation _transitionAnimationHandler;
    private WaitForSecondsRealtime _transitionStartWait;
    private WaitForSecondsRealtime _transitionEndWait;

    private void Start() {
        _hud = FindObjectOfType<HUD>();
        _transitionAnimationHandler = FindObjectOfType<LevelTransitionAnimation>();
        _transitionStartWait = new WaitForSecondsRealtime(_transitionAnimationHandler.GetAnimationDuration(LevelTransitionAnimation.TRANSITION_START));
        _transitionEndWait = new WaitForSecondsRealtime(_transitionAnimationHandler.GetAnimationDuration(LevelTransitionAnimation.TRANSITION_END));

        IronSourceHandler.instance.SubscribeToIronSourceEvent(IronSourceHandler.IronSourceEvent.InterstitialAdShowSucceededEvent, AdOpened);
        IronSourceHandler.instance.SubscribeToIronSourceEvent(IronSourceHandler.IronSourceEvent.InterstitialAdClosedEvent, AdClosed);

        SceneManager.LoadScene(SaveSystem.instance.progress.levelCurrent, LoadSceneMode.Additive);
    }

    public void GoToScene(int sceneId) {
        if (sceneId != SceneManager.GetSceneAt(1).buildIndex) StartCoroutine(GoToSceneRoutine((sceneId == 0 && SaveSystem.instance.progress.levelCurrent == SceneManager.sceneCountInBuildSettings - 1) ? 1 : sceneId));
    }

    private IEnumerator GoToSceneRoutine(int sceneId) {
        _hud.PauseBtn(false);
        _transitionAnimationHandler.TransitionStartAnimation();

        yield return _transitionStartWait;

        IronSourceHandler.instance.InterstitialShow();
        AsyncOperation loadOperation = SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1));

        yield return loadOperation;

        print(SaveSystem.instance.progress.levelCurrent); // 9
        loadOperation = SceneManager.LoadSceneAsync(sceneId == 0 ? SaveSystem.instance.progress.levelCurrent : sceneId, LoadSceneMode.Additive);
        SaveSystem.instance.SaveUpdate(SaveSystem.SaveType.Progress);

        yield return loadOperation;
        SaveSystem.instance.progress.levelCurrent = SceneManager.GetSceneAt(1).buildIndex;
        print(SaveSystem.instance.progress.levelCurrent); // 1

        while (_waitForAd) { yield return null; }

        _transitionAnimationHandler.TransitionEndAnimation();

        yield return _transitionEndWait;

        onLevelEnter?.Invoke();
        _hud.PauseBtn(true);
        onLevelStart.Invoke();
    }

    public void RestartBtn() {
        StartCoroutine(RetryRoutine());
    }

    private IEnumerator RetryRoutine() {
        _transitionAnimationHandler.TransitionStartAnimation();

        yield return _transitionStartWait;

        SaveSystem.instance.progress.levelCurrent = SceneManager.GetSceneAt(1).buildIndex;
        SaveSystem.instance.SaveUpdate(SaveSystem.SaveType.Progress); // In order to save the player wants to continue on this level

        _retryAmount++;
        if (_retryAmount >= _retryUntilAd) {
            IronSourceHandler.instance.InterstitialShow();
        }
        while (_waitForAd) { yield return null; }

        onLevelStart.Invoke();

        _transitionAnimationHandler.TransitionEndAnimation();

        yield return _transitionEndWait;

        _hud.PauseBtn(true);
    }

    private void AdOpened(IronSourceAdInfo adInfo) {
        _waitForAd = true;
        _retryAmount = 0;
    }

    private void AdClosed(IronSourceAdInfo adInfo) {
        _waitForAd = false;
    }

}
