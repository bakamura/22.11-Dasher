using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float _dashForce;
    [SerializeField] private float _internalCooldown;
    private float _currentCooldown;

    private Camera _cam;
    private Rigidbody2D _rb;

    private void Start() {
        _cam = Camera.main;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        _currentCooldown -= Time.deltaTime;

        // Make input stop working when and while pausing
        // Mobile
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) Dash(_cam.ScreenToWorldPoint(Input.GetTouch(0).position) - transform.position);

        // Pc
        if(Input.GetMouseButtonDown(0)) Dash(_cam.ScreenToWorldPoint(Input.mousePosition) - transform.position);
    }

    private void Dash(Vector2 direction) {
        if (_currentCooldown <= 0) {
            _rb.velocity = direction.normalized * _dashForce;
            _currentCooldown = _internalCooldown;
        }
    }

}
