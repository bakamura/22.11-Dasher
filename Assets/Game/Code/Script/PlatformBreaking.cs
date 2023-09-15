using System.Collections;
using UnityEngine;

public class PlatformBreaking : MonoBehaviour {

    [Header("Parameters")]

    [SerializeField] private float _respawnDelay;

    [Header("Cache")]

    private Collider2D _col;
    private WaitForSeconds _respawnWait;
    private static Collider2D _playerCol;

    private void Awake() {
        _col = GetComponent<Collider2D>();
        _respawnWait = new WaitForSeconds(_respawnDelay);
    }

    private void Start() {
        if(_playerCol == null) _playerCol = PlayerDash.instance.GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider == _playerCol) StartCoroutine(BreakRespawn());
    }

    private IEnumerator BreakRespawn() {

        // Start Animation
        yield return new WaitForSeconds(1.25f); // DEBUG
        GetComponent<SpriteRenderer>().enabled = false; // DEBUG
        // Wait Animation Duration

        _col.enabled = false;

        yield return _respawnWait;

        _col.enabled = true;

        // Respawn Animation
        GetComponent<SpriteRenderer>().enabled = true; // DEBUG
    }
}
