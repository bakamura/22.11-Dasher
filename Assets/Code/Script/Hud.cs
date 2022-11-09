using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hud : MonoBehaviour {

    public static Hud Instance;

    public GameObject pauseMenu;

    [SerializeField] private Image _musicBtn;
    [SerializeField] private Sprite _musicEnabledIcon;
    [SerializeField] private Sprite _musicDisabledIcon;
    [SerializeField] private Image _sfxBtn;
    [SerializeField] private Sprite _sfxEnabledIcon;
    [SerializeField] private Sprite _sfxDisabledIcon;

    [SerializeField] private RectTransform _successDisplay;
    [SerializeField] private float _displayAnimationDuration;
    [SerializeField] private TextMeshProUGUI _timeDisplay;

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) {
            Debug.Log("More than one player instance found!");
            Destroy(gameObject);
        }
    }

    private void Start() {
        // Read if music/sfx is enabled
    }

    public void PauseGame(bool b) {
        Time.timeScale = b ? 0 : 1;
        pauseMenu.SetActive(b);

        Player.Instance.active = !b;
    }

    public void EnableMusic() {


        if (_musicBtn.sprite == _musicEnabledIcon) _musicBtn.sprite = _musicDisabledIcon;
        else _musicBtn.sprite = _musicEnabledIcon;
    }

    public void EnableSfx() {


        if (_sfxBtn.sprite == _sfxEnabledIcon) _sfxBtn.sprite = _sfxDisabledIcon;
        else _sfxBtn.sprite = _sfxEnabledIcon;
    }

    public void ReturnToMenu() {
        SceneManager.LoadScene(0);
    }

    public IEnumerator ShowSuccessDisplay() {
        Player.Instance.active = false;

        TimeSpan ts = TimeSpan.FromSeconds(Player.Instance.elapsedTime);
        _timeDisplay.text = ts.Minutes + ":" + ts.Seconds + "." + ts.Milliseconds;
        // Save completion
        // Compare to best time, update best if needed

        float initialPos = _successDisplay.anchoredPosition.y;
        float currentPos = 0.00001f;
        Vector2 pos = Vector2.zero;
        while (currentPos < 1) {
            currentPos += Time.deltaTime;
            pos[1] = initialPos * -Mathf.Log10(currentPos) / 4;
            _successDisplay.anchoredPosition = pos.y < 0 ? pos : Vector2.zero;
            yield return null;
        }
    }

}
