using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager> {

    [Header("Parameters")]

    [HideInInspector] public UnityEvent onLevelEnter = new UnityEvent(); // Needed only if PlayerInitialPos setter will use it
    [HideInInspector] public UnityEvent onLevelStart = new UnityEvent();

    [Header("Cache")]

    private bool _waitForAd = false;
    private HUD _hud;
    private LevelTransitionAnimation _transitionAnimationHandler;
    private WaitForSeconds _transitionStartWait;
    private WaitForSeconds _transitionEndWait;

    private void Start() {
        _hud = FindObjectOfType<HUD>();
        _transitionAnimationHandler = FindObjectOfType<LevelTransitionAnimation>();
        _transitionStartWait = new WaitForSeconds(_transitionAnimationHandler.GetAnimationDuration(LevelTransitionAnimation.TRANSITION_START));
        _transitionEndWait = new WaitForSeconds(_transitionAnimationHandler.GetAnimationDuration(LevelTransitionAnimation.TRANSITION_END));

        IronSourceHandler.instance.SubscribeToIronSourceEvent(IronSourceHandler.IronSourceEvent.InterstitialAdShowSucceededEvent, AdOpened);
        IronSourceHandler.instance.SubscribeToIronSourceEvent(IronSourceHandler.IronSourceEvent.InterstitialAdClosedEvent, AdClosed);

        SceneManager.LoadScene(SaveSystem.instance.progress.levelCurrent, LoadSceneMode.Additive);
    }

    public void GoToScene(int sceneId) {
        StartCoroutine(GoToSceneRoutine(sceneId));
    }

    private IEnumerator GoToSceneRoutine(int sceneId) {
        if (sceneId == 0) sceneId = SaveSystem.instance.progress.levelCurrent + 1;

        _hud.PauseBtn(false);

        _transitionAnimationHandler.TransitionStartAnimation();

        yield return _transitionStartWait;

        IronSourceHandler.instance.InterstitialShow();

        AsyncOperation loadOperation = SceneManager.UnloadSceneAsync(SaveSystem.instance.progress.levelCurrent);

        yield return loadOperation;

        SaveSystem.instance.progress.levelCurrent = sceneId;
        loadOperation = SceneManager.LoadSceneAsync(SaveSystem.instance.progress.levelCurrent, LoadSceneMode.Additive);

        yield return loadOperation;

        while (_waitForAd) {
            yield return null;
        }

        _transitionAnimationHandler.TransitionEndAnimation();

        yield return _transitionEndWait;


        onLevelEnter?.Invoke();
        RestartBtn();
    }

    public void RestartBtn() {
        _hud.PauseBtn(true);
        onLevelStart.Invoke();
        // Show Ad here ? Probably not, frustration on retry-players builds faster and lead to disengagement
    }

    private void AdOpened(IronSourceAdInfo adInfo) {
        _waitForAd = true;
    }

    private void AdClosed(IronSourceAdInfo adInfo) {
        _waitForAd = false;
    }

}
