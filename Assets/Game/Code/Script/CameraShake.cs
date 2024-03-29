using System.Collections;
using UnityEngine;

public class CameraShake : Singleton<CameraShake> {
    
    [Header("Parameters")]

    [SerializeField] private float _shakeAmount;
    [SerializeField] private float _shakeDuration;
    private Vector3 _anchorPosition;

    [Header("Cache")]

    private WaitForSeconds _shakeWait;

    protected override void Awake() {
        base.Awake();

        _shakeWait = new WaitForSeconds(_shakeDuration / 3);
        _anchorPosition = transform.position;
    }

    private void Start() {
        PlayerDash.instance.onDash.AddListener(ShakeCamera);
        LevelManager.instance.onLevelStart.AddListener(ShakeStop);
    }

    private void ShakeCamera(Vector2 direction) {
        StartCoroutine(ShakeCameraRoutine(direction));
    }

    private IEnumerator ShakeCameraRoutine(Vector2 direction) {
        Vector3 shakeVector = (Vector3)(direction * _shakeAmount);
        transform.position = _anchorPosition + shakeVector;

        yield return _shakeWait;

        transform.position = _anchorPosition;

        yield return _shakeWait;

        transform.position = _anchorPosition - shakeVector;

        yield return _shakeWait;

        transform.position = _anchorPosition;
    }

    private void ShakeStop() {
        StopAllCoroutines();
        transform.position = _anchorPosition; // Reset Anchor pos if level has moving camera
    }
}
