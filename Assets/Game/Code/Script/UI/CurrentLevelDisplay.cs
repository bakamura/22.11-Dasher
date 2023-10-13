using System.Collections;
using TMPro;
using UnityEngine;

public class CurrentLevelDisplay : MonoBehaviour {

    [Header("Parameters")]

    [SerializeField] private CanvasGroup _currentLevelDisplay;
    private TextMeshProUGUI _displayText;
    [SerializeField] private float _displayFullDuration;
    [SerializeField] private float _displayFadeDuration;

    [Header("Cache")]

    private WaitForSeconds _displayFullWait;

    private void Awake() {
        _displayText = _currentLevelDisplay.GetComponent<TextMeshProUGUI>();
        _displayFullWait = new WaitForSeconds(_displayFullDuration);
    }

    private void Start() {
        LevelManager.instance.onLevelStart.AddListener(ShowLevelDisplay);
        ShowLevelDisplay();
    }

    private void ShowLevelDisplay() {
        StartCoroutine(ShowLevelDisplayRoutine());
    }

    private IEnumerator ShowLevelDisplayRoutine() {
        _displayText.text = SaveSystem.instance.progress.levelCurrent.ToString();
        _currentLevelDisplay.alpha = 1;

        yield return _displayFullWait;

        while (_currentLevelDisplay.alpha > 0) {
            _currentLevelDisplay.alpha -= Time.deltaTime / _displayFadeDuration;

            yield return null;
        }

    }

}
