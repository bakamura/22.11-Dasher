using UnityEngine;

public class SetPlayerSpawn : MonoBehaviour {

    private void Start() {
        PlayerDash.instance.initialPos = transform.position;
        Destroy(gameObject);
    }

}
