using UnityEngine;

public class SetPlayerSpawn : MonoBehaviour {

    private void Start() {
        PlayerDash.instance.initialPos = transform.position;
        PlayerDash.instance.transform.position = transform.position;
        Destroy(gameObject);
    }

}
