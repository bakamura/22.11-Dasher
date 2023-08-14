using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEnterButtonSpawner : MonoBehaviour {

    [Header("Parameters")]

    [SerializeField] private GameObject _levelEnterBtnPrefab;
    [SerializeField] private Transform _btnParent;

    public Button[] InstantiateButons() {
        Button[] btn = new Button[SceneManager.sceneCountInBuildSettings - 1];
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings - 1; i++) {
            int sceneId = i + 1;
            btn[i] = Instantiate(_levelEnterBtnPrefab, _btnParent).GetComponent<Button>();
            btn[i].onClick.AddListener(() => LevelManager.instance.GoToScene(sceneId)); // Because it's not possible to change parameters in an already present action
            btn[i].GetComponentInChildren<TextMeshProUGUI>().text = (sceneId).ToString();
            if(i > 0) btn[i].interactable = SaveSystem.instance.progress.levelCleared[i - 1]; // 
        }

        return btn;
    }

}
