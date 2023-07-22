using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HUD : Menu {

    [Header("Pause")]

    [SerializeField] private GameObject _pauseMenu;
    [HideInInspector] public UnityEvent<bool> onPause = new UnityEvent<bool>();

    [Space(16)]

    [SerializeField] private Image _pauseBtnImage;
    [SerializeField] private Sprite[] _pauseSprite = new Sprite[2];

    //private GameObject[] _pauseBehaviours;

    private void Start() {
        Goal.onGoal.AddListener(HidePauseBtn);
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        //levelManager.onLevelEnter.AddListener(PauseGameObjectsUpdate);
        levelManager.onLevelStart.AddListener(ShowPauseBtn);
    }

    public void PauseBtn() {
        bool b = Time.timeScale == 0;
        _pauseBtnImage.sprite = b ? _pauseSprite[1] : _pauseSprite[0];

        OpenMenu(b ? null : _pauseMenu);

        Time.timeScale = b ? 1f : 0f;
        onPause?.Invoke(b);
        //foreach(GameObject gObject in _pauseBehaviours) gObject.SetActive(b);
    }

    public void ShowPauseBtn() {
        _pauseBtnImage.gameObject.SetActive(true);
    }

    private void HidePauseBtn() {
        _pauseBtnImage.gameObject.SetActive(false);
    }

    //private void PauseGameObjectsUpdate() {
    //    // Get somehow every object that can be paused
    //}

}
