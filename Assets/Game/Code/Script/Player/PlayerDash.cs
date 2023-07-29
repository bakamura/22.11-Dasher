using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDash : Singleton<PlayerDash> {

    [Header("Dash")]

    [SerializeField] private float _dashVelocity;
    private bool _dashAvailable;
    [HideInInspector] public UnityEvent<Vector2> onDash = new UnityEvent<Vector2>();
    [HideInInspector] public UnityEvent onDashReady = new UnityEvent();

    [Header("Check Ground")]

    [SerializeField] private Vector2 _groundCheckPos;
    [SerializeField] private Vector2 _groundCheckBox;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundCheckDelay;
    private bool _groundCheckAvailable = true;

    [Header("Restart")]

    [HideInInspector] public Vector3 initialPos;

    [Header("Cache")]

    private Rigidbody2D _rb;
    [HideInInspector] public Collider2D col;

    protected override void Awake() {
        base.Awake();

        _rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void Start() {
        FindObjectOfType<LevelManager>().onLevelStart.AddListener(GoToInitialPos);
    }

    private void Update() {
        if (CheckGround()) ResetDash();
    }

    public void Dash(Vector2 direction) {
        if (_dashAvailable) {
            _dashAvailable = false;
            StartCoroutine(GroundCheckDelay());

            direction = direction.normalized;
            _rb.velocity = direction * _dashVelocity;
            onDash.Invoke(direction);
        }
    }

    private bool CheckGround() {
        if (_groundCheckAvailable) return Physics2D.OverlapBox((Vector2)transform.position + _groundCheckPos, _groundCheckBox, 0, _groundLayer);
        return false;
    }

    private IEnumerator GroundCheckDelay() {
        _groundCheckAvailable = false;

        float t = 0;
        while (t <= _groundCheckDelay) {
            if (!Physics2D.OverlapBox((Vector2)transform.position + _groundCheckPos, _groundCheckBox, 0, _groundLayer)) break;

            t += Time.deltaTime;
            yield return null;
        }

        _groundCheckAvailable = true;
    }

    public void ResetDash() {
        if (!_dashAvailable) onDashReady?.Invoke();
        _dashAvailable = true;
    }

    private void GoToInitialPos() {
        transform.position = initialPos;
        _rb.velocity = Vector2.zero;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        Gizmos.color = CheckGround() ? Color.green : Color.red;
        Gizmos.DrawWireCube(transform.position + (Vector3)_groundCheckPos, _groundCheckBox);
    }
#endif

}
