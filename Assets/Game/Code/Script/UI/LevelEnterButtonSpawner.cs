using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEnterButtonSpawner : MonoBehaviour {

    [Header("Parameters")]

    [SerializeField] private GameObject _levelEnterBtnPrefab;
    [SerializeField] private Transform _btnParent;

    private void Start() {
        InstantiateButons();
    }

    private void InstantiateButons() {
        GameObject btn;
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++) {
            btn = Instantiate(_levelEnterBtnPrefab, _btnParent);
            btn.GetComponent<Button>().onClick.AddListener(() => LevelManager.instance.GoToScene(i)); // Because it's not possible to change parameters in an already present action
            btn.GetComponentInChildren<TextMeshProUGUI>().text = i.ToString();
        }
    }

}
