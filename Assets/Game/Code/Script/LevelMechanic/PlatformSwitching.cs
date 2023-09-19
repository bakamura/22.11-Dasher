using UnityEngine;

public class PlatformSwitching : MonoBehaviour {

    [Header("Parameters")]

    [SerializeField] private bool _switchState;

    [Header("Cache")]

    private PlatformSwitchingAnimation _animationHandler;
    private Collider2D _col;

    private void Awake() {
        _animationHandler = GetComponent<PlatformSwitchingAnimation>();
        _col = GetComponent<Collider2D>();
    }

    private void Start() {
        PlayerDash.instance.onDash.AddListener(Switch);
    }

    private void Switch(Vector2 v2) {
        _switchState = !_switchState;
        _col.enabled = _switchState;
        _animationHandler.SwitchAnimation(_switchState);
    }

#if UNITY_EDITOR
    private void OnValidate() {
        _col = GetComponent<Collider2D>();
        _col.enabled = _switchState;
        _animationHandler = GetComponent<PlatformSwitchingAnimation>();
        _animationHandler.SwitchAnimation(_switchState);
    }
#endif

}
