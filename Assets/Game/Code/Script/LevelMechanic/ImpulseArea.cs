using UnityEngine;

public class ImpulseArea : MonoBehaviour {

    [Header("Parameters")]

    [SerializeField] private float _velocityMin;

    [Header("Cache")]

    private Rigidbody2D _playerRb;
    private Collider2D _playerCol;
    private float _gravityScaleFull;

    private void Start() {
        _playerRb = FindObjectOfType<PlayerDash>().GetComponent<Rigidbody2D>();
        _playerCol = _playerRb.GetComponent<Collider2D>();
        _gravityScaleFull = _playerRb.gravityScale;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision == _playerCol) {
            _playerRb.gravityScale = 0f;
            if(_playerRb.velocity.magnitude <= 0) _playerRb.velocity = Vector3.up; // Doesn't consider possibility of bein vertically stuck, should read each collision direction maybe
            else if (_playerRb.velocity.magnitude < _velocityMin) _playerRb.velocity = _playerRb.velocity.normalized * _velocityMin;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision == _playerCol) {
            _playerRb.gravityScale = _gravityScaleFull;
        }
    }

    private void OnDestroy() {
        _playerRb.gravityScale = _gravityScaleFull;
    }

}
