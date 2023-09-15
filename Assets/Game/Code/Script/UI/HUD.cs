using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HUD : Menu {

    [Header("Parameters")]

    [SerializeField] private GameObject _hud;
    [SerializeField] private GameObject _successDisplay;

    [Header("Pause")]

    [HideInInspector] public UnityEvent<bool> onPause = new UnityEvent<bool>();

    [Header("Level Selector")]

    [HideInInspector] public Button[] _levelSelectBtn;

    private void Start() {
        Goal.onGoal.AddListener(HidePauseBtn);
        LevelManager.instance.onLevelStart.AddListener(ShowPauseBtn);
    }

    public void PauseBtn(bool notPausing) {
        Time.timeScale = notPausing ? 1f : 0f;
        onPause?.Invoke(notPausing);
    }

    public void ShowPauseBtn() {
        OpenMenu(_hud);
    }

    private void HidePauseBtn() {
        OpenMenu(_successDisplay);
    }

}
