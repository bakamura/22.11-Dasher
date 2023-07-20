using UnityEngine;
using UnityEngine.UI;

public class HUD : Menu {

    [Header("Pause")]

    [SerializeField] private GameObject _pauseMenu;

    [Space(16)]

    [SerializeField] private Image _pauseBtnImage;
    [SerializeField] private Sprite[] _pauseSprite = new Sprite[2];

    private void Awake() {
        Goal.onGoal.AddListener(HidePauseBtn);
        GetComponent<LevelManager>().onLevelStart.AddListener(ShowPauseBtn);
    }

    public void PauseBtn() {
        bool b = Time.timeScale == 0;
        _pauseBtnImage.sprite = b ? _pauseSprite[1] : _pauseSprite[0];

        OpenMenu(b ? null : _pauseMenu);

        Time.timeScale = b ? 1f : 0f;
        // List every routine to pause
    }

    public void ShowPauseBtn() {
        _pauseBtnImage.gameObject.SetActive(true);
    }

    private void HidePauseBtn() {
        _pauseBtnImage.gameObject.SetActive(false);
    }

}
