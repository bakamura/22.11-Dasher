using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEnterButtonSpawner : MonoBehaviour {

    [Header("Parameters")]

    [SerializeField] private GameObject _levelEnterBtnPrefab;
    [SerializeField] private RectTransform _btnParent;

    private void Start() {
        FindObjectOfType<HUD>()._levelSelectBtn = InstantiateButtons();
        Destroy(this);
    }

    private Button[] InstantiateButtons() {
        Button[] btn = new Button[SceneManager.sceneCountInBuildSettings - 1];
        GridLayoutGroup gridLayout = _btnParent.GetComponent<GridLayoutGroup>();
        Vector2 sizeParent = _btnParent.sizeDelta;
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings - 1; i++) {
            int sceneId = i + 1;
            btn[i] = Instantiate(_levelEnterBtnPrefab, _btnParent).GetComponent<Button>();
            btn[i].onClick.AddListener(() => LevelManager.instance.GoToScene(sceneId)); // Because it's not possible to change parameters in an already present action
            btn[i].onClick.AddListener(() => SfxHandler.instance.UiUpSfx()); // Also
            btn[i].GetComponentInChildren<TextMeshProUGUI>().text = (sceneId).ToString();
            if (i > 0) btn[i].interactable = SaveSystem.instance.progress.levelCleared[i - 1]; // 

            else sizeParent[1] = 0;
            if (i % 3 == 0) sizeParent[1] += gridLayout.cellSize.y + gridLayout.spacing.y;
        }

        _btnParent.sizeDelta = sizeParent;

        return btn;
    }

}
