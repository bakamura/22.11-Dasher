using UnityEngine;

public class PlatformSwitching : MonoBehaviour {

    [Header("Parameters")]

    [SerializeField] private bool _startState = true;
    private bool _switchState = true;

    [Header("Cache")]

    private PlatformSwitchingAnimation _animationHandler;
    private Collider2D _col;

    private void Awake() {
        _animationHandler = GetComponent<PlatformSwitchingAnimation>();
        _col = GetComponent<Collider2D>();
    }

    private void Start() {
        if (!_startState) SwitchState(Vector2.zero);
        PlayerDash.instance.onDash.AddListener(SwitchState);
        LevelManager.instance.onLevelEnter.AddListener(Restart);
    }

    private void SwitchState(Vector2 v2) {
        _switchState = !_switchState;
        _col.enabled = _switchState;
        _animationHandler.SwitchAnimation(_switchState);
    }

    private void Restart() {
        if (_switchState != _startState) SwitchState(Vector2.zero);
    }

#if UNITY_EDITOR
    private void OnValidate() {
        // GetComponent<SpriteRenderer>().sprite;
        _switchState = _startState;
        GetComponent<PlatformSwitchingAnimation>().SwitchAnimation(_switchState);
    }
#endif

}
