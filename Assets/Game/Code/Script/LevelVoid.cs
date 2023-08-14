using UnityEngine;

public class LevelVoid : MonoBehaviour {

    private Collider2D _playerCol;

    private void Awake() {
        _playerCol = FindObjectOfType<PlayerDash>().GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision == _playerCol) LevelManager.instance.RestartBtn();
    }

}
