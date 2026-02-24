using UnityEngine;

public class ImpulseArea : MonoBehaviour {

    [Header("Parameters")]

    [SerializeField] private float _velocityMin;

    [Header("Cache")]

    private Rigidbody2D _playerRb;
    private Collider2D _playerCol;
    private float _gravityScaleFull;

    private void Start() {
        _playerRb = FindAnyObjectByType<PlayerDash>().GetComponent<Rigidbody2D>();
        _playerCol = _playerRb.GetComponent<Collider2D>();
        _gravityScaleFull = _playerRb.gravityScale;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision == _playerCol) {
            _playerRb.gravityScale = 0f;
            if(_playerRb.linearVelocity.magnitude <= 0) _playerRb.linearVelocity = Vector3.up; // Doesn't consider possibility of bein vertically stuck, should read each collision direction maybe
            else if (_playerRb.linearVelocity.magnitude < _velocityMin) _playerRb.linearVelocity = _playerRb.linearVelocity.normalized * _velocityMin;
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
