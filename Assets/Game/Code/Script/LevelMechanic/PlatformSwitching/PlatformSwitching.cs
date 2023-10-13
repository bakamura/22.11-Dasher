using UnityEngine;
using UnityEngine.Events;

public class PlatformSwitching : MonoBehaviour {

    [HideInInspector] public static UnityEvent onSwitch = new UnityEvent(); 

    [Header("Parameters")]

    [SerializeField] private bool _startState = true;
    private bool _switchState = true;

    [Header("Cache")]

    private PlatformSwitchingAnimation _animationHandler;
    private Collider2D _col;
    private static bool _switchThisFrame = false;

    private void Awake() {
        _animationHandler = GetComponent<PlatformSwitchingAnimation>();
        _col = GetComponent<Collider2D>();
    }

    private void Start() {
        if (!_startState) SwitchState(Vector2.zero);
        PlayerDash.instance.onDash.AddListener(SwitchState);
        LevelManager.instance.onLevelStart.AddListener(Restart);
    }

    private void Update() {
        _switchThisFrame = false;
    }

    private void SwitchState(Vector2 v2) {
        _switchState = !_switchState;
        _col.enabled = _switchState;
        _animationHandler.SwitchAnimation(_switchState);
        if (!_switchThisFrame) {
            onSwitch.Invoke();
            _switchThisFrame = true;
        }
    }

    private void Restart() {
        if (_switchState != _startState) SwitchState(Vector2.zero);
    }

#if UNITY_EDITOR
    [Header("Visualizing")]

    [Tooltip("0 = Active, 1 = Inactive")]
    [SerializeField] private Sprite[] _sprites;

    private void OnValidate() {
        if (!Application.isPlaying) {
            GetComponent<SpriteRenderer>().sprite = _sprites[_startState ? 0 : 1];
            GetComponent<Collider2D>().enabled = _startState;
        }
    }
#endif

}
