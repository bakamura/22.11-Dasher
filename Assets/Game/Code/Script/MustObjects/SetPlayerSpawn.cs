using UnityEngine;

public class SetPlayerSpawn : MonoBehaviour {

    private void Start() {
        PlayerDash.instance.initialPos = transform.position;
        PlayerDash.instance.transform.position = transform.position; // Should be done instead in LevelManager.onLevelStart, check script priority
        Destroy(gameObject);
    }

}
