using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SimulatedThumbStick : Singleton<SimulatedThumbStick> {

    [Header("Parameters")]

    [SerializeField] private RectTransform _thumbStickRectTransform;
    private GameObject _thumbStickContainerRectTransform;
    private Vector2 _thumbStickContainerPos;
    [SerializeField] private float _thumbStickDragMin;
    [SerializeField] private float _thumbStickDragMax;
    [SerializeField] private float _thumbStickRecognitionMax;
    [HideInInspector] public UnityEvent<Vector2> onThumbStickHold = new UnityEvent<Vector2>();
    [HideInInspector] public UnityEvent<Vector2> onThumbStickRelease = new UnityEvent<Vector2>();
    [HideInInspector] public UnityEvent onThumbStickCancel = new UnityEvent();

    [Header("Cache")]

    private Touch _touch;
    private Vector2 _touchDirection;
    private bool _isInRange;
    private float _screenY;

    protected override void Awake() {
        base.Awake();

        _thumbStickContainerRectTransform = _thumbStickRectTransform.parent.gameObject;
        _thumbStickContainerPos = _thumbStickRectTransform.position;
    }

    private void Start() {
        FindObjectOfType<HUD>().onPause.AddListener(ToggleInput);
        Goal.onGoal.AddListener(HideInput);
        LevelManager.instance.onLevelStart.AddListener(ShowInput);
        _screenY = Screen.height;
    }

    private void Update() {
        if (_thumbStickContainerRectTransform.activeSelf) ThumbStickInput();

#if UNITY_EDITOR
        if (_thumbStickContainerRectTransform.activeSelf) {
            if (Input.GetMouseButton(0)) {
                _touchDirection = (Vector2)Input.mousePosition - _thumbStickContainerPos;

                _isInRange = IsInRange(_touchDirection.magnitude, _thumbStickDragMin, _thumbStickRecognitionMax);
                _thumbStickRectTransform.anchoredPosition = _isInRange ? Vector2.ClampMagnitude(_touchDirection, _thumbStickDragMax) : Vector2.zero;
                if (_isInRange) onThumbStickHold?.Invoke(_touchDirection);
                else onThumbStickCancel?.Invoke();
            }
            else if (Input.GetMouseButtonUp(0)) {
                if (IsInRange(_touchDirection.magnitude, _thumbStickDragMin, _thumbStickRecognitionMax)) onThumbStickRelease?.Invoke(_touchDirection);
                _thumbStickRectTransform.anchoredPosition = Vector2.zero;
            }
        }
#endif
    }

    private void ThumbStickInput() {
        if (Input.touchCount > 0) {
            _touch = Input.GetTouch(0);
            _touchDirection = _touch.position - _thumbStickContainerPos;
            if (IsInRange(_touchDirection.magnitude, _thumbStickDragMin, _thumbStickRecognitionMax)) {
                if (_touch.phase == TouchPhase.Ended) {
                    onThumbStickRelease?.Invoke(_touchDirection);
                    _thumbStickRectTransform.anchoredPosition = Vector2.zero; // Create Method and subscribe to event
                }
                else {
                    _thumbStickRectTransform.anchoredPosition = Vector2.ClampMagnitude(_touchDirection, _thumbStickDragMax); // Create Method and subscribe to event
                    onThumbStickHold?.Invoke(_touchDirection);
                }
            }
            else {
                _thumbStickRectTransform.anchoredPosition = Vector2.zero; // Create Method and subscribe to event
                onThumbStickCancel?.Invoke();
            }
        }
    }

    private bool IsInRange(float value, float min, float max) {
        return min <= value && value <= max;
    }

    private void ToggleInput(bool isTrue) {
        _thumbStickContainerRectTransform.SetActive(isTrue);
        _thumbStickRectTransform.gameObject.SetActive(isTrue);
    }

    private void ShowInput() {
        // Putting in a Routine to prevent Input on the same event as closing a Menu
        StartCoroutine(ShowInputRoutine());
    }

    private IEnumerator ShowInputRoutine() {
        yield return null;
        yield return null; //
        yield return null; //

        ToggleInput(true);
    }

    private void HideInput() {
        ToggleInput(false);
    }

}
