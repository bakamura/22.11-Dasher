using UnityEngine;
using UnityEngine.Events;

public class Goal : MonoBehaviour {

    public static UnityEvent onGoal = new UnityEvent();
    private Collider2D _col;

    private void Awake() {
        _col = GetComponent<Collider2D>();
        LevelManager.instance.onLevelStart.AddListener(Restart);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision == PlayerDash.instance.col) {
            onGoal?.Invoke();
            _col.enabled = false;
        }
    }

    private void Restart() {
        _col.enabled = true;
    }

}
