using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeScoreDisplay : MonoBehaviour {

    [Header("Parameters")]

    [SerializeField] private GameObject _scoreTextPrefab;
    private TextMeshProUGUI[] _scoreText;
    [SerializeField] private RectTransform _scoreTextParent;

    [Header("Cache")]

    private TimeSpan maxTSPan = TimeSpan.FromMinutes(10) - TimeSpan.FromMilliseconds(1);
    
    private void Start() {
        _scoreText = new TextMeshProUGUI[SaveSystem.instance.progress.levelClearTime.Length];
        TimeSpan tSpan;

        GridLayoutGroup gridLayout = _scoreTextParent.GetComponent<GridLayoutGroup>();
        Vector2 sizeParent = _scoreTextParent.sizeDelta;

        for (int i = 0; i < _scoreText.Length; i++) {
            tSpan = SaveSystem.instance.progress.levelClearTime[i] < TimeSpan.FromMinutes(10) ? SaveSystem.instance.progress.levelClearTime[i] : maxTSPan;

            _scoreText[i] = Instantiate(_scoreTextPrefab, _scoreTextParent).GetComponent<TextMeshProUGUI>();
            _scoreText[i].text = $"{i + 1} || {(SaveSystem.instance.progress.levelClearTime[i] != TimeSpan.Zero ? tSpan.ToString(@"m\:ss\.fff") : "UNCLEAR")}";

            sizeParent[1] += gridLayout.cellSize.y + gridLayout.spacing.y;
        }

        _scoreTextParent.sizeDelta = sizeParent;
    }

    public void ScoreTextUpdate(int level, TimeSpan newTime) {
        newTime = newTime < TimeSpan.FromMinutes(10) ? newTime : maxTSPan;
        _scoreText[level].text = $"{level + 1} || {newTime.ToString(@"m\:ss\.fff")}";
    }

}
