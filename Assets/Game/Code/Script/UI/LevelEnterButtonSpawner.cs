using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEnterButtonSpawner : MonoBehaviour {

    [Header("Parameters")]

    [SerializeField] private GameObject _levelEnterBtnPrefab;
    [SerializeField] private RectTransform _btnParent;
    [SerializeField] private float[] _textSizePerDigit = new float[3];

    private void Start() {
        FindAnyObjectByType<HUD>().levelSelectBtn = InstantiateButtons();
        Destroy(this);
    }

    private Button[] InstantiateButtons() {
        Button[] btn = new Button[SceneManager.sceneCountInBuildSettings - 1];
        GridLayoutGroup gridLayout = _btnParent.GetComponent<GridLayoutGroup>();
        Vector2 sizeParent = _btnParent.sizeDelta;
        TextMeshProUGUI tmpUgui;
        int btnPerRow = Mathf.FloorToInt(GetComponent<CanvasScaler>().referenceResolution.x / gridLayout.cellSize.x); // Needs to take in spacing from gridLayout
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings - 1; i++) {
            int sceneId = i + 1;
            btn[i] = Instantiate(_levelEnterBtnPrefab, _btnParent).GetComponent<Button>();
            btn[i].onClick.AddListener(() => LevelManager.instance.GoToScene(sceneId)); // Because it's not possible to change parameters in an already present action
            btn[i].onClick.AddListener(() => SfxHandler.instance.UiUpSfx()); // Also
            tmpUgui = btn[i].GetComponentInChildren<TextMeshProUGUI>();
            tmpUgui.text = (sceneId).ToString();
            tmpUgui.fontSize = _textSizePerDigit[Mathf.FloorToInt(Mathf.Log10(sceneId))];
            if (i > 0) btn[i].interactable = SaveSystem.instance.progress.levelCleared[i - 1]; // 

            else sizeParent[1] = 0;
            if (i % btnPerRow == 0) sizeParent[1] += gridLayout.cellSize.y + gridLayout.spacing.y;
        }

        _btnParent.sizeDelta = sizeParent;

        return btn;
    }

}
