using System.Collections;
using UnityEngine;

public class DashReseter : MonoBehaviour {

    [SerializeField] private float _respawnTime;
    private WaitForSeconds _respawnWait;
    private bool _collectable = true;

    private SpriteRenderer _sr;

    private void Start() {
        _sr = GetComponent<SpriteRenderer>();

        _respawnWait = new WaitForSeconds(_respawnTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (_collectable && collision.CompareTag("Player")) {
            Player.Instance.ResetDash();
            StartCoroutine(Respawn());
        }
    }

    private IEnumerator Respawn() {
        _collectable = false;
        _sr.enabled = false;

        yield return _respawnWait;

        _collectable = true;
        _sr.enabled = true;
    }

}
