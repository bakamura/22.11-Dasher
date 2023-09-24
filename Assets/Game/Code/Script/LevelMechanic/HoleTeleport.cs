using UnityEngine;
using UnityEngine.Events;

public class HoleTeleport : MonoBehaviour {

    [HideInInspector] public static UnityEvent onTeleport = new UnityEvent();

    [Header("Properties")]

    [SerializeField] private HoleTeleport _holeConnected;
    [HideInInspector] public bool canTeleport = true;

    [Header("Cache")]

    //private Vector2 _v2C;
    private float _sinC;
    private float _cosC;

    private void Start() {
        float holeConnectedAngle = (180 + transform.eulerAngles.z - _holeConnected.transform.eulerAngles.z) * Mathf.Deg2Rad;
        _sinC = Mathf.Sin(holeConnectedAngle);
        _cosC = Mathf.Cos(holeConnectedAngle);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (canTeleport && collision == PlayerDash.instance.col) Teleport();
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision == PlayerDash.instance.col) canTeleport = true;
    }

    private void Teleport() {
        onTeleport.Invoke();

        // Position
        _holeConnected.canTeleport = false;
        PlayerDash.instance.transform.position = _holeConnected.transform.position;

        // Angle
        Debug.Log($"Vel Before: {PlayerDash.instance.rb.velocity}");
        // Set not working?
        //PlayerDash.instance.rb.velocity.Set(PlayerDash.instance.rb.velocity.x * _cosC - PlayerDash.instance.rb.velocity.y * _sinC,
        //PlayerDash.instance.rb.velocity.x * _sinC + PlayerDash.instance.rb.velocity.y * _cosC);
        PlayerDash.instance.rb.velocity = new Vector2(PlayerDash.instance.rb.velocity.x * _cosC - PlayerDash.instance.rb.velocity.y * _sinC,
        PlayerDash.instance.rb.velocity.x * _sinC + PlayerDash.instance.rb.velocity.y * _cosC);
        Debug.Log($"Vel After: {PlayerDash.instance.rb.velocity}");
        //_v2C[0] = PlayerDash.instance.rb.velocity.x * _cosC - PlayerDash.instance.rb.velocity.y * _sinC;
        //_v2C[1] = PlayerDash.instance.rb.velocity.x * _sinC + PlayerDash.instance.rb.velocity.y * _cosC;
    }

}
