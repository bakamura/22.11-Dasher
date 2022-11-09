using UnityEngine;
using UnityEngine.SceneManagement;

public class Hud : MonoBehaviour {

    [SerializeField] private GameObject _pauseMenu;

    public void PauseGame(bool b) {
        Time.timeScale = b ? 0 : 1;
        _pauseMenu.SetActive(b);
    }

    public void ReturnToMenu() {
        SceneManager.LoadScene(0);
    }

}
