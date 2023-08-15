using UnityEngine;

public class PlayerAnimation : AnimationHandler {

    [Header("Animation")]

    [Tooltip("Sprite is only updating its flipX when velocity is bigger than this")]
    [SerializeField] private float _flipVelocityTreshould;

    [Header("Indicators")]

    [SerializeField] private Transform _arrowIndicatorTransform;

    [Header("Cache")]

    private Rigidbody2D _rb;
    private const string PLAYER_IDLE = "PlayerIdle";
    private const string PLAYER_DASH = "PlayerDash";
    private const string PLAYER_FALL = "PlayerFall";

    private SpriteRenderer _arrowSr;
    private bool _canShowArrow = true;

    protected override void Awake() {
        base.Awake();

        _rb = GetComponent<Rigidbody2D>();
        _arrowSr = _arrowIndicatorTransform.GetComponent<SpriteRenderer>();
    }

    private void Start() {
        PlayerDash dashScript = GetComponent<PlayerDash>();
        dashScript.onDashReady.AddListener(IdleAnimation);
        dashScript.onDashReady.AddListener(CanShowArrow);
        dashScript.onDash.AddListener(DashAnimation);
        dashScript.onDash.AddListener(CannotShowArrow);
        SimulatedThumbStick.instance.onThumbStickHold.AddListener(ArrowDirectionUpdate);
        SimulatedThumbStick.instance.onThumbStickRelease.AddListener(HideArrow);
    }

    private void Update() {
        if(Mathf.Abs(_rb.velocity.x) > _flipVelocityTreshould) _sr.flipX = _rb.velocity.x < 0;
        if (_animation[_currentAnimation].name != PLAYER_IDLE) {
            if (_rb.velocity.y > 0) ChangeAnimation(PLAYER_DASH);
            else ChangeAnimation(PLAYER_FALL);
        }
    }

    private void ArrowDirectionUpdate(Vector2 direction) {
        if (_canShowArrow) {
            _arrowSr.enabled = true;
            _arrowIndicatorTransform.eulerAngles = Vector3.forward * Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }
    }

    private void HideArrow() {
        _arrowSr.enabled = false;
    }

    private void CanShowArrow() { _canShowArrow = true; }

    private void CannotShowArrow(Vector2 v2) { _canShowArrow = false; }

    private void IdleAnimation() {
        ChangeAnimation(PLAYER_IDLE);
    }

    private void DashAnimation(Vector2 v2) {
        ChangeAnimation(PLAYER_DASH);
    }

}
