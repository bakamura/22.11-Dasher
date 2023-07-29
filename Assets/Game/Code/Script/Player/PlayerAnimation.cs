using UnityEngine;

public class PlayerAnimation : AnimationHandler {

    [Header("Cache")]

    private const string PLAYER_IDLE = "PlayerIdle";
    private const string PLAYER_DASH = "PlayerDash";

    private void Start() {
        PlayerDash dashScript = GetComponent<PlayerDash>();
        dashScript.onDashReady.AddListener(IdleAnimation);
        dashScript.onDash.AddListener(DashAnimation); 
    }

    private void IdleAnimation() {
        ChangeAnimation(PLAYER_IDLE);
    }

    private void DashAnimation(Vector2 v2) {
        ChangeAnimation(PLAYER_DASH);
    }

}
