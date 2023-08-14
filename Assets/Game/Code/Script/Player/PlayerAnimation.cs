using UnityEngine;

public class PlayerAnimation : AnimationHandler {

    [Header("Indicators")]

    [SerializeField] private Transform _arrowIndicatorTransform;

    [Header("Cache")]

    private SpriteRenderer _arrowSr;
    private bool _canShowArrow = true;
    private const string PLAYER_IDLE = "PlayerIdle";
    private const string PLAYER_DASH = "PlayerDash";

    protected override void Awake() {
        base.Awake();

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

    private void ArrowDirectionUpdate(Vector2 direction) {
        if (_canShowArrow) {
            _arrowSr.enabled = true;
            _arrowIndicatorTransform.eulerAngles = Vector3.forward * Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }
    }

    private void CanShowArrow() { _canShowArrow = true; }

    private void CannotShowArrow(Vector2 v2) { _canShowArrow = false; }

    private void HideArrow() {
        _arrowSr.enabled = false;
    }

    private void IdleAnimation() {
        ChangeAnimation(PLAYER_IDLE);
    }

    private void DashAnimation(Vector2 v2) {
        ChangeAnimation(PLAYER_DASH);
    }

}
