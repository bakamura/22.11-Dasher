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

    [SerializeField] private Button[] _levelSelectBtn;

    private void Start() {
        Goal.onGoal.AddListener(HidePauseBtn);
        FindObjectOfType<LevelManager>().onLevelStart.AddListener(ShowPauseBtn);
        LevelEnterBtnUpdate();
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

    public void LevelEnterBtnUpdate() {
        for (int i = 1; i < SaveSystem.instance.progress.levelCleared.Length; i++) _levelSelectBtn[i].interactable = SaveSystem.instance.progress.levelCleared[i];
    }
}
