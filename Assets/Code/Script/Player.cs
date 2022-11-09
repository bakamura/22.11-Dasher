using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player Instance;
    public bool active = true;

    [SerializeField] private float _dashForce;
    [SerializeField] private float _internalCooldown;
    private float _currentCooldown;

    [SerializeField] private float _camShakeIntensity;
    [SerializeField] private float _camShakeDuration;
    private WaitForSeconds _waitShake;

    [HideInInspector] public float elapsedTime;

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

        _waitShake = new WaitForSeconds(_camShakeDuration / 4);
    }

    private void Update() {
        if (active) {
            _currentCooldown -= Time.deltaTime;

            // Make input stop working when and while pausing
            // Mobile
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) StartCoroutine(Dash(_cam.ScreenToWorldPoint(Input.GetTouch(0).position) - transform.position));

            // Pc
            if (Input.GetMouseButtonUp(0)) StartCoroutine(Dash(_cam.ScreenToWorldPoint(Input.mousePosition) - transform.position));

            elapsedTime += Time.deltaTime;
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
    }

    public void ResetDash() {
        _currentCooldown = 0;
    }

}
