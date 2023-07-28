using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class SuccessDisplay : Singleton<SuccessDisplay> {

    [Header("Animation")]

    [SerializeField] private RectTransform _successDisplayRectTransform;
    [SerializeField] private float _successDisplayAnimationDuration;
    private Vector2 _successDisplayInitialPos;
    [SerializeField] private Vector2 _successDisplayFinalPos;

    [Header("Time")]

    [SerializeField] private TextMeshProUGUI _timeTakenText;
    [SerializeField] private string _textBeforeTime;
    private float _timeAtStart;

    private void Start() {
        PlayerDash.instance.onDash.AddListener(StartTimer);
        Goal.onGoal.AddListener(ShowDisplay);
        FindObjectOfType<LevelManager>().onLevelStart.AddListener(Restart);
    }

    private void ShowDisplay() {
        StartCoroutine(ShowDisplayRoutine());
    }

    private IEnumerator ShowDisplayRoutine() {
        TimeSpan span = TimeSpan.FromSeconds(Time.timeSinceLevelLoad - _timeAtStart);
        _timeTakenText.text = _textBeforeTime + span.ToString(@"m\:ss\.fff"); // Check what happens if more than 10mins

        _successDisplayRectTransform.anchoredPosition = _successDisplayInitialPos;

        float animation = 0;
        while (true) {
            yield return null;

            animation += Time.deltaTime / _successDisplayAnimationDuration;
            if (animation >= 1) {
                _successDisplayRectTransform.anchoredPosition = _successDisplayFinalPos;
                break;
            }

            _successDisplayRectTransform.anchoredPosition = Vector2.Lerp(_successDisplayInitialPos, _successDisplayFinalPos, LogarithmicLerp(animation));
        }
    }

    private float LogarithmicLerp(float value) {
        return 1f - Mathf.Pow(1 - value, 2f);
    }

    private void StartTimer(Vector2 v2) {
        _timeAtStart = Time.timeSinceLevelLoad;
        PlayerDash.instance.onDash.RemoveListener(StartTimer);
    }

    private void Restart() {
        PlayerDash.instance.onDash.AddListener(StartTimer);
    }

}
