using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSkipOffer : MonoBehaviour {

    [Header("Parameters")]

    [SerializeField] private byte _skipTimeMin;
    [SerializeField] private byte _skipCheckRetryMin;

    [SerializeField] private GameObject _skipBtn;

    [Header("Cache")]

    private WaitForSeconds _skipShowWait;
    private WaitForSeconds _skipCheckRetryWait;

    private void Awake() {
        _skipShowWait = new WaitForSeconds(_skipTimeMin);
        _skipCheckRetryWait = new WaitForSeconds(_skipCheckRetryMin);
    }

    private void Start() {
        LevelManager.instance.onLevelEnter.AddListener(ResetSkipBtn);
        StartCoroutine(DelayFirstSkipShow());
    }

    public void SkipBtn() {
        IronSourceHandler.instance.SubscribeToIronSourceEvent(IronSourceHandler.IronSourceEvent.RewardedAdRewardedEvent, LevelSkiped);
    }

    private void LevelSkiped() {
        SaveSystem.instance.CompleteLevel(TimeSpan.Zero); //
        LevelManager.instance.SkipToNextLevel();
    }

    private void ResetSkipBtn() {
        StopAllCoroutines();
        if (SaveSystem.instance.progress.levelClearTime[SaveSystem.instance.progress.levelCurrent - 1] == TimeSpan.Zero && 
           SceneManager.GetSceneAt(1).buildIndex < SceneManager.sceneCountInBuildSettings - 1) StartCoroutine(ResetSkipBtnRoutine());
    }

    private IEnumerator ResetSkipBtnRoutine() {
        _skipBtn.SetActive(false);

        yield return _skipShowWait;

        while (IronSourceHandler.instance.RewardedCheck()) yield return _skipCheckRetryWait;
        _skipBtn.SetActive(true);
    }

    private IEnumerator DelayFirstSkipShow() {
        yield return null;

        ResetSkipBtn();
    }

}
