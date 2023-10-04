using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlatformBreaking : MonoBehaviour {
    
    [HideInInspector] public static UnityEvent onBreakingStart = new UnityEvent();
    [HideInInspector] public static UnityEvent onBreaking = new UnityEvent();

    [Header("Parameters")]

    [SerializeField] private float _respawnDelay;

    [Header("Cache")]

    private PlatformBreakingAnimation _animationHandler;
    private Collider2D _col;
    private WaitForSeconds _breakWait;
    private WaitForSeconds _respawnWait;
    private static Collider2D _playerCol;
    private bool _breaking = false;
    private static Rigidbody2D _playerRb; //

    private void Awake() {
        _animationHandler = GetComponent<PlatformBreakingAnimation>();
        _col = GetComponent<Collider2D>();

        _respawnWait = new WaitForSeconds(_respawnDelay);
    }

    private void Start() {
        if (_playerCol == null) _playerCol = PlayerDash.instance.GetComponent<Collider2D>();
        _playerRb = _playerCol.GetComponent<Rigidbody2D>(); //
        _breakWait = new WaitForSeconds(_animationHandler.BreakAnimationDuration());
        LevelManager.instance.onLevelStart.AddListener(ForceRespawn);
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (!_breaking && collision.collider == _playerCol && PlayerAbove()) StartCoroutine(BreakRespawn());
    }

    private bool PlayerAbove() {
        return _playerRb.velocity.y < 0.05f && transform.position.y < _playerRb.transform.position.y - _playerRb.transform.lossyScale.y / 2;
    }

    private IEnumerator BreakRespawn() {
        _breaking = true;
        _animationHandler.BreakAnimation();
        onBreakingStart?.Invoke();

        yield return _breakWait;

        _animationHandler.NullAnimation();
        _col.enabled = false;
        onBreaking?.Invoke();

        yield return _respawnWait;

        _animationHandler.RespawnAnimation();
        _col.enabled = true;
        _breaking = false;
    }

    private void ForceRespawn() {
        StopAllCoroutines();
        _animationHandler.RespawnAnimation();
        _col.enabled = true;
        _breaking = false;
    }

}
