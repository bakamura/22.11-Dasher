using UnityEngine;

public class ImpulseArea : MonoBehaviour {

    [Header("Cache")]

    private Rigidbody2D _playerRb;
    private Collider2D _playerCol;
    private float _gravityScaleFull;

    private void Start() {
        _playerRb = FindObjectOfType<PlayerDash>().GetComponent<Rigidbody2D>();
        _playerCol = _playerRb.GetComponent<Collider2D>();
        _gravityScaleFull = _playerRb.gravityScale;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision == _playerCol) {
            _playerRb.gravityScale = 0f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision == _playerCol) {
            _playerRb.gravityScale = _gravityScaleFull;
        }
    }

}
