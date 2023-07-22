using System.Collections;
using UnityEngine;

public class DashResetter : MonoBehaviour {

    [Header("Parameters")]

    [SerializeField] private float _respawnDelay;

    [Header("Cache")]

    private WaitForSeconds _respawnWait;
    private Collider2D _col;

    private void Awake() {
        _col = GetComponent<Collider2D>();
    }

    private void Start() {
        _respawnWait = new WaitForSeconds(_respawnDelay);
        FindObjectOfType<LevelManager>().onLevelStart.AddListener(ForceRespawn);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision == PlayerDash.instance.col) {
            PlayerDash.instance.ResetDash();
            StartCoroutine(Respawn());
        }
    }

    private IEnumerator Respawn() {
        _col.enabled = false;
        // Animation 
        
        yield return _respawnWait;

        _col.enabled = true;
        // Animation
    }

    private void ForceRespawn() {
        StopAllCoroutines();
        _col.enabled = true;
        // Active Animation
    }

}
