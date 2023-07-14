using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Player : MonoBehaviour, IRestart {

    public static Player Instance;
    public bool active = true;

    [Header("Dash")]

    [SerializeField] private float _dashForce;
    [SerializeField] private float _internalCooldown;
    private float _currentCooldown;

    // Input
    private Touch touch;
    private RectTransform[] _inputIgnoreRectTransform;
    private EventSystem _eventSystem;

    [Header("Camera")]

    [SerializeField] private float _camShakeIntensity;
    [SerializeField] private float _camShakeDuration;
    private WaitForSeconds _waitShake;

    [Header("Time")]

    [HideInInspector] public float initialTime;
    private bool _notHasInput = true;

    [Header("Restart")]

    private Vector3 _initialPos;

    [Header("Cache")]

    private Camera _cam;
    private Rigidbody2D _rb;

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) {
            Debug.Log("More than one player instance found!");
            Destroy(gameObject);
        }
    }

    private void Start() {
        _cam = Camera.main;
        _rb = GetComponent<Rigidbody2D>();

        GameObject[] ignoreObjects = GameObject.FindGameObjectsWithTag("IgnoreDash");
        _inputIgnoreRectTransform = new RectTransform[ignoreObjects.Length];
        for (int i = 0; i < ignoreObjects.Length; i++) _inputIgnoreRectTransform[i] = ignoreObjects[i].GetComponent<RectTransform>();

        _waitShake = new WaitForSeconds(_camShakeDuration / 4);
    }

    private void Update() {
        print(NotIgnoreArea(Input.mousePosition));
        if (active) {
            _currentCooldown -= Time.deltaTime;

            // Mobile
            if (Input.touchCount > 0) {
                touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Ended && NotIgnoreArea(touch.position)) StartCoroutine(Dash(_cam.ScreenToWorldPoint(touch.position) - transform.position));
            }
#if UNITY_EDITOR
            // Pc
            if (Input.GetMouseButtonUp(0) && NotIgnoreArea(Input.mousePosition)) StartCoroutine(Dash(_cam.ScreenToWorldPoint(Input.mousePosition) - transform.position));
#endif
        }
    }

    private IEnumerator Dash(Vector2 direction) {
        if (!Hud.Instance.pauseMenu.activeSelf && _currentCooldown <= 0) {
            direction = direction.normalized;
            _rb.velocity = direction * _dashForce;
            _currentCooldown = _internalCooldown;
            Vector3 camPos = new Vector3(direction.x * _camShakeIntensity, direction.y * _camShakeIntensity, -10);
            _cam.transform.position = camPos;

            yield return _waitShake;

            camPos[0] = 0;
            camPos[1] = 0;
            _cam.transform.position = camPos;

            yield return _waitShake;

            camPos[0] = direction.x * -_camShakeIntensity;
            camPos[1] = direction.y * -_camShakeIntensity;
            _cam.transform.position = camPos;

            yield return _waitShake;

            camPos[0] = 0;
            camPos[1] = 0;
            _cam.transform.position = camPos;
        }

        if (_notHasInput) {
            _notHasInput = false;
            initialTime = Time.timeSinceLevelLoad;
        }
    }

    public void ResetDash() {
        _currentCooldown = 0;
    }

    public bool NotIgnoreArea(Vector2 inputPos) {
        Vector2 localPoint;
        foreach (RectTransform ignoreRectTransform in _inputIgnoreRectTransform) 
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(ignoreRectTransform, inputPos, _cam, out localPoint) && ignoreRectTransform.rect.Contains(localPoint)) return false;
        return true;
    }

    public void Restart() {
        _initialPos = transform.position;
        _currentCooldown = 0;

        _notHasInput = true;
    }
}
